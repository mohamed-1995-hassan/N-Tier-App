using BOL.Entities.ViewModel;
using BOS.Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operations
{
  public  interface IUserOperation
       {
         Task<IdentityResult> Create(RegisterBindingModel model);
         Task<SignInResult> Login(LoginBindingModel model);
         Task<IdentityResult> AddRole(string role);
         Task<bool> AsignRole(string UserEmail, string Role);
         Task AddDefaultAdmin(RegisterBindingModel registerBindingModel);
         Task<string> DeleteUser(string userID);
         Task<string> WalletOperation(string TransfareFromId, string TransfareToId, double BalanceAmount);
       }
}
