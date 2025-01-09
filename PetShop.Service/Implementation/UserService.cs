using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Implementation
{
    using global::PetShop.Domain.Identity;
    using global::PetShop.Repository.Interface;
    using global::PetShop.Service.Interface;
    using global::PetShop.Domain.Identity;
    using global::PetShop.Domain.DTO;
    using System;
    using System.Collections.Generic;
    using global::PetShop.Service.Mappers;

    namespace PetShop.Service
    {
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;
            private readonly IPetService _petService;

            public UserService(IUserRepository userRepository, IPetService petService)
            {
                _userRepository = userRepository;
                _petService = petService;
            }

            public PetShopApplicationUserDTO FindById(string id)
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("User ID cannot be null or empty", nameof(id));

                var user = _userRepository.Get(id);
                if (user == null)
                    throw new InvalidOperationException($"User with ID {id} not found");

                return user.toDTO();
            }


            public List<PetShopApplicationUserDTO> FindAll()
            {
                var users = _userRepository.GetAll();
                if (users == null || !users.Any())
                    return new List<PetShopApplicationUserDTO>();

                return users.Select(u => u.toDTO()).ToList();
            }

            public PetShopApplicationUserDTO Update(string id, PetShopApplicationUserDTO userDto)
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("User ID cannot be null or empty", nameof(id));

                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto), "User DTO cannot be null");

                var existingUser = _userRepository.Get(id);
                if (existingUser == null)
                    throw new InvalidOperationException($"User with ID {id} not found");


				existingUser.Name = userDto.Name;
                existingUser.Email = userDto.Email; 
                existingUser.Surname = userDto.Surname;
                existingUser.ContactPhoneNumber = userDto.ContactPhoneNumber;
                existingUser.Email = userDto.Email;
                existingUser.Age = userDto.Age;
                existingUser.Address = userDto.Address;
                existingUser.AdoptionApplications = userDto.AdoptionApplications;

                _userRepository.Update(existingUser);

                return existingUser.toDTO();
            }

            public PetShopApplicationUserDTO Store(PetShopApplicationUserDTO userDto)
            {
                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto), "User DTO cannot be null");

                var newUser = userDto.toApplicationUser();
                _userRepository.Insert(newUser);

                return newUser.toDTO();
            }

            public PetShopApplicationUserDTO DeleteById(string id)
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("User ID cannot be null or empty", nameof(id));

                var user = _userRepository.Get(id);
                if (user == null)
                    throw new InvalidOperationException($"User with ID {id} not found");

                _userRepository.Delete(user);

                return user.toDTO();
            }
        }
    }

}
