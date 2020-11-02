using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the client dates view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatesView : ContentPage
    {
        public DatesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}