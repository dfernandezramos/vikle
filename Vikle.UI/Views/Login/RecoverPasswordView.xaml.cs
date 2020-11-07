using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Login
{
    /// <summary>
    /// This class contains the definition of the recover password view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecoverPasswordView : MvxContentPage<RecoverPasswordVM>
    {
        public RecoverPasswordView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ContinueButton.Command = ViewModel.RecoverPasswordCommand;
            LoginButton.Command = ViewModel.LoginNavigateCommand;
            SignupButton.Command = ViewModel.SignupNavigateCommand;
        }
    }
}