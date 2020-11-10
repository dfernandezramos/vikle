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
            
            Mvx.IoCProvider.RegisterSingleton<IRestClient> (new RestClient(Constants.API_BASE_URI));
            Mvx.IoCProvider.RegisterSingleton<IApiClientService> (new ApiClientService ());
            Mvx.IoCProvider.RegisterSingleton<ISecureStorageService> (_secureStorageService);
        }
    }
}