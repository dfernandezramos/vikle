using MvvmCross.Binding.BindingContext;
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
            UsernameEntry.BindingContext = ViewModel;
            UsernameEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserName));
            UserPasswordEntry.BindingContext = ViewModel;
            UserPasswordEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserPassword));
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowLoginError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.LoginError));

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.RecoverPasswordCommand.ExecuteAsync();
            ForgotPasswordLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}