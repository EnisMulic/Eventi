namespace Eventi.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            private const string EndpointBase = "Auth";
            public const string Register = Base + "/" + EndpointBase  + "/Register";
            public const string Login = Base + "/" + EndpointBase + "/Login";
            public const string Refresh = Base + "/" + EndpointBase + "/Refresh";
        }

        public static class User
        {
            private const string EndpointBase = "User";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
        }

        public static class City
        {
            private const string EndpointBase = "City";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
        }

        public static class Country
        {
            private const string EndpointBase = "Country";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
            public const string GetCity = Base + "/" + EndpointBase + "/{id}/" + "City";
        }

        public static class Venue
        {
            private const string EndpointBase = "Venue";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
        }

        public static class Performer
        {
            private const string EndpointBase = "Performer";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
        }

        public static class Event
        {
            private const string EndpointBase = "Performer";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
            public const string GetPerformer = Base + "/" + EndpointBase + "/{id}/" + "Performer";
            public const string PostPerformer = Base + "/" + EndpointBase + "/{eventId}/" + "Performer" + "/{performerId}";
            public const string DeletePerformer = Base + "/" + EndpointBase + "/{eventId}/" + "Performer" + "/{performerId}";
            public const string GetSponsor = Base + "/" + EndpointBase + "/{id}/" + "Sponsor";
            public const string PostSponsor = Base + "/" + EndpointBase + "/{id}/" + "Sponsor";
            public const string PutSponsor = Base + "/" + EndpointBase + "/{id}/" + "Sponsor" + "/{sponsorId}/";
            public const string DeleteSponsor = Base + "/" + EndpointBase + "/{id}/" + "Sponsor" + "/{sponsorId}/";
        }
        
        public static class Sponsor
        {
            private const string EndpointBase = "Performer";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
        }

        public static class Organizer
        {
            private const string EndpointBase = "Organizer";
            public const string Get = Base + "/" + EndpointBase;
            public const string GetById = Base + "/" + EndpointBase;
            public const string Post = Base + "/" + EndpointBase;
            public const string Put = Base + "/" + EndpointBase;
            public const string Delete = Base + "/" + EndpointBase;
            public const string GetEvent = Base + "/" + EndpointBase + "/{id}/" + "Event";
        }
    }
}
