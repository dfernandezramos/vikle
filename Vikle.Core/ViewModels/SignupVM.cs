using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the signup viewmodel.
    /// </summary>
    public class SignupVM : MvxViewModel
    {

        readonly IMvxNavigationService _mvxNavigationService;
        readonly ISignupService _signupService;
        private string _signupError;
        private bool _showSignupError;

        /// <summary>
        /// Gets or sets the back navigation command
        /// </summary>
        public IMvxAsyncCommand BackNavigationCommand { get; }
        
        /// <summary>
        /// Gets or sets the signup command
        /// </summary>
        public IMvxAsyncCommand SignupCommand { get; }
        
        /// <summary>
        /// Gets or sets the user signup data
        /// </summary>
        public SignupData UserData { get; set; }

        /// <summary>
        /// Gets or sets the recover error message.
        /// </summary>
        public string SignupError
        {
            get => _signupError;
            set
            {
                _signupError = value;
                RaisePropertyChanged(() => SignupError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the recover error message has to be shown or not.
        /// </summary>
        public bool ShowSignupError
        {
            get => _showSignupError;
            set
            {
                _showSignupError = value;
                RaisePropertyChanged(() => ShowSignupError);
            }
        }

        public SignupVM(IMvxNavigationService navigationService, ISignupService signupService)
        {
            _mvxNavigationService = navigationService;
            _signupService = signupService;

            BackNavigationCommand = new MvxAsyncCommand(BackNavigation);
            SignupCommand = new MvxAsyncCommand(Signup);
            UserData = new SignupData();
        }
        
        async Task BackNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Close(this, cancellationToken);
        }
        
        async Task Signup(CancellationToken cancellationToken)
        {
            ShowSignupError = false;
            Result result = await _signupService.SignUp(UserData);

            if (result.Error)
            {
                SignupError = result.Message;
                ShowSignupError = true;
            }
            else
            {
                var confirmationParams = new ConfirmationParams
                {
                    Title = Strings.SignupConfirmationViewTitle,
                    Subtitle = Strings.SignupConfirmationViewSubtitle
                };
                
                await _mvxNavigationService.Navigate<ConfirmationVM, ConfirmationParams>(confirmationParams ,cancellationToken: cancellationToken);
            }
        }
    }
}