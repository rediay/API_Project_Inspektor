using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class PersonTypeService : BaseService, IPersonTypeService
    {
        private readonly IPersonTypeRepository _personTypeRepository;

        public PersonTypeService(ICurrentContextProvider contextProvider, IPersonTypeRepository personTypeRepository) : base(contextProvider)
        {
            _personTypeRepository = personTypeRepository;
        }

        public async Task<ResponseDTO<List<PersonType>>> GetAll(bool includeDeleted = false)
        {
            var records = await _personTypeRepository.GetAll(Session, includeDeleted);
            var response = new ResponseDTO<List<PersonType>>(records);
            return response;
        }

        public async Task<ResponseDTO<PersonType>> GetById(int id, bool includeDeleted = false)
        {
            var result = await _personTypeRepository.Get(id, Session, includeDeleted);

            if (result == null)
            {
                return new ResponseDTO<PersonType>(null) {Succeeded = false};
            }

            var response = new ResponseDTO<PersonType>(result);

            return response;
        }
    }
}