namespace Eventi.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            public const string Register = Base + "/Account/Register";
            public const string Login = Base + "/Account/Login";
            public const string Refresh = Base + "/Account/Refresh";
        }

        public static class User
        {
            public const string Get = Base + "/User";
            public const string GetById = Base + "/User";
            public const string Post = Base + "/User";
            public const string Put = Base + "/User";
            public const string Delete = Base + "/User";
        }

        public static class City
        {
            public const string Get = Base + "/City";
            public const string GetById = Base + "/City";
            public const string Post = Base + "/City";
            public const string Put = Base + "/City";
            public const string Delete = Base + "/City";
        }
    }
}
