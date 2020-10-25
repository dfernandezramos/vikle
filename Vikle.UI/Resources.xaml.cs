using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI
{
    /// <summary>
    /// This class contains the resources dictionary to use it in the Vikle.UI views.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resources : ResourceDictionary
    {
        public Resources()
        {
            InitializeComponent();
        }
    }
}