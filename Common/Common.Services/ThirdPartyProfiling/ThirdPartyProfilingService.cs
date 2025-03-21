using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.ThirdPartyProfiling;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Common.Services.Infrastructure.Services.ThirdPartyProfiling;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Common.Services.ThirdPartyProfiling
{
    public class ThirdPartyProfilingService : BaseService, IThirdPartyProfilingService
    {
        private const string NameColumn = "Nombre / Razón social";
        private const string DocumentTypeColumn = "Tipo Identificación";
        private const string DocumentColumn = "Número Identificación";
        private const string PersonTypeColumn = "Tipo Persona";

        private readonly IThirdPartyProfilingRepository _thirdPartyProfilingRepository;
        private readonly ICategoryVariableRepository _categoryVariableRepository;
        private readonly IUserService _userService;
        private readonly IRiskProfileRepository _riskProfileRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IRiskProfileVariableRepository _riskProfileVariableRepository;

        public ThirdPartyProfilingService(ICurrentContextProvider contextProvider,
            IThirdPartyProfilingRepository thirdPartyProfilingRepository,
            ICategoryVariableRepository categoryVariableRepository,
            IDocumentTypeRepository documentTypeRepository,
            IRiskProfileRepository riskProfileRepository,
            IRiskProfileVariableRepository riskProfileVariableRepository,
            IUserService userService
        ) : base(contextProvider)
        {
            _thirdPartyProfilingRepository = thirdPartyProfilingRepository;
            _categoryVariableRepository = categoryVariableRepository;
            _documentTypeRepository = documentTypeRepository;
            _riskProfileRepository = riskProfileRepository;
            _riskProfileVariableRepository = riskProfileVariableRepository;
            _userService = userService;
        }

        public async Task<PagedResponseDTO<List<ThirdPartyProfilingDTO>>> GetAll(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse =
                await _thirdPartyProfilingRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var list = pagedResponse.Data.Select(data => data.MapTo<ThirdPartyProfilingDTO>()).ToList();
            return pagedResponse.CopyWith(list);
        }

        public async Task<object> Import(IFormFile templateFile)
        {
            var dataSet = FilesHelper.ExcelToDataSet(FilesHelper.IFormFileToByteArray(templateFile));
            var tableCollection = dataSet.Tables[0];
            var updateRecords = new List<Entities.ThirdPartyProfiling>();
            var thirdPartyProfilingList = new List<ThirdPartyProfilingDTO>();
            var categoriesIds = new HashSet<int>();

            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var companyId = currentUser.CompanyId;

            var riskProfiles = await _riskProfileRepository.GetAll(Session, companyId);

            var riskProfileVariables = (await _riskProfileVariableRepository.GetAll(Session, companyId))
                .Select(item => item.Name).ToList();

            foreach (DataRow row in tableCollection.Rows)
            {
                var currentCategories = new List<int>();
                
                foreach (var name in riskProfileVariables)
                {
                    var value = row[name];
                    // var value = row["Actividad Economica"];
                    var validated = value.ToString().IsNullOrEmpty();
                    
                    if (!validated)
                    {
                        var categoryId = Convert.ToInt16(value);
                        categoriesIds.Add(categoryId);
                        currentCategories.Add(categoryId);
                    }
                }

                ThirdPartyProfilingDTO item = new ThirdPartyProfilingDTO
                {
                    Name = Convert.ToString(row[NameColumn]),
                    DocumentType = Convert.ToString(row[DocumentTypeColumn]),
                    Document = Convert.ToString(row[DocumentColumn]),
                    PersonType = Convert.ToString(row[PersonTypeColumn]),
                    CategoriesIds = currentCategories
                };

                thirdPartyProfilingList.Add(item);
            }
            

            var categoryVariables = await _categoryVariableRepository.Get(Session, companyId,
                categoriesIds.ToList());

            IDictionary<int, CategoryVariable> categoryVariablesMap = new Dictionary<int, CategoryVariable>();

            foreach (var categoryVariable in categoryVariables)
            {
                categoryVariablesMap.Add(categoryVariable.Id, categoryVariable);
            }
            
            foreach (var item in thirdPartyProfilingList)
            {
                float score = 0;

                foreach (var categoryId in item.CategoriesIds)
                {
                    var countryVariable = categoryVariablesMap[categoryId];
                    var riskProfileVariable = countryVariable.RiskProfileVariable;

                    var countryVariableWeight = countryVariable.Weight;
                    var riskProfileVariableWeight = riskProfileVariable.Weight;

                    score += countryVariableWeight * (riskProfileVariableWeight / 10);
                }

                var thirdPartyProfiling = new Entities.ThirdPartyProfiling
                {
                    Name = item.Name,
                    Document = item.Document,
                    DocumentType = item.DocumentType,
                    PersonType = item.PersonType,
                    Score = score,
                    CompanyId = companyId,
                };

                foreach (var riskProfile in riskProfiles)
                {
                    var startValue = riskProfile.StartValue;
                    var endValue = riskProfile.EndValue;

                    if (score > startValue && score <= endValue)
                    {
                        thirdPartyProfiling.Type = riskProfile.Name;
                        break;
                    }
                }

                updateRecords.Add(thirdPartyProfiling);
            }

            // return updateRecords;
            return await _thirdPartyProfilingRepository.Import(Session, updateRecords);
        }

        public async Task<List<string>> GetExportTemplateColumns()
        {
            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var companyId = currentUser.CompanyId;

            var riskProfileVariables = await _riskProfileVariableRepository.GetAll(Session, companyId);

            var cellNames = new List<string>
            {
                NameColumn, DocumentTypeColumn, DocumentColumn, PersonTypeColumn
            };

            cellNames.AddRange(riskProfileVariables.Select(riskProfile => riskProfile.Name));

            return cellNames;
        }
    }
}