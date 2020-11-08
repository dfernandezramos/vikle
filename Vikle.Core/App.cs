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
        public override void Initialize()
        {
            RegisterServices();
            RegisterAppStart<WelcomeVM>();
        }

        void RegisterServices()
        {
            Mvx.IoCProvider.RegisterType<ILoginService, LoginService>();
            Mvx.IoCProvider.RegisterType<IRecoverPasswordService, RecoverPasswordService>();
            Mvx.IoCProvider.RegisterType<ISignupService, SignupService> ();
            
            Mvx.IoCProvider.RegisterSingleton<IRestClient> (new RestClient(Constants.API_BASE_URI));
            Mvx.IoCProvider.RegisterSingleton<IApiClientService> (new ApiClientService ());
            Mvx.IoCProvider.RegisterSingleton<ISecureStorageService> (new SecureStorageService());
        }
    }
}