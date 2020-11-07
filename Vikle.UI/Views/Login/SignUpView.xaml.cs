using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms.Xaml;

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
        }
    }
}