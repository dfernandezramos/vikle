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
    /// This class implements the recover password viewmodel.
    /// </summary>
    public class RecoverPasswordVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        readonly IRecoverPasswordService _recoverPasswordService;
        private string _recoverError;
        private string _email;
        private bool _showRecoverError;

        /// <summary>
        /// Gets or sets the login navigation command
        /// </summary>
        public IMvxAsyncCommand LoginNavigateCommand { get; }
        
        /// <summary>
        /// Gets or sets the signup navigation command
        /// </summary>
        public IMvxAsyncCommand SignupNavigateCommand { get; }
        
        /// <summary>
        /// Gets or sets the recover password command
        /// </summary>
        public IMvxAsyncCommand RecoverPasswordCommand { get; }

        /// <summary>
        /// Gets or sets the email the user wants to recover the password from
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        /// <summary>
        /// Gets or sets the recover error message.
        /// </summary>
        public string RecoverError
        {
            get => _recoverError;
            set
            {
                _recoverError = value;
                RaisePropertyChanged(() => RecoverError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the recover error message has to be shown or not.
        /// </summary>
        public bool ShowRecoverError
        {
            get => _showRecoverError;
            set
            {
                _showRecoverError = value;
                RaisePropertyChanged(() => ShowRecoverError);
            }
        }

        public RecoverPasswordVM(IMvxNavigationService mvxNavigationService, IRecoverPasswordService recoverPasswordService)
        {
            _mvxNavigationService = mvxNavigationService;
            _recoverPasswordService = recoverPasswordService;
            
            LoginNavigateCommand = new MvxAsyncCommand(LoginNavigation);
            SignupNavigateCommand = new MvxAsyncCommand(SignupNavigation);
            RecoverPasswordCommand = new MvxAsyncCommand(RecoverPassword);
        }
        
        async Task LoginNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<LoginVM>(cancellationToken: cancellationToken);
        }
        
        async Task SignupNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<SignupVM>(cancellationToken: cancellationToken);
        }

        async Task RecoverPassword(CancellationToken cancellationToken)
        {
            ShowRecoverError = false;
            Result result = _recoverPasswordService.RecoverPassword(Email);

            if (result.Error)
            {
                RecoverError = result.Message;
                ShowRecoverError = true;
            }
            else
            {
                var confirmationParams = new ConfirmationParams
                {
                    Title = Strings.ResetConfirmationViewTitle,
                    Subtitle = Strings.ResetConfirmationViewSubtitle
                };
            
                await _mvxNavigationService.Navigate<ConfirmationVM, ConfirmationParams>(confirmationParams ,cancellationToken: cancellationToken);
            }
        }
    }
}