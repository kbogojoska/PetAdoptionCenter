﻿using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;
using PetShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public T Delete(Guid? id)
        {
            T instance = Get(id);
            _entities.Remove(instance);
            _context.SaveChanges();
            return instance;
        }

        public T Get(Guid? id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _entities;

            if (include != null)
            {
                query = include(query); 
            }

            return id == null ? null : query.FirstOrDefault(s => s.Id == id.Value);
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _entities;

            if (include != null)
            {
                query = include(query); 
            }

            return query.AsEnumerable();
        }

        public T Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }
            _entities.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }
            _entities.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> FindBy(Func<T, bool> predicate)
        {
            return _entities.AsEnumerable().Where(predicate);
        }

    }
}
