using BLL.Operations;
using BOS.Entities.Models;
using BOS.Entities.ViewModel;
using DAL.BaseRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N_Tier_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Account : ControllerBase
    {
        private readonly UserOperation _userOperation;
        private readonly CountryOperation _countryOperation;

       public Account(UserOperation userOperation, CountryOperation countryOperation)
        {
            _userOperation = userOperation;
            _countryOperation = countryOperation;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result =await _userOperation.Create(model);

            if (result.Succeeded)
            {
                return Ok("Added");
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("createCountry")]
        public IActionResult createCountry([FromBody] Country country)
        {
            _countryOperation.Create(country);
            return Ok();
        }

        [HttpGet]
        public IActionResult index() {
            return Content("hello");
        }
    }
}
