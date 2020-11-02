using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the customer contact view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactCustomerView : ContentPage
    {
        public ContactCustomerView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}