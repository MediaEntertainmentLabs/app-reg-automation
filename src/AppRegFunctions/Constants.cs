namespace AppRegFunctions
{
    public static class Constants
    {
        public static class Auth
        {
            public static string AdminPolicy = "AdminPolicy";
            public static string ApproverPolicy = "ApproverPolicy";
            public static string UserPolicy = "UserPolicy";

            public static string[] RequiredScopes = new string[] { "user_impersonation" };
        }
    }
}
