using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Login.Welcome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeView : ContentPage
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