// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the customer contact view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class ContactCustomerView : MvxContentPage<ContactCustomerVM>
    {
        public ContactCustomerView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            var emailTapGestureRecognizer = new TapGestureRecognizer();
            emailTapGestureRecognizer.Tapped += async (sender, args) => await ViewModel.SendEmailCommand.ExecuteAsync();
            EmailLabel.GestureRecognizers.Add(emailTapGestureRecognizer);
            
            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            CallButton.Command = ViewModel.CallCommand;
            PhoneLabel.BindingContext = ViewModel;
            PhoneLabel.SetBinding(Label.TextProperty, nameof(ViewModel.Phone));
            EmailLabel.BindingContext = ViewModel;
            EmailLabel.SetBinding(Label.TextProperty, nameof(ViewModel.Email));
            ErrorLabel.BindingContext = ViewModel;
            ErrorLabel.SetBinding(Label.IsVisibleProperty, nameof(ViewModel.ShowContactError));
            ErrorLabel.SetBinding(Label.TextProperty, nameof(ViewModel.ContactError));
            NameLabel.BindingContext = ViewModel;
            NameLabel.SetBinding(Label.TextProperty, nameof(ViewModel.Name));
            SurnameLabel.BindingContext = ViewModel;
            SurnameLabel.SetBinding(Label.TextProperty, nameof(ViewModel.SurName));
        }
    }
}