using BLL.Operations;
using BOL.Entities.ViewModel;
using BOS.Entities.Models;
using BOS.Entities.ViewModel;
using DAL.BaseRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace N_Tier_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Account : ControllerBase
    {
        private readonly UserOperation _userOperation;
       public Account(UserOperation userOperation)
        {
            _userOperation = userOperation;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            IdentityResult result=new IdentityResult();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           // if (HttpContext.User.IsInRole("Admin"))
           // {

                 result = await _userOperation.Create(model);
           // }

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
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var signInResult = await _userOperation.Login(model);
            if (signInResult.Succeeded)
                return Ok("Success");
            else
                return BadRequest("Errors");
        }

        [Authorize(Roles = "Admin,DefaultAdmin")]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([Required] string Role)
        {
            if (ModelState.IsValid)
            {
               await _userOperation.AddRole(Role);
            }
            return BadRequest("Errors");
        }
       // [Authorize(Roles = "Admin,DefaultAdmin")]
        [HttpPost]
        [Route("AsignRole")]
        public async Task<IActionResult> AsignRole(string UserEmail, string Role)
        {
            bool result = await _userOperation.AsignRole(UserEmail, Role);
            if (result)
                return Ok();
            else
                return BadRequest("Failed To Add");
        }

        [HttpPost]
        [Route("AddDefaultAdmin")]
        // Secret Code For Create Super Admin is 123852@_mn
        public async Task<IActionResult> AddDefaultAdmin(string SecretCodeForCreation,RegisterBindingModel registerBindingModel)
        {
            if (SecretCodeForCreation == "123852@_mn") {
               await _userOperation.AddDefaultAdmin(registerBindingModel);
               return Ok();
            }
           return BadRequest("operation failed");

        }

        [HttpPost]
        [Authorize(Roles = "Admin,DefaultAdmin")]
        public async Task<IActionResult> RemoveUser(string userID)
        {
            return Content(await _userOperation.DeleteUser(userID));
        }
        [HttpPost]
        public async Task<IActionResult> TransfareBalance(string TransfareFromId,string TransfareToId,double BalanceAmount)
        {
            return Content(await _userOperation.WalletOperation(TransfareFromId, TransfareToId, BalanceAmount));
        }

        [HttpGet]
        public IActionResult index() {
            return Content("hello");
        }
    }
}
