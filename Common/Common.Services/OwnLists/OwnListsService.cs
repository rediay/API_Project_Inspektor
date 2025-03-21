using AutoMapper;
using Common.DTO;
using Common.DTO.OwnLists;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.OwnLists;
using Common.Services.Infrastructure.Repositories.OwnLists;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.OwnLists
{
    public class OwnListsService : BaseService, IOwnListsService
    {
        private readonly IMapper _mapper;
        protected readonly IOwnListsRepository _ownListsRepository;
        public OwnListsService(ICurrentContextProvider contextProvider, IOwnListsRepository ownListsRepository, IMapper mapper) : base(contextProvider)        {
            
            _ownListsRepository = ownListsRepository;
            _mapper = mapper;
        }

        public async Task<List<OwnListDTO>> GetOwnLists(int CompanyId)
        {
            var ownList = await _ownListsRepository.GetOwnLists(CompanyId, Session);
            var map = ownList.MapTo<List<OwnListDTO>>();
            return map;
        }

        public async Task<bool> UpdateOwnList(OwnListDTO ownListDTO)
        {
            try
            {
                if (ownListDTO.Id!=0)
                {
                    var ownList= ownListDTO.MapTo<Entities.OwnList>();
                    return await _ownListsRepository.UpdateOwnList(ownList, Session);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> CreateOwnList(OwnListDTO ownListDTO)
        {
            try
            {
                var ownList = ownListDTO.MapTo<Entities.OwnList>();
                return await _ownListsRepository.UpdateOwnList(ownList, Session);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }


        }

        public async Task<bool> DeleteOwnList(int id)
        {

            try
            {               
                return await _ownListsRepository.DeleteOwnList(id, Session);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
