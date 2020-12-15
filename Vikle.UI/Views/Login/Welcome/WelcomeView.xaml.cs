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

namespace Vikle.UI.Views.Login.Welcome
{
    /// <summary>
    /// This class contains the definition of the Welcome view.
    /// </summary>
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = true)]
    public partial class WelcomeView : MvxContentPage<WelcomeVM>
    {
        public WelcomeView()
        {
            InitializeComponent();

            Carousel.ItemsSource = new View[]
            {
                new WelcomeVikleView(), 
                new WelcomeHistoryView(),
                new WelcomeReparationView(),
                new WelcomeStatusView(),
                new WelcomeDatesView()
            };
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            LoginButton.Command = ViewModel.LoginNavigateCommand;
            SignupButton.Command = ViewModel.SignupNavigateCommand;
        }
    }
}