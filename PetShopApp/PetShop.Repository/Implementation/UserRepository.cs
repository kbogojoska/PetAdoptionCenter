using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Identity;
using PetShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<PetShopApplicationUser> _entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<PetShopApplicationUser>();
        }

        public void Delete(PetShopApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is not found");
            }
            _entities.Remove(user);
            _context.SaveChanges();
        }

        public PetShopApplicationUser Get(string id)
        {
            return _entities
				.Include(u => u.AdoptionApplications)
                .ThenInclude(a => a.Pet)
                .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<PetShopApplicationUser> GetAll()
        {
            return _entities
                .Include(u => u.AdoptionApplications)
                .ThenInclude(a => a.Pet)
                .AsEnumerable();
        }

        public void Insert(PetShopApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            _entities.Add(user);
            _context.SaveChanges();
        }

        public void Update(PetShopApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            _entities.Update(user);
            _context.SaveChanges();
        }
    }
}
