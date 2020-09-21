using Event_Attender.Web.Areas.Administrator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestovi.Areas.Administrator.AdministratorController
{
    [TestClass]
    public class AdministratorControllerTest : TestingDataBase
    {
        [TestMethod]
        public void AdminProfilUsernameTest()
        {

            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.AdminProfil(1) as ViewResult;
            var model = result.Model as AdministratorVM;
            Assert.AreEqual("User2", model.Username);
        }

        [TestMethod]
        public void AdminProfilViewOnUrediTest()
        {

            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.AdminProfil(1) as ViewResult;


            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestIsOldPassworde_Result_True()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.IsOldPassword("password1", 1);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestIsOldPassworde_Result_False()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.IsOldPassword("Password2", 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestMatchNewPassworde_Result_True()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.MatchNewPassword("Password2", "Password2");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMatchNewPassword_Result_False()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.MatchNewPassword("Password2", "Password1");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsUsernameUnique_Result_True()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.IsUsernameUnique(Guid.NewGuid().ToString(), 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsUsernameUnique_Result_False()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.IsUsernameUnique("User1", 2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsEmailUnique_Result_True()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.IsEmailUnique(Guid.NewGuid().ToString(), 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsEmailUnique_Result_False()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.AdministratorController(ctx);
            var result = controller.IsEmailUnique("azra.becirevic1998@gmail.com", 2);

            Assert.IsFalse(result);
        }
    }
}
