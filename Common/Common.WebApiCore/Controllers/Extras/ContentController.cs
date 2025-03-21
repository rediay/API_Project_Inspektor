using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.Services.Extras;
using Common.Services.Management;
using Common.WebApiCore.Controllers.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Extras
{
    [Route("Content")]
    public class ContentController : BaseApiController
    {
        protected readonly IContentService _contentService;
        protected readonly IContentCategoryService _contentCategory;
        protected readonly IContentTypeService _contentType;
        protected readonly IContentStatusesServices _contentStatusesServices;




        public ContentController(IContentService planService, IContentCategoryService contentCategory, IContentTypeService contentType, IContentStatusesServices contentStatusesServices)
        {
            _contentService = planService;
            _contentCategory = contentCategory;
            _contentType = contentType;
            _contentStatusesServices = contentStatusesServices;
        }

        #region CRUD CATEGORY

        [HttpGet]
        [Route(nameof(ContentController.GetCategorys))]
        public async Task<IActionResult> GetCategorys()
        {
            var plans = await _contentCategory.GetCategorys();
            return Ok(plans);
        }

        [HttpGet]
        [Route(nameof(ContentController.GetCategoryId))]
        public async Task<IActionResult> GetCategoryId(int id)
        {
            var plans = await _contentCategory.GetCategoryId(id);
            return Ok(plans);
        }

        [HttpGet]
        [Route(nameof(ContentController.GetCategorysbyType))]
        public async Task<IActionResult> GetCategorysbyType(int id)
        {
            var categorys = await _contentCategory.GetCategorysbyType(id);
            return Ok(categorys);
        }

        [HttpPost]
        [Route(nameof(ContentController.UpdateCategory))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> UpdateCategory(ContentCategoryDTO categoyDTO)
        {
            var result = await _contentCategory.UpdateCategory(categoyDTO);

            if (result.Id != categoyDTO.Id)
                return BadRequest();

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.AddCategory))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddCategory(ContentCategoryDTO categoyDTO)
        {


            var result = await _contentCategory.UpdateCategory(categoyDTO);
            if (result == null)
                return BadRequest(result);

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.DeleteCategory))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool result = await _contentCategory.DeleteCategory(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }




        #endregion

        #region CRUD STATE
        /// <summary>
        ///Obtiene todos los estados de de las publicaciones
        /// </summary>
        [HttpGet]
        [Route(nameof(ContentController.GetStates))]
        public async Task<IActionResult> GetStates()
        {
            var plans = await _contentStatusesServices.GetStates();
            return Ok(plans);
        }

        [HttpGet]
        [Route(nameof(ContentController.GetStateId))]
        public async Task<IActionResult> GetStateId(int id)
        {
            var plans = await _contentStatusesServices.GetStateId(id);
            return Ok(plans);
        }

        [HttpPost]
        [Route(nameof(ContentController.UpdateState))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> UpdateState(ContentStatusDTO stateDTO)
        {
            var result = await _contentStatusesServices.UpdateState(stateDTO);

            if (result.Id != stateDTO.Id)
                return BadRequest();

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.AddState))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddState(ContentStatusDTO stateDTO)
        {


            var result = await _contentStatusesServices.UpdateState(stateDTO);
            if (result == null)
                return BadRequest(result);

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.DeleteState))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _contentStatusesServices.DeleteState(id);
            if (state)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region CRUD TypeContent

        [HttpGet]
        [Route(nameof(ContentController.GetTypeContent))]
        public async Task<IActionResult> GetTypeContent()
        {
            var plans = await _contentType.GetTypeContent();
            return Ok(plans);
        }

        [HttpGet]
        [Route(nameof(ContentController.GetTypeContentId))]
        public async Task<IActionResult> GetTypeContentId(int id)
        {
            var plans = await _contentType.GetTypeContentId(id);
            return Ok(plans);
        }

        [HttpPost]
        [Route(nameof(ContentController.UpdateTypeContent))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> UpdateTypeContent(ContentTypeDTO typecontentDTO)
        {
            var result = await _contentType.UpdateTypeContent(typecontentDTO);

            if (result.Id != typecontentDTO.Id)
                return BadRequest();

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.AddTypeContent))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddTypeContent(ContentTypeDTO stateDTO)
        {


            var result = await _contentType.UpdateTypeContent(stateDTO);
            if (result == null)
                return BadRequest(result);

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.DeleteTypeContent))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteTypeContent(int id)
        {
            var result = await _contentType.DeleteTypeContent(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region CRUD Content
        [HttpGet]
        [Route(nameof(ContentController.GetContents))]
        public async Task<IActionResult> GetContents()
        {
            var plans = await _contentService.GetContents();
            return Ok(plans);
        }

        [HttpGet]
        [Route(nameof(ContentController.GetContentId))]
        public async Task<IActionResult> GetContentId(int id)
        {
            var plans = await _contentService.GetContentId(id);
            return Ok(plans);
        }

        [HttpPost]
        [Route(nameof(ContentController.UpdateContent))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> UpdateContent(ContentDTO contentDTO)
        {
            var result = await _contentService.UpdateContent(contentDTO);

            if (result.Id != contentDTO.Id)
                return BadRequest();

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.AddContent))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddContent(ContentDTO stateDTO)
        {


            var result = await _contentService.UpdateContent(stateDTO);
            if (result == null)
                return BadRequest(result);

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(ContentController.DeleteContent))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var result = await _contentService.DeleteContent(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
