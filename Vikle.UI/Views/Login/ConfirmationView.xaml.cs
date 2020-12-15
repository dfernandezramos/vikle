// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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