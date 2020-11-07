using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the login viewmodel.
    /// </summary>
    public class LoginVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;

        /// <summary>
        /// Gets or sets the login command
        /// </summary>
        public IMvxAsyncCommand LoginCommand { get; }
        
        /// <summary>
        /// Gets or sets the signup navigation command
        /// </summary>
        public IMvxAsyncCommand SignupNavigateCommand { get; }

        /// <summary>
        /// Gets or sets the recover password command
        /// </summary>
        public IMvxAsyncCommand RecoverPasswordCommand { get; }

        public LoginVM(IMvxNavigationService navigationService)
        {
            _mvxNavigationService = navigationService;

            LoginCommand = new MvxAsyncCommand(Login);
            SignupNavigateCommand = new MvxAsyncCommand(SignupNavigation);
            RecoverPasswordCommand = new MvxAsyncCommand(RecoverPasswordNavigation);
        }

        async Task RecoverPasswordNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<RecoverPasswordVM>(cancellationToken: cancellationToken);
        }

        async Task Login(CancellationToken cancellationToken)
        {
            // TODO: Pending implement login logic
        }
        
        async Task SignupNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<SignupVM>(cancellationToken: cancellationToken);
        }
    }
}