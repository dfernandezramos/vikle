using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the client's vehicles view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiclesView : ContentPage
    {
        public VehiclesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}