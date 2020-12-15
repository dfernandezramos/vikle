// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".

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