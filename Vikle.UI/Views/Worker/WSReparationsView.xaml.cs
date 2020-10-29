using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the worker side reparations view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WSReparationsView : ContentPage
    {
        public WSReparationsView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}