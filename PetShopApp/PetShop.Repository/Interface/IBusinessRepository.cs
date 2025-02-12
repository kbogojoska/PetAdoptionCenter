using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Repository.Interface
{
    public interface IBusinessRepository
    {
        void TransferBusinessData();
        List<Business> GetAllBusinesses();
    }
}
