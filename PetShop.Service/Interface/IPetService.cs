using PetShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Interface
{
    public interface IPetService 
    {
        ResponsePetDTO Store(RequestPetDTO requestDto); // Saves a pet using RequestPetDto
        ResponsePetDTO Update(Guid id, RequestPetDTO requestDto); // Updates a pet using RequestPetDto
        RequestPetDTO FindById(Guid id); // Retrieves a pet by Id as ResponsePetDto
        List<RequestPetDTO> FindAll(); // Retrieves all pets as a list of ResponsePetDto
        ResponsePetDTO DeleteById(Guid id);
        List<RequestPetDTO> FindByShelter(Guid shelterId);
        List<RequestPetDTO> FindByCity(string city);

    }
}
