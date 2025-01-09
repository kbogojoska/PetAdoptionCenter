using Microsoft.IdentityModel.Tokens;
using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Domain.Identity;
using PetShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Mappers
{
    public static class PetShopApplicationUserMapper
    {

        public static PetShopApplicationUserDTO toDTO (this PetShopApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var adoptionApplications = new List<AdoptionApplication>();

            if (!user.AdoptionApplications.IsNullOrEmpty())
            {
                adoptionApplications = user.AdoptionApplications.ToList();
            }
            return new PetShopApplicationUserDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Email = user.Email,
                Age = user.Age,
                AdoptionApplications = adoptionApplications,
                ContactPhoneNumber = user.ContactPhoneNumber,
            };
        }

        public static PetShopApplicationUser toApplicationUser(this PetShopApplicationUserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "User cannot be null");
            }

            var adoptionApplications = new List<AdoptionApplication>();

            var user =  new PetShopApplicationUser
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Address = userDTO.Address,
                Email = userDTO.Email,
                Age = userDTO.Age,
                ContactPhoneNumber = userDTO.ContactPhoneNumber,
            };

            if (!userDTO.AdoptionApplications.IsNullOrEmpty())
            {
                adoptionApplications = userDTO.AdoptionApplications.ToList();
            }

            user.AdoptionApplications = adoptionApplications;

            return user;
        }
    }
}
