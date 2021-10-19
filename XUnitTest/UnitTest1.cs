using BLL.Operations;
using BOL.Entities.ViewModel;
using BOS.Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {


        [Fact]
        public void testCreation_ShouldSuccess()
        {
            //Arrange
            RegisterBindingModel registerBindingModel = new RegisterBindingModel
            {
                Email = "uu@uuuu.com",
                Name = "Ahmed2277",
                Password = "123478N@nmm",
                ConfirmPassword = "123478N@nmm"
            };
            //Act
            Mock<IUserOperation> Mock = new Mock<IUserOperation>();
            Mock.Setup(userMgr => userMgr.Create(registerBindingModel)).ReturnsAsync(IdentityResult.Success);
        }

        [Fact]
        public void Login()
        {
            //Arrange
            LoginBindingModel loginBindingModel = new LoginBindingModel {
                Email = "s@suuu.com",
                Password = "123478N@n"
            };
            //Act
            Mock<IUserOperation> Mock = new Mock<IUserOperation>();
            Mock.Setup(userMgr => userMgr.Login(loginBindingModel)).ReturnsAsync(SignInResult.Success);
        }

        [Fact]
        public void Test_Role_Asigned()
        {
            //Arrange
            string userEmail = "Mohamed@gmail.com";
            string Role = "Admin";
            //Act
            Mock<IUserOperation> Mock = new Mock<IUserOperation>();
            Mock.Setup(op => op.AsignRole(userEmail, Role)).Returns(Task.FromResult(true));
        }
        [Fact]
        public void Test_AddRole_Success()
        {
            //Arrange
            string Role = "Admin";
            Mock<IUserOperation> Mock = new Mock <IUserOperation> ();
            //Act
            Mock.Setup(op => op.AddRole(Role)).Returns(Task.FromResult(IdentityResult.Success));
        }

        [Fact]
        public void AddDefaultAdmin_To_Success() {

            RegisterBindingModel registerBindingModel = new RegisterBindingModel
            {
                Email = "uu@uuuu.com",
                Name = "Ahmed2277",
                Password = "123478N@nmm",
                ConfirmPassword = "123478N@nmm"
            };
            Mock<IUserOperation> Mock = new Mock<IUserOperation>();
            Mock.Setup(op => op.AddDefaultAdmin(registerBindingModel)).Returns(Task.FromResult(IdentityResult.Success));

        }

        [Fact]
        public void DeleteUser_Success()
        {
            string ID = "214ebea3-aef8-4e48-a110-ed50f701dbc1";
            Mock<IUserOperation> Mock = new Mock<IUserOperation>();
            Mock.Setup(op => op.DeleteUser(ID)).Returns(Task.FromResult("user deleted successfully"));
        }

        [Fact]
        public void WalletOperation_To_Success()
        {
            string TransfareFromId= "214ebea3-aef8-4e48-a110-ed50f701dbc1";
            string TransfareToId= "9d5279fe-dea3-49b7-8040-9a364519bbc3";
            double BalanceAmount=200;
            Mock<IUserOperation> Mock = new Mock<IUserOperation>();
            Mock.Setup(op => op.WalletOperation(TransfareFromId, TransfareToId, BalanceAmount)).Returns(Task.FromResult("User Added Successfully"));

        }
    }
}
