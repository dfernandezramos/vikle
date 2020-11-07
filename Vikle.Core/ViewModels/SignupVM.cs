using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the signup viewmodel.
    /// </summary>
    public class SignupVM : MvxViewModel
    {

        readonly IMvxNavigationService _mvxNavigationService;

        /// <summary>
        /// Gets or sets the back navigation command
        /// </summary>
        public IMvxAsyncCommand BackNavigationCommand { get; }
        
        /// <summary>
        /// Gets or sets the signup command
        /// </summary>
        public IMvxAsyncCommand SignupCommand { get; }
        
        public SignupVM(IMvxNavigationService navigationService)
        {
            _mvxNavigationService = navigationService;

            BackNavigationCommand = new MvxAsyncCommand(BackNavigation);
            SignupCommand = new MvxAsyncCommand(Signup);
        }
        
        async Task BackNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Close(this, cancellationToken);
        }
        
        async Task Signup(CancellationToken cancellationToken)
        {
            // TODO: Pending implement signup logic
            
            var confirmationParams = new ConfirmationParams
            {
                Title = "Signup successful!",
                Subtitle = "We have sent you a confirmation e-mail but you can now log in"
            };
            
            await _mvxNavigationService.Navigate<ConfirmationVM, ConfirmationParams>(confirmationParams ,cancellationToken: cancellationToken);
        }
    }
}