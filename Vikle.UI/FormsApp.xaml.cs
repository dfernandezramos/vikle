using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI
{
    /// <summary>
    /// This class contains the Main App class for the application. It will initialize the main components of the
    /// application and handle the main events of the app.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormsApp : Application
    {
        public FormsApp()
        {
            InitializeComponent();
        }
    }
}