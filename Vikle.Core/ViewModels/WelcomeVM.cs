using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Welcome viewmodel
    /// </summary>
    public class WelcomeVM : MvxViewModel
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
        
        public WelcomeVM(IMvxNavigationService navigationService)
        {
            _mvxNavigationService = navigationService;

            LoginNavigateCommand = new MvxAsyncCommand(LoginNavigation);
            SignupNavigateCommand = new MvxAsyncCommand(SignupNavigation);
        }
        
        async Task LoginNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<LoginVM>(cancellationToken: cancellationToken);
        }
        
        async Task SignupNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<SignupVM>(cancellationToken: cancellationToken);
        }
    }
}