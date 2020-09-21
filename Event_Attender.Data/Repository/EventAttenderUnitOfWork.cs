using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Repository
{
    public class EventAttenderUnitOfWork
    {
        private readonly MojContext ctx;
        private EventAttenderRepository<Administrator> administratorRepository;
        private EventAttenderRepository<Grad> gradRepository;
        private EventAttenderRepository<Drzava> drzavaRepository;
        private EventAttenderRepository<Event> eventRepository;
        private EventAttenderRepository<Izvodjac> izvodjacRepository;
        private EventAttenderRepository<IzvodjacEvent> izvodjacEventRepository;
        private EventAttenderRepository<Karta> kartaRepository;
        private EventAttenderRepository<Korisnik> korisnikRepository;
        private EventAttenderRepository<Kupovina> kupovinaRepository;
        private EventAttenderRepository<KupovinaTip> kupovinaTipRepository;
        private EventAttenderRepository<Like> likeRepository;
        private EventAttenderRepository<LogPodaci> logPodaciRepository;
        private EventAttenderRepository<Organizator> organizatorRepository;
        private EventAttenderRepository<Osoba> osobaRepository;
        private EventAttenderRepository<ProdajaTip> prodajaTipRepository;
        private EventAttenderRepository<ProstorOdrzavanja> prostorOdrzavanjaRepository;
        private EventAttenderRepository<Radnik> radnikRepository;
        private EventAttenderRepository<RadnikEvent> radnikEventRepository;
        private EventAttenderRepository<Recenzija> recenzijaRepository;
        private EventAttenderRepository<Sjediste> sjedisteRepository;
        private EventAttenderRepository<Sponzor> sponzorRepository;
        private EventAttenderRepository<SponzorEvent> sponzorEventRepository;
        private EventAttenderRepository<ChatPoruke> chatPorukeRepository;
        
        public EventAttenderUnitOfWork(MojContext context)
        {
            ctx = context;
        }

        public EventAttenderRepository<Administrator> AdministratorRepository
        {
            get
            {

                if (administratorRepository == null)
                {
                    administratorRepository = new EventAttenderRepository<Administrator>(ctx);
                }
                return administratorRepository;
            }
        }

        public EventAttenderRepository<Grad> GradRepository
        {
            get
            {

                if (gradRepository == null)
                {
                    gradRepository = new EventAttenderRepository<Grad>(ctx);
                }
                return gradRepository;
            }
        }

        public EventAttenderRepository<Drzava> DrzavaRepository
        {
            get
            {

                if (drzavaRepository == null)
                {
                    drzavaRepository = new EventAttenderRepository<Drzava>(ctx);
                }
                return drzavaRepository;
            }
        }

        public EventAttenderRepository<Event> EventRepository
        {
            get
            {

                if (eventRepository == null)
                {
                    eventRepository = new EventAttenderRepository<Event>(ctx);
                }
                return eventRepository;
            }
        }

        public EventAttenderRepository<Izvodjac> IzvodjacRepository
        {
            get
            {

                if (izvodjacRepository == null)
                {
                    izvodjacRepository = new EventAttenderRepository<Izvodjac>(ctx);
                }
                return izvodjacRepository;
            }
        }

        public EventAttenderRepository<IzvodjacEvent> IzvodjacEventRepository
        {
            get
            {

                if (izvodjacEventRepository == null)
                {
                    izvodjacEventRepository = new EventAttenderRepository<IzvodjacEvent>(ctx);
                }
                return izvodjacEventRepository;
            }
        }

        public EventAttenderRepository<Karta> KartaRepository
        {
            get
            {

                if (kartaRepository == null)
                {
                    kartaRepository = new EventAttenderRepository<Karta>(ctx);
                }
                return kartaRepository;
            }
        }

        public EventAttenderRepository<Korisnik> KorisnikRepository
        {
            get
            {

                if (korisnikRepository == null)
                {
                    korisnikRepository = new EventAttenderRepository<Korisnik>(ctx);
                }
                return korisnikRepository;
            }
        }

        public EventAttenderRepository<Kupovina> KupovinaRepository
        {
            get
            {

                if (kupovinaRepository == null)
                {
                    kupovinaRepository = new EventAttenderRepository<Kupovina>(ctx);
                }
                return kupovinaRepository;
            }
        }

        public EventAttenderRepository<KupovinaTip> KupovinaTipRepository
        {
            get
            {

                if (kupovinaTipRepository == null)
                {
                    kupovinaTipRepository = new EventAttenderRepository<KupovinaTip>(ctx);
                }
                return kupovinaTipRepository;
            }
        }

        public EventAttenderRepository<Like> LikeRepository
        {
            get
            {

                if (likeRepository == null)
                {
                    likeRepository = new EventAttenderRepository<Like>(ctx);
                }
                return likeRepository;
            }
        }

        public EventAttenderRepository<LogPodaci> LogPodaciRepository
        {
            get
            {

                if (logPodaciRepository == null)
                {
                    logPodaciRepository = new EventAttenderRepository<LogPodaci>(ctx);
                }
                return logPodaciRepository;
            }
        }

        public EventAttenderRepository<Organizator> OrganizatorRepository
        {
            get
            {

                if (organizatorRepository == null)
                {
                    organizatorRepository = new EventAttenderRepository<Organizator>(ctx);
                }
                return organizatorRepository;
            }
        }

        public EventAttenderRepository<Osoba> OsobaRepository
        {
            get
            {

                if (osobaRepository == null)
                {
                    osobaRepository = new EventAttenderRepository<Osoba>(ctx);
                }
                return osobaRepository;
            }
        }

        public EventAttenderRepository<ProdajaTip> ProdajaTipRepository
        {
            get
            {

                if (prodajaTipRepository == null)
                {
                    prodajaTipRepository = new EventAttenderRepository<ProdajaTip>(ctx);
                }
                return prodajaTipRepository;
            }
        }

        public EventAttenderRepository<ProstorOdrzavanja> ProstorOdrzavanjaRepository
        {
            get
            {

                if (prostorOdrzavanjaRepository == null)
                {
                    prostorOdrzavanjaRepository = new EventAttenderRepository<ProstorOdrzavanja>(ctx);
                }
                return prostorOdrzavanjaRepository;
            }
        }

        public EventAttenderRepository<Radnik> RadnikRepository
        {
            get
            {

                if (radnikRepository == null)
                {
                    radnikRepository = new EventAttenderRepository<Radnik>(ctx);
                }
                return radnikRepository;
            }
        }

        public EventAttenderRepository<Recenzija> RecenzijaRepository
        {
            get
            {

                if (recenzijaRepository == null)
                {
                    recenzijaRepository = new EventAttenderRepository<Recenzija>(ctx);
                }
                return recenzijaRepository;
            }
        }

        public EventAttenderRepository<Sjediste> SjedisteRepository
        {
            get
            {

                if (sjedisteRepository == null)
                {
                    sjedisteRepository = new EventAttenderRepository<Sjediste>(ctx);
                }
                return sjedisteRepository;
            }
        }

        public EventAttenderRepository<RadnikEvent> RadnikEventRepository
        {
            get
            {

                if (radnikEventRepository == null)
                {
                    radnikEventRepository = new EventAttenderRepository<RadnikEvent>(ctx);
                }
                return radnikEventRepository;
            }
        }

        public EventAttenderRepository<Sponzor> SponzorRepository
        {
            get
            {

                if (sponzorRepository == null)
                {
                    sponzorRepository = new EventAttenderRepository<Sponzor>(ctx);
                }
                return sponzorRepository;
            }
        }

        public EventAttenderRepository<SponzorEvent> SponzorEventRepository
        {
            get
            {

                if (sponzorEventRepository == null)
                {
                    sponzorEventRepository = new EventAttenderRepository<SponzorEvent>(ctx);
                }
                return sponzorEventRepository;
            }
        }

        public EventAttenderRepository<ChatPoruke> ChatPorukeRepository
        {
            get
            {

                if (chatPorukeRepository == null)
                {
                    chatPorukeRepository = new EventAttenderRepository<ChatPoruke>(ctx);
                }
                return chatPorukeRepository;
            }
        }
    }
}
