using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Label = Xamarin.Forms.Label;

namespace Vikle.UI.Views.Login
{
    /// <summary>
    /// This class contains the definition of the Sign Up view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : MvxContentPage<SignupVM>
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ConfirmButton.Command = ViewModel.SignupCommand;
            GoBackButton.Command = ViewModel.BackNavigationCommand;
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowSignupError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.SignupError));
            NameEntry.BindingContext = ViewModel.UserData;
            NameEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserData.Name));
            SurnameEntry.BindingContext = ViewModel.UserData;
            SurnameEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserData.Surname));
            PhoneEntry.BindingContext = ViewModel.UserData;
            PhoneEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserData.Phone));
            EmailEntry.BindingContext = ViewModel.UserData;
            EmailEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserData.Email));
            PasswordEntry.BindingContext = ViewModel.UserData;
            PasswordEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserData.Password));
            RepeatPasswordEntry.BindingContext = ViewModel.UserData;
            RepeatPasswordEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.UserData.RepeatedPassword));
        }
    }
}