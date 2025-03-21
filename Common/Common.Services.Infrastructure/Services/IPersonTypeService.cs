using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface IPersonTypeService
    {
        Task<ResponseDTO<List<PersonType>>> GetAll(bool includeDeleted = false);
        Task<ResponseDTO<PersonType>> GetById(int id, bool includeDeleted = false);
    }
}