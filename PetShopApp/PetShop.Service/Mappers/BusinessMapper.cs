using Microsoft.AspNetCore.Identity;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Mappers
{
    public static class BusinessMapper
    {
        public static BusinessDto toBusinessDto(this Business business)
        {
            return new BusinessDto
            {
                Id = business.Id,
                Name = business.Name,
                Address = business.Address,
                Rating = business.Rating,
                PhoneNumber = business.PhoneNumber,
                IsOpen = business.IsOpen,
                IsAvailable = business.IsAvailable,
            };
        }
    }
}
