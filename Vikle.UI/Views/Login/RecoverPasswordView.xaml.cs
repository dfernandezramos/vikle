// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Label = Xamarin.Forms.Label;

namespace Vikle.UI.Views.Login
{
    /// <summary>
    /// This class contains the definition of the recover password view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecoverPasswordView : MvxContentPage<RecoverPasswordVM>
    {
        public RecoverPasswordView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ContinueButton.Command = ViewModel.RecoverPasswordCommand;
            LoginButton.Command = ViewModel.LoginNavigateCommand;
            SignupButton.Command = ViewModel.SignupNavigateCommand;
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowRecoverError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.RecoverError));
            EmailEntry.BindingContext = ViewModel;
            EmailEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.Email));
        }
    }
}