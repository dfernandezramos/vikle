// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using Vikle.Core;
using Vikle.Core.Enums;
using Xamarin.Forms;

namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the status bar view definition for the vehicle detail view.
    /// </summary>
    public partial class StatusBar
    {
        private ReparationStatus? _status;

        public static readonly BindableProperty StatusProperty =
            BindableProperty.Create (nameof(Status), typeof(ReparationStatus), typeof(StatusBar), null, BindingMode.OneWay, propertyChanged: StatusPropertyChanged);

        /// <summary>
        /// Gets or sets the reparation status to display it in the bar.
        /// </summary>
        public ReparationStatus? Status
        {
            get => _status;
            set
            {
                _status = value;
                ChangeStatus();
            }
        }

        public StatusBar()
        {
            InitializeComponent();
            PendingElement.Title = Strings.Pending;
            RepairingElement.Title = Strings.Repairing;
            RepairedElement.Title = Strings.Repaired;
        }
        
        void ChangeStatus()
        {
            if (Status == null)
            {
                return;
            }
                
            PendingElement.Active = false;
            RepairingElement.Active = false;
            RepairedElement.Active = false;

            switch (_status)
            {
                case ReparationStatus.Pending:
                    PendingElement.Active = true;
                    break;
                case ReparationStatus.Repairing:
                    RepairingElement.Active = true;
                    break;
                case ReparationStatus.Repaired:
                    RepairedElement.Active = true;
                    break;
            }
        }
        
        static void StatusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (StatusBar)bindable;
            control.Status = (ReparationStatus)newValue;
        }
    }
}