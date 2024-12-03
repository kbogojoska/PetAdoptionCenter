using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Identity;
using PetShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new ArgumentNullException("user is not found");
            }
            _entities.Remove(user);
            _context.SaveChanges();
        }

        public PetShopApplicationUser Get(string id)
        {
            var str = id.ToString();
            return _entities.Include(u => u.Name)
                .Include(u => u.Surname)
                .Include(u => u.ContactPhoneNumber)
                .Include(u => u.Age)
                .First(u => u.Id == str);
        }

        public IEnumerable<PetShopApplicationUser> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void Insert(PetShopApplicationUser user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("user is not found");
            }
            _entities.Add(user);
            _context.SaveChanges();
        }

        public void Update(PetShopApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user is not found");
            }
            _entities.Update(user);
            _context.SaveChanges();
        }
    }
}
