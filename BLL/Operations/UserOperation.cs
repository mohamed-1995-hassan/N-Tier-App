using BOS.Entities.Models;
using BOS.Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operations
{
   public class UserOperation
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserOperation(UserManager<ApplicationUser> userManager) {
            
            _userManager = userManager;
        }

        public async Task<IdentityResult> Create(RegisterBindingModel model)
        {
            var user = new ApplicationUser() { Email = model.Email, UserName = model.Name };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
