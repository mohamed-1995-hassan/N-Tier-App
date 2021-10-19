using BOL.Entities.ViewModel;
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
    public class UserOperation : IUserOperation
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserOperation(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public UserOperation()
        {

        }


        public async Task<IdentityResult> Create(RegisterBindingModel model)
        {
            var user = new ApplicationUser() { Email = model.Email, UserName = model.Name };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<SignInResult> Login(LoginBindingModel model)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            return signInResult;
        }

        public async Task<IdentityResult> AddRole(string role)
        {
          return  await _roleManager.CreateAsync(new IdentityRole(role));
        }
        
        public async Task<bool> AsignRole(string UserEmail,string Role) {

            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(UserEmail);
            if (applicationUser != null)
            {
                IdentityResult identityResult = await _userManager.AddToRoleAsync(applicationUser, Role);
                return identityResult.Succeeded;
            }
            else
                return false;
        }

        public async Task AddDefaultAdmin(RegisterBindingModel registerBindingModel) {

            var user = new ApplicationUser() { Email = registerBindingModel.Email, UserName = registerBindingModel.Name };
            IdentityResult result = await _userManager.CreateAsync(user, registerBindingModel.Password);
            if (result.Succeeded)
            {
               await AddRole("DefaultAdmin");
               await AsignRole(user.Email, "DefaultAdmin");
            }
        
        }

        public async Task<string> DeleteUser(string userID)
        {
            
            if (userID == null)
                return "ID is A Null Value";
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(userID);
            if (applicationUser == null)
                return "User Not Found in database";
            if (!await _userManager.IsInRoleAsync(applicationUser, "DefaultAdmin"))
            {
                await _userManager.DeleteAsync(applicationUser);
                return "user deleted successfully";
            }
            else
            {
                return "user can not be deleted";
            }
                   
        }

        public async Task<string> WalletOperation(string TransfareFromId, string TransfareToId, double BalanceAmount)
        {
            ApplicationUser FromUser=null;
            ApplicationUser ToUser=null;
            if(TransfareFromId!=null&&TransfareToId!=null)
            {
                FromUser = await _userManager.FindByIdAsync(TransfareFromId);
                ToUser = await _userManager.FindByIdAsync(TransfareToId);

                if(FromUser!=null&&ToUser!=null)
                {
                    if (FromUser.Balance >= BalanceAmount)
                    {
                        FromUser.Balance -= BalanceAmount;
                        ToUser.Balance += BalanceAmount;
                        await _userManager.UpdateAsync(FromUser);
                        await _userManager.UpdateAsync(ToUser);
                        return "User Added Successfully";
                    }
                    else
                    {
                        return "your balance is less than the amount";
                    }
                }
                else {
                    return "no Users With This Credintials";
                }
            }
            else
            {
                return "Errore in the ID’s";
            }
        } 
    }
}
