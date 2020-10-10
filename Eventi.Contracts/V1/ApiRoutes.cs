namespace Eventi.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            public const string RegisterClient = Base + "/Auth/Register/Client";
            public const string RegisterAdministrator = Base + "/Auth/Register/Administrator";
            public const string RegisterOrganizer = Base + "/Auth/Register/Organizer";
            public const string Login = Base + "/Auth/Login";
            public const string Refresh = Base + "/Auth/Refresh";
            public const string Get = Base + "/Auth/Account/{id}";
        }

        public static class Client
        {
            public const string Get = Base + "/Client";
            public const string GetById = Base + "/Client/{id}";
            public const string Post = Base + "/Client";
            public const string Put = Base + "/Client/{id}";
            public const string Delete = Base + "/Client/{id}";
        }

        public static class City
        {
            public const string Get = Base + "/City";
            public const string GetById = Base + "/City/{id}";
            public const string Post = Base + "/City";
            public const string Put = Base + "/City/{id}";
            public const string Delete = Base + "/City/{id}";
            public const string GetVenue = Base + "/City/{id}/Venue";
            public const string GetEvent = Base + "/City/{id}/Event";
        }

        public static class Country
        {
            public const string Get = Base + "/Country";
            public const string GetById = Base + "/Country/{id}";
            public const string Post = Base + "/Country";
            public const string Put = Base + "/Country/{id}";
            public const string Delete = Base + "/Country/{id}";
            public const string GetCity = Base + "/Country/{id}/City";
        }

        public static class Venue
        {
            public const string Get = Base + "/Venue";
            public const string GetById = Base + "/Venue/{id}";
            public const string Post = Base + "/Venue";
            public const string Put = Base + "/Venue/{id}";
            public const string Delete = Base + "/Venue/{id}";
            public const string GetEvent = Base + "/Venue/{id}/Event";
        }

        public static class Performer
        {
            public const string Get = Base + "/Performer";
            public const string GetById = Base + "/Performer/{id}";
            public const string Post = Base + "/Performer";
            public const string Put = Base + "/Performer/{id}";
            public const string Delete = Base + "/Performer/{id}";
        }

        public static class Event
        {
            public const string Get = Base + "/Event";
            public const string GetById = Base + "/Event/{id}";
            public const string Post = Base + "/Event";
            public const string Put = Base + "/Event/{id}";
            public const string Delete = Base + "/Event/{id}";
            public const string GetPerformer = Base + "/Event/{id}/Performer";
            public const string PostPerformer = Base + "/Event/{eventId}/Performer/{performerId}";
            public const string DeletePerformer = Base + "/Event/{eventId}/Performer/{performerId}";
            public const string GetSponsor = Base + "/Event/{id}/Sponsor";
            public const string PostSponsor = Base + "/Event/{id}/Sponsor";
            public const string PutSponsor = Base + "/Event/{id}/Sponsor/{sponsorId}/";
            public const string DeleteSponsor = Base + "/Event/{id}/Sponsor/{sponsorId}/";
        }
        
        public static class Sponsor
        {
            public const string Get = Base + "/Sponsor";
            public const string GetById = Base + "/Sponsor/{id}";
            public const string Post = Base + "/Sponsor";
            public const string Put = Base + "/Sponsor/{id}";
            public const string Delete = Base + "/Sponsor/{id}";
        }

        public static class Organizer
        {
            public const string Get = Base + "/Organizer";
            public const string GetById = Base + "/Organizer/{id}";
            public const string Post = Base + "/Organizer";
            public const string Put = Base + "/Organizer/{id}";
            public const string Delete = Base + "/Organizer/{id}";
            public const string GetEvent = Base + "/Organizer/{id}/Event";
        }
    }
}
