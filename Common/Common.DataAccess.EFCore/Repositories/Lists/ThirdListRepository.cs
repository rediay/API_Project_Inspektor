/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Amazon.Runtime.Documents;
using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure.Repositories.Lists;
using Common.Services.Infrastructure.Repositories.RestrictiveLists;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.Lists
{
    public class ThirdListRepository : BaseLoggableRepository<ThirdList, DataContext>, IThirdListRepository
    {
        public ThirdListRepository(DataContext context) : base(context)
        {
        }
        
   

        public async Task<PagedResponseDTO<List<ThirdList>>> GetAll(ContextSession session,
          PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var total = await queryEntities.CountAsync();

            var newsList = await queryEntities
              .Skip(totalSkipped)
              .Take(paginationFilterDto.PerPage)
              .ToListAsync(); // Materializa solo la página actual de resultados

            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;


            var pagedResponseDto = new PagedResponseDTO<List<ThirdList>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<PagedResponseDTO<List<ThirdList>>> GetAllQuery(ContextSession session,
         ListPaginationFilterThirdDTO paginationFilter, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilter.PageNumber - 1) * paginationFilter.PerPage;

            var queryEntities = GetEntities(session);

            if (paginationFilter.Name is not null)
                queryEntities = queryEntities.Where(obj => obj.Name.Contains(paginationFilter.Name));

            if (paginationFilter.Document is not null)
                queryEntities = queryEntities.Where(obj => obj.Document.Contains(paginationFilter.Document));

            if (paginationFilter.DocumentTypeId is not null)
            {
                var entero = int.Parse(paginationFilter.DocumentTypeId);
                queryEntities = queryEntities.Where(obj => obj.DocumentTypeId == entero);
            }

            var filteredList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilter.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilter.PageNumber;
            var perPage = paginationFilter.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<ThirdList>>(filteredList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<PagedResponseDTO<List<ThirdList>>> GetAllToVerify(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session);

            queryEntities = queryEntities.Include(p => p.DocumentType);
            // Filtra y selecciona solo los elementos que cumplen las condiciones
            var filteredEntities = await queryEntities
                .Where(item => !item.Validated && item.TempData != null)
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            // Crea una nueva lista para almacenar los objetos deserializados
            var deserializedList = new List<ThirdList>();

            foreach (var entity in filteredEntities)
            {
                // Deserializa TempData y agrega el resultado a la nueva lista
                var thirdList = JsonConvert.DeserializeObject<ThirdList>(entity.TempData);
                deserializedList.Add(thirdList);
            }

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<ThirdList>>(deserializedList, pageNumber, perPage, total);

            return pagedResponseDto;
        }



        public async Task<ThirdList> GetListById(string id,ContextSession session)
        {
            var list = await GetEntities(session).Where(x => x.Id == int.Parse(id))
                .FirstOrDefaultAsync();
            return list;
        }

        public async Task<bool> BulkCreate(List<ThirdList> lists, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                await context.AddRangeAsync(lists);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<bool> Delete(int id, ContextSession session)
        {
           
            var state = await GetEntities(session).FirstOrDefaultAsync(x => x.Id == id);
            if (state != null)
            {
                var context = GetContext(session);
                context.Remove(state);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }



      
    }
}