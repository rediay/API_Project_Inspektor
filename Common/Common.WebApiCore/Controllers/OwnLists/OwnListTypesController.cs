using Common.DTO;
using Common.Services.Infrastructure.Management;
using Common.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Common.WebApiCore.Controllers.OwnLists
{
    [Route("OwnListTypes")]
    public class OwnListTypesController : BaseApiController
    {
        protected readonly IOwnListTypesService _ownListTypesService;

        public OwnListTypesController(IOwnListTypesService ownListTypesService)
        {
            _ownListTypesService = ownListTypesService;
        }

        [HttpGet]
        [Route(nameof(OwnListTypesController.GetOwnListTypes))]
        [ValidateIdCompany]
        public async Task<IActionResult> GetOwnListTypes(int CompanyId)
        {
            var companyTypeList = await _ownListTypesService.GetOwnListTypes(CompanyId);
            return Ok(companyTypeList);
        }

        [HttpPost]
        [Route(nameof(OwnListTypesController.UpdateOwnListTypes))]
        [ValidateIdCompany]
        public async Task<IActionResult> UpdateOwnListTypes(OwnListTypesDTO ownListTypesDTO)
        {
            bool result = await _ownListTypesService.UpdateOwnListType(ownListTypesDTO);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route(nameof(OwnListTypesController.CreateOwnListType))]
        [ValidateIdCompany]
        public async Task<IActionResult> CreateOwnListType(OwnListTypesDTO ownListTypesDTO)
        {
            bool result = await _ownListTypesService.CreateOwnListType(ownListTypesDTO);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route(nameof(OwnListTypesController.DeleteOwnListType))]
        [ValidateIdCompany]
        public async Task<IActionResult> DeleteOwnListType(string id)
        {
            bool result = await _ownListTypesService.DeleteOwnListType(Convert.ToInt32(id));

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route(nameof(OwnListTypesController.ExportExcel))]
        [ValidateIdCompany]
        public async Task<IActionResult> ExportExcel(int CompanyId)
        {
            var FileHelper = new FilesHelper();

            var companyTypeList = await _ownListTypesService.GetOwnListTypes(CompanyId);
            if (companyTypeList == null)
                return BadRequest();
            else
            {
                List<dynamic> data = new List<dynamic>();
                data.Add(companyTypeList);
                List<string> names = new List<string>()
                {
                    "ListasPropias",
                };
                return File(FileHelper.TableToExcel(data, names, null),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ListasPropias.xlsx");
            }
        }
        
        [HttpPost]
        [Route("ImportOwnLists")]
        [Authorize]
        public async Task<bool> ImportOwnLists()
        {
            var ownListTypeId = Convert.ToInt32(Request.Form["ownListTypeId"]);
            var templateFile = Request.Form.Files[0];
            return await _ownListTypesService.ImportOwnLists(ownListTypeId, templateFile);
        }

        [HttpPost]
        [Route("DeleteImportedOwnListsByType/{ownListTypeId:int}")]
        [Authorize]
        public async Task<bool> DeleteImportedOwnListsByType(int ownListTypeId)
        {
            return await _ownListTypesService.DeleteImportedOwnListsByType(ownListTypeId);
        }
    }
}
