// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.ViewModels;
using RestSharp;
using Vikle.Core.Interfaces;
using Vikle.Core.Services;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.Core
{
    /// <summary>
    /// This class contains the Main App class for the application. It will initialize the main components for the
    /// MVVMCross Framework.
    /// </summary>
    public class App : MvxApplication
    {
        private ISecureStorageService _secureStorageService;
        
        public override void Initialize()
        {
            // FIXME: This is a workaround for using the iOS emulator. iOS emulators need a keychain-access-groups key in
            // the entitlements.plist file in order to enable the keychain and then use the secure storage service. To be
            // able to use a custom entitlements.plist file you need a provisioning profile. For getting a provisioning
            // profile you need an Apple Developer program enrollment. For that you need to pay 100 euros per year.
            // https://docs.microsoft.com/en-us/xamarin/essentials/secure-storage?tabs=ios
#if DEBUG
            if (Device.RuntimePlatform == Device.iOS)
            {
                _secureStorageService = new DummySecureStorageService();
            }
            else
            {
                _secureStorageService = new SecureStorageService();
            }
#else
            _secureStorageService = new SecureStorageService();
#endif
            
            // Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
            RegisterServices();

            if (UserHasToken())
            {
                if (UserIsWorker())
                {
                    RegisterAppStart<WorkerRootVM>();
                }
                else
                {
                    RegisterAppStart<ClientRootVM>();
                }
            }
            else
            {
                RegisterAppStart<WelcomeVM>();
            }
        }
        
        bool UserHasToken()
        {
            var token = _secureStorageService.GetAsync(Constants.SS_TOKEN).Result;
            return !string.IsNullOrEmpty(token);
        }
        
        bool UserIsWorker()
        {
            var worker = _secureStorageService.GetAsync(Constants.SS_WORKER).Result;
            return !string.IsNullOrEmpty(worker);
        }

        void RegisterServices()
        {
            Mvx.IoCProvider.RegisterType<ILoginService, LoginService>();
            Mvx.IoCProvider.RegisterType<IRecoverPasswordService, RecoverPasswordService>();
            Mvx.IoCProvider.RegisterType<ISignupService, SignupService> ();
            Mvx.IoCProvider.RegisterType<IVehiclesService, VehiclesService> ();
            Mvx.IoCProvider.RegisterType<IVehicleDetailService, VehicleDetailService> ();
            Mvx.IoCProvider.RegisterType<IHistoryService, HistoryService> ();
            Mvx.IoCProvider.RegisterType<IDatesService, DatesService> ();
            Mvx.IoCProvider.RegisterType<IDateDetailsService, DateDetailsService> ();
            Mvx.IoCProvider.RegisterType<IWSReparationsService, WSReparationsService> ();
            Mvx.IoCProvider.RegisterType<IWSReparationDetailService, WSReparationDetailService> ();
            Mvx.IoCProvider.RegisterType<IContactCustomerService, ContactCustomerService> ();

            Mvx.IoCProvider.RegisterSingleton<IRestClient> (new RestClient(Constants.API_BASE_URI));
            Mvx.IoCProvider.RegisterSingleton<IApiClientService> (new ApiClientService ());
            Mvx.IoCProvider.RegisterSingleton<IXamarinEssentialsService> (new XamarinEssentialsService ());
            Mvx.IoCProvider.RegisterSingleton<ISecureStorageService> (_secureStorageService);
        }
    }
}