using AutoMapper;
using Common.DTO;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.Repositories.OwnLists;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Http;

namespace Common.Services.OwnLists
{
    public class OwnListTypesService : BaseService, IOwnListTypesService
    {
        private readonly IMapper _mapper;
        protected readonly IOwnListsTypesRepository _ownListsTypesRepository;
        private readonly IUserService _userService;

        public OwnListTypesService(ICurrentContextProvider contextProvider,
            IUserService userService,
            IOwnListsTypesRepository ownListsTypesRepository, IMapper mapper) : base(contextProvider)
        {
            _ownListsTypesRepository = ownListsTypesRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<OwnListTypesDTO>> GetOwnListTypes(int CompanyId)
        {
            var ownListType = await _ownListsTypesRepository.GetOwnListTypes(CompanyId, Session);
            var map = ownListType.MapTo<List<OwnListTypesDTO>>();
            return map;
        }

        public async Task<bool> UpdateOwnListType(OwnListTypesDTO ownListTypesDTO)
        {
            try
            {
                if (ownListTypesDTO.Id!=0)
                {
                    var ownListType= ownListTypesDTO.MapTo<Entities.OwnListType>();
                    return await _ownListsTypesRepository.UpdateOwnListType(ownListType, Session);
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

        public async Task<bool> CreateOwnListType(OwnListTypesDTO ownListTypesDTO)
        {
            try
            {
                var ownListType = ownListTypesDTO.MapTo<Entities.OwnListType>();
                return await _ownListsTypesRepository.UpdateOwnListType(ownListType, Session);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }


        }

        public async Task<bool> DeleteOwnListType(int id)
        {

            try
            {               
                return await _ownListsTypesRepository.DeleteOwnListType(id, Session);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> ImportOwnLists(int ownListTypeId, IFormFile templateFile)
        {
            var dataSet = FilesHelper.ExcelToDataSet(FilesHelper.IFormFileToByteArray(templateFile));
            var tableCollection = dataSet.Tables[0];
            var ownLists = new List<OwnList>();
            
            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var companyId = currentUser.CompanyId;

            foreach (DataRow row in tableCollection.Rows)
            {
                OwnList ownList = new OwnList
                {
                    //DocumentType = Convert.ToString(row["TipoDocumento"]),
                    TypeIdentification = Convert.ToString(row["TipoDocumento"]),
                    Identification = Convert.ToString(row["DocumentoIdentidad"]),
                    Name = Convert.ToString(row["NombreCompleto"]),
                    Source = Convert.ToString(row["FuenteConsulta"]),
                    //TipoPersona = Convert.ToString(row["TipoPersona"]),
                    Alias = Convert.ToString(row["Alias"]),
                    Crime = Convert.ToString(row["Delito"]),
                    Zone = Convert.ToString(row["Zona"]),
                    Link = Convert.ToString(row["Link"]),
                    MoreInformation = Convert.ToString(row["OtraInformacion"]),
                    OwnListTypeId = ownListTypeId,
                    CompanyId = companyId
                };
                ownLists.Add(ownList);
            }

            return await _ownListsTypesRepository.ImportOwnLists(ownLists, Session);
        }

        public Task<bool> DeleteImportedOwnListsByType(int ownListTypeId)
        {
            return _ownListsTypesRepository.DeleteImportedOwnListsByType(ownListTypeId, Session);
        }
    }
}
