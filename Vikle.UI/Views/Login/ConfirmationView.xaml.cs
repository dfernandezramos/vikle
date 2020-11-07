using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Login
{
    /// <summary>
    /// This class implements the confirmation view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationView : MvxContentPage<ConfirmationVM>
    {
        public ConfirmationView()
        {
            InitializeComponent();
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            LoginButton.Command = ViewModel.LoginNavigateCommand;
            SignupButton.Command = ViewModel.SignupNavigateCommand;
            TitleLabel.Text = ViewModel.Title;
            SubtitleLabel.Text = ViewModel.Subtitle;
        }
    }
}