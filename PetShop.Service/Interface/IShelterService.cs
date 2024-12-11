using Microsoft.EntityFrameworkCore.Update.Internal;
using PetShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Interface
{
    public interface IShelterService : IGenericCRUDService<ShelterDTO, string>
    {
    }
}
