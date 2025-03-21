/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Services.Infrastructure.Mail;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Cities
{
    [Route("Countries")]
    public class CountryController : BaseApiController
    {
        protected readonly ICountryService _countryService;


        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;


        }

        [HttpGet]
        [Route(nameof(GetCountries))]
        public async Task<IActionResult> GetCountries()
        {
            var countriesResponse = await _countryService.GetCountries();
            return Ok(countriesResponse);
        }



    }
}