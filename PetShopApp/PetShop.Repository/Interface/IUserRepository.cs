using PetShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<PetShopApplicationUser> GetAll();
        PetShopApplicationUser Get(string id);
        void Insert(PetShopApplicationUser user);
        void Update(PetShopApplicationUser user);
        void Delete(PetShopApplicationUser user);
    }
}
