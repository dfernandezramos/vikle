using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the reparation detail view for the worker side.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WSReparationDetailView : ContentPage
    {
        public WSReparationDetailView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}