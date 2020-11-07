using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the recover password viewmodel.
    /// </summary>
    public class RecoverPasswordVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;

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
        
        public RecoverPasswordVM(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            
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
            // TODO: Implement recover password logic

            var confirmationParams = new ConfirmationParams
            {
                Title = "Reset successful!",
                Subtitle = "If the provided address exists, we will send you a password recover e-mail"
            };
            
            await _mvxNavigationService.Navigate<ConfirmationVM, ConfirmationParams>(confirmationParams ,cancellationToken: cancellationToken);
        }
    }
}