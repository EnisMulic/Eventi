using Event_Attender.Web.Areas.Administrator.Controllers;
using Event_Attender.Web.Areas.Administrator.Models;
using Event_Attender.Web.Areas.ModulGuest.Controllers;
using Event_Attender.Web.Areas.ModulKorisnik.Controllers;
using Event_Attender.Web.Areas.ModulKorisnik.Models;
using Event_Attender.Web.Areas.ModulRadnik.Controllers;
using Event_Attender.Web.Areas.ModulRadnik.Models;
using Event_Attender.Web.Controllers;
using Event_Attender.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestovi
{
    
    [TestClass]
    public class HomeControllerTest:TestingDataBase
    {
        [TestMethod]
        public void IndexTestPrijePokretanjaZakomentiratiSetLogiraniKorisnik()
        {
            //prije pokretanja zakomentarisati liniju koda  HttpContext.SetLogiraniUser(null); u HomeController, akcija Index
            var hc = new Event_Attender.Web.Controllers.HomeController(ctx);
            var result = hc.Index() as ViewResult;

            var model = result.Model as Event_Attender.Web.ViewModels.PretragaEventaVM;

            var eventi = model.Eventi as List<Event_Attender.Web.ViewModels.PretragaEventaVM.Rows>;

            Assert.AreEqual(3, eventi.Count());
        }

        [TestMethod]
        public void PrivacyTest()
        {
            var hc = new Event_Attender.Web.Controllers.HomeController(ctx);
            var result = hc.Privacy() as ViewResult;
            Assert.IsNotNull(result);
        }
    }

    [TestClass]
    public class KorisnikControllerTest:TestingDataBase
    {
        [TestMethod]
        public void PrikazEvenataTest()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PrikazEvenata() as List<Event_Attender.Web.Areas.ModulKorisnik.Models.PretragaEventaVM.Rows>;

            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void PretragaPoKategorijiTestVraca1EventKategorijeMuzika()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PretragaPoKategoriji(Kategorija.Muzika);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void PretragaPoKategorijiTestVraca1EventKategorijeSport()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PretragaPoKategoriji(Kategorija.Sport);

            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void PretragaPoKategorijiTestVraca1EventKategorijeKultura()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PretragaPoKategoriji(Kategorija.Kultura);

            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void PretragaPoNazivuLokacijiTestVraca1EventSaIstimNazivom()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PretragaPoNazivuLokaciji("EventTest2");

            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void PretragaPoNazivuLokacijiTestVraca3EventaSaIstomLokacijom()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PretragaPoNazivuLokaciji("Grad1");

            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public void PretragaPoNazivuLokacijiTestVraca0EventaSaIstomLokacijom()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.PretragaPoNazivuLokaciji("Sarajevo");

            Assert.AreEqual(0, result.Count());
            
        }
        [TestMethod]
        public void OEventuTestProvjeraDaLiVracaTacanEvent()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.OEventu(2, 1) as ViewResult;

            var model = result.Model as EventKorisnikVM;

            Assert.AreEqual("EventTest2", model.Naziv);
        }
        [TestMethod]
        public void OEventuTestProvjeraDaLiVracaTacnogKorisnika()
        {
            var controller = new KorisnikController(ctx);

            var result = controller.OEventu(2, 1) as ViewResult;

            var model = result.Model as EventKorisnikVM;

            Assert.AreEqual("Ime", model.KorisnikIme);
        }
        [TestMethod]
        public void KupovinaKarteTestPoslano0ZaIdEventa()
        {
            var controller = new KorisnikController(ctx);
            var result = controller.KupiKartu(0, 1) as PartialViewResult;

            Assert.AreEqual(result.ViewName, "NemKupovina");

        }
        [TestMethod]
        public void KupovinaKarteTestPoslano0ZaIdKorisnika()
        {
            var controller = new KorisnikController(ctx);
            var result = controller.KupiKartu(2, 0) as PartialViewResult;

            Assert.AreEqual(result.ViewName, "NemKupovina");

        }
        
        
    }
    [TestClass]
    public class GuestControllerTest : TestingDataBase
    {
        [TestMethod]
        public void PretragaPoLokacijiTestVraca3EventSaIstomLokacijom()
        {
            var controller = new GuestController(ctx);
            var result=controller.PretraziPoLokaciji("Grad1") as ViewResult;
            var model = result.Model as Event_Attender.Web.Areas.ModulGuest.Models.PretragaEventaVM;
            var eventi = model.Eventi as List<Event_Attender.Web.Areas.ModulGuest.Models.PretragaEventaVM.Rows>;

            Assert.AreEqual(3, eventi.Count());

        }

        [TestMethod]
        public void PretragaPoLokacijiTestVraca1EventSaIstimNazivom()
        {
            var controller = new GuestController(ctx);
            var result = controller.PretraziPoNazivu("EventTest3") as ViewResult;
            var model = result.Model as Event_Attender.Web.Areas.ModulGuest.Models.PretragaEventaVM;
            var eventi = model.Eventi as List<Event_Attender.Web.Areas.ModulGuest.Models.PretragaEventaVM.Rows>;

            Assert.AreEqual(1, eventi.Count());

        }

    }
    [TestClass]
    public class RadnikControllerTest : TestingDataBase
    {
        [TestMethod]
        public void DetaljiTest()
        {
            var controller = new RadnikController(ctx);

            var result = controller.Detalji(2) as ViewResult;
            var model = result.Model as EventDetaljiVM;
            var tipoviProdaje = model.TipoviProdaje as List<EventDetaljiVM.TipProdaje>;

            Assert.AreEqual(1, tipoviProdaje.Count());

        }
    }
    [TestClass]
    public class PosjeceniEventiControllerTest : TestingDataBase
    {
        [TestMethod]
        public void RecenzijaTestVracaPostojecuRecenzijuKojaImaKomentar_komentar()
        {
            var controller = new PosjeceniEventiController(ctx);

            var result = controller.Recenzija(1, 1) as PartialViewResult;

            var model = result.Model as RecenzijaVM;

            Assert.AreEqual(3, model.Ocjena);
        }
    }
}
