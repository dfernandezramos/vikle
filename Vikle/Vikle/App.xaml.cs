using Vikle.UI.Views.Login.Welcome;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Vikle
{
    /// <summary>
    /// This class contains the Main App class for the application. It will initialize the main components of the
    /// application and handle the main events of the app.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
#if DEBUG
            HotReloader.Current.Run(this); 
#endif

            MainPage = new WelcomeView();
        }

        /// <summary>
        /// Handles the Start event of the application.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Handles the Sleep event of the application.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Handles the Resume event of the application
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}