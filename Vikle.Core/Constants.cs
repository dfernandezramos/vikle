namespace Vikle.Core
{
    /// <summary>
    /// This static class contains the definition of all the constants used by the application. 
    /// </summary>
    public static class Constants
    {
#if DEBUG
        // Setup your machine local IP address in order to test it with emulators and also with a real device
        public const string API_BASE_URI = "http://192.168.0.150:32003";
#else
        public const string API_BASE_URI = "http://www.vikle.com";
#endif

        public const string SS_TOKEN = "Vikle_Token";
        public const string SS_USER_ID = "Vikle_UserId";
        public const string SS_WORKER = "Vikle_Worker";
    }
}