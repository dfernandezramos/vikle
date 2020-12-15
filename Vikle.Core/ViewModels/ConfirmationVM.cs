// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    public class ConfirmationVM : MvxViewModel<ConfirmationParams>
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
        /// Gets or sets the title text of the view.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the subtitle text of the view.
        /// </summary>
        public string Subtitle { get; set; }

        public ConfirmationVM(IMvxNavigationService navigationService)
        {
            _mvxNavigationService = navigationService;

            LoginNavigateCommand = new MvxAsyncCommand(LoginNavigation);
            SignupNavigateCommand = new MvxAsyncCommand(SignupNavigation);
        }
        
        public override void Prepare(ConfirmationParams parameters)
        {
            Title = parameters.Title;
            Subtitle = parameters.Subtitle;
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