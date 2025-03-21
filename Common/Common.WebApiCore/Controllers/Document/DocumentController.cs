

using Common.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Common.DTO;
using Rotativa.AspNetCore;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Common.Services.Infrastructure.Document;

namespace Common.WebApiCore.Controllers.Document
{
    [Route("Document")]
    public class DocumentController : BaseApiController
    {
        protected readonly IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        //[HttpGet]
        //[Route("{QueryId:int}")]
        //[Authorize(Policy = "AdminOnly")]
        //public async Task<IActionResult> Get(int QueryId)
        //{
        //}
        [HttpGet]
        //[Route("{document:string}")]
        [Route(nameof(DocumentController.GetName))]
        [Authorize]
        public async Task<IActionResult> GetName(string document)
        {
             //return Ok(document);
            var result = await documentService.GetNameByDocument(document);
            if (result != null)
                return Ok(result);
            return NotFound();
        } 
        [HttpGet]
        //[Route("{document:string}")]
        [Route(nameof(DocumentController.Get))]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] ContentPaginationFilterDTO paginationFilterDto)
        {
             //return Ok(document);
            var result = await documentService.GetDocument(paginationFilterDto);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(NombreCedulaDTO nombreCedulaDTO)
        {
            var result = await documentService.Create(nombreCedulaDTO);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(int id, NombreCedulaDTO nombreCedulaDTO)
        {
            if (id != nombreCedulaDTO.IdCliente)
            {
                return BadRequest();
            }

            var result = await documentService.Edit(nombreCedulaDTO);

            if (result !=null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
           

            var result = await documentService.DeleteDocument(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}
