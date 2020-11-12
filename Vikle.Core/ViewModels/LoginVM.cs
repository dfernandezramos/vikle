using System;
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
    /// This class contains the implementation of the login viewmodel.
    /// </summary>
    public class LoginVM : MvxViewModel
    {
        readonly IMvxNavigationService _mvxNavigationService;
        readonly ILoginService _loginService;
        private bool _showLoginError;
        private string _loginError;

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

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets the login error message.
        /// </summary>
        public string LoginError
        {
            get => _loginError;
            set
            {
                _loginError = value;
                RaisePropertyChanged(() => LoginError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the login error message has to be shown or not.
        /// </summary>
        public bool ShowLoginError
        {
            get => _showLoginError;
            set
            {
                _showLoginError = value;
                RaisePropertyChanged(() => ShowLoginError);
            }
        }

        public LoginVM(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _mvxNavigationService = navigationService;
            _loginService = loginService;

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
            ShowLoginError = false;
            LoginResult result = await _loginService.Login(UserName, UserPassword);

            if (result.Error)
            {
                ShowLoginError = true;
                LoginError = result.Message;
            }
            else
            {
                if (result.Worker)
                {
                    await _mvxNavigationService.Navigate<WorkerRootVM>(cancellationToken: cancellationToken);
                }
                else
                {
                    await _mvxNavigationService.Navigate<ClientRootVM>(cancellationToken: cancellationToken);
                }
            }
        }
        
        async Task SignupNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<SignupVM>(cancellationToken: cancellationToken);
        }
    }
}