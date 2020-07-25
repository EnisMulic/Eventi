using Event_Attender.Web.Areas.Administrator.Models;
using Event_Attender.Web.Areas.Administrator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestovi.Areas.Administrator.HomeController
{
    [TestClass]
    public class AdministratorHomeControllerTest : TestingDataBase
    {
        [TestMethod]
        public void BrojEventTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.EventList() as ViewResult;
            var model = result.Model as List<EventVM>;


            Assert.AreEqual(4, model.Count());
        }

        [TestMethod]
        public void BrojOrganizatorTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.OrganizatorList() as ViewResult;
            var model = result.Model as List<OrganizatorVM>;


            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void BrojKorisnikaTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.KorisnikList() as ViewResult;
            var model = result.Model as List<KorisnikVM>;


            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void BrojIzvodjacTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.IzvodjacList() as ViewResult;
            var model = result.Model as List<IzvodjacVM>;


            Assert.AreEqual(5, model.Count());
        }

        [TestMethod]
        public void BrojRadnikTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.RadnikList() as ViewResult;
            var model = result.Model as List<RadnikVM>;


            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void BrojProstorTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.ProstorList() as ViewResult;
            var model = result.Model as List<ProstorOdrzavanjaVM>;


            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void BrojGradTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.GradList() as ViewResult;
            var model = result.Model as List<GradVM>;


            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void BrojDrzavaTest()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.DrzavaList() as ViewResult;
            var model = result.Model as List<DrzavaVM>;


            Assert.AreEqual(1, model.Count());
        }

        [TestMethod]
        public void TestDodajEventView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.EventDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajKorisnikView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.KorisnikDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajOrganizatorView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.OrganizatorDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajGradView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.GradDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajRadnikView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.RadnikDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajDrzavaView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.DrzavaDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajProstorView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.ProstorDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajIzvodjacView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.IzvodjacDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestDodajSponzorView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.SponzorDodaj();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestEventInfoView()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.EventInfo(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void TestEventInfo()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller.EventInfo(1) as ViewResult;
            var model = result.Model as EventVM;

            Assert.AreEqual("EventTest1", model.Naziv);
        }

        [TestMethod]
        public void AdminSidebarPartialViewTests()
        {
            var controller = new Event_Attender.Web.Areas.Administrator.Controllers.HomeController(ctx);
            var result = controller._AdminSidebar();

            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

    }
}
