using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Login.Welcome
{
    /// <summary>
    /// This class contains the definition of the Welcome view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeView : MvxContentPage<WelcomeVM>
    {
        private View[] _views;

        public WelcomeView()
        {
            InitializeComponent();
			
            _views = new View[]
            {
                new WelcomeVikleView(), 
                new WelcomeHistoryView(),
                new WelcomeReparationView(),
                new WelcomeStatusView(),
                new WelcomeDatesView()
            };

            Carousel.ItemsSource = _views;
        }
    }
}