using Common.DTO;
using Common.DTO.Management;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("DocumentTypeController")]
    public class DocumentTypeController : BaseApiController
    {

        protected readonly IDocumentTypeService _DocumentTypeService;
        

        public DocumentTypeController(IDocumentTypeService DocumentTypeService)
        {
            this._DocumentTypeService = DocumentTypeService;
            
        }

        [HttpPost]
        [Route(nameof(DocumentTypeController.Update))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Update(DocumentTypeDTO DocumentTypeDTO)
        {

            var result = await _DocumentTypeService.Edit(DocumentTypeDTO);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route(nameof(DocumentTypeController.Create))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(DocumentTypeDTO DocumentTypeDTO)
        {

            var result = await _DocumentTypeService.Edit(DocumentTypeDTO);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        } 
        [HttpPost]
        [Route(nameof(DocumentTypeController.Delete))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(string id)
        {

            bool result = await _DocumentTypeService.Delete(Convert.ToInt32(id));

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
        [Route(nameof(DocumentTypeController.DocumentType))]
        public async Task<IActionResult> DocumentType()
        {

            var result = await _DocumentTypeService.Get();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(null);
        }





    }
}
