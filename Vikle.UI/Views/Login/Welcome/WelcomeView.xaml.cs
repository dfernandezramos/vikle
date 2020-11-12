using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;

namespace Vikle.UI.Views.Login.Welcome
{
    /// <summary>
    /// This class contains the definition of the Welcome view.
    /// </summary>
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = true)]
    public partial class WelcomeView : MvxContentPage<WelcomeVM>
    {
        public WelcomeView()
        {
            InitializeComponent();

            Carousel.ItemsSource = new View[]
            {
                new WelcomeVikleView(), 
                new WelcomeHistoryView(),
                new WelcomeReparationView(),
                new WelcomeStatusView(),
                new WelcomeDatesView()
            };
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            LoginButton.Command = ViewModel.LoginNavigateCommand;
            SignupButton.Command = ViewModel.SignupNavigateCommand;
        }
    }
}