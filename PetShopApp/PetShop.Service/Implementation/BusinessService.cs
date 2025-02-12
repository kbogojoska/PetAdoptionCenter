using PetShop.Domain.DTO;
using PetShop.Repository.Interface;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Implementation
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public List<BusinessDto> GetAllBusinesses()
        {
            return _businessRepository.GetAllBusinesses().Select(b => b.toBusinessDto()).ToList();

            
        }

        public void TransferBusinessData()
        {
            _businessRepository.TransferBusinessData();
        }
    }

}
