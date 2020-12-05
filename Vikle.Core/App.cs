using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.ViewModels;
using RestSharp;
using Vikle.Core.Interfaces;
using Vikle.Core.Services;
using Vikle.Core.ViewModels;

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
            _secureStorageService = new SecureStorageService();
            
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