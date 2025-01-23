using PetShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Interface
{
    public interface IGenericCRUDService<TDto,TId>
    {
        TDto Store(TDto tDto); // saves in db
        TDto Update(TId id, TDto tDto); // updates in db
        TDto FindById(TId id); // TId to support string, Guid ...
        List<TDto> FindAll();
        TDto DeleteById(TId id);
    }
}
