using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;
using PetShop.Domain.Identity;

namespace PetShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<PetShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Shelter> Shelters { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<AdoptionApplication> AdoptionApplications { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }

    }
}
