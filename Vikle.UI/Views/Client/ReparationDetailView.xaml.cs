// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core;
using Vikle.Core.ViewModels;
using Vikle.UI.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the client reparation detail view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class ReparationDetailView : MvxContentPage<ReparationDetailVM>
    {
        public ReparationDetailView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
            PlateNumberLabel.Text = ViewModel.Model.PlateNumber;
            DateLabel.Text = $"{new DateTime(ViewModel.Model.Date).ToLocalTime():d MMM yyyy}";
            OilFilterElement.Enabled = ViewModel.Model.OilFilter;
            GasFilterElement.Enabled = ViewModel.Model.GasFilter;
            AirFilterElement.Enabled = ViewModel.Model.AirFilter;
            LiquidsElement.Enabled = ViewModel.Model.Liquids;
            ITVElement.Enabled = ViewModel.Model.ITV;
            TBDSElement.Enabled = ViewModel.Model.TBDS;
            SetDetails();
        }

        void SetDetails()
        {
            if (ViewModel.Model?.Details == null)
            {
                return;
            }

            foreach (var detail in ViewModel.Model.Details)
            {
                var label = new Label {
                    Text = $"- {detail}",
                    Padding = new Thickness(0,0,0,10),
                    TextColor = (Color)Resources["BlueStrongColor"],
                    FontSize = 18
                };
                Details.Children.Add(label);
            }
        }
    }
}