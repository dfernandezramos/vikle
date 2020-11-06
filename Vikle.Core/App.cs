using MvvmCross.ViewModels;

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
            // RegisterAppStart<TipViewModel>();
        }

        void RegisterServices()
        {
            // Mvx.IoCProvider.RegisterType<ICalculationService, CalculationService>();
        }
    }
}