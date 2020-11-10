namespace Vikle.Core
{
    /// <summary>
    /// This static class contains the definition of all the constants used by the application. 
    /// </summary>
    public static class Constants
    {
#if DEBUG
        public const string API_BASE_URI = "http://localhost:8080";
#else
        public const string API_BASE_URI = "http://www.vikle.com";
#endif

        public const string SS_TOKEN = "Vikle_Token";
        public const string SS_USER_ID = "Vikle_UserId";
        public const string SS_WORKER = "Vikle_Worker";
    }
}