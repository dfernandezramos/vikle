using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Login
{
    /// <summary>
    /// This class contains the definition of the login view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : MvxContentPage<LoginVM>
    {
        public LoginView()
        {
            InitializeComponent();
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            LoginButton.Command = ViewModel.LoginCommand;
            SignupButton.Command = ViewModel.SignupNavigateCommand;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.RecoverPasswordCommand.ExecuteAsync();
            ForgotPasswordLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}