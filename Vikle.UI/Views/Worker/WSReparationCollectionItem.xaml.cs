// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vikle.Core;
using Vikle.Core.Enums;
using Vikle.Core.Models;
using Vikle.UI.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the reparation item template for the CollectionView in the worker side.
    /// </summary>
    public partial class WSReparationCollectionItem
    {
        public WSReparationCollectionItem()
        {
            InitializeComponent();
            this.PropertyChanged += OnPropertyChanged;
            
            TopDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:dd}");
            BottomDateLabel.SetBinding(Label.TextProperty, "Date", BindingMode.Default, new LongToDateTimeConverter(), stringFormat: "{0:MMMM}");
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                var reparation = (Reparation)this.BindingContext;
                if (reparation == null)
                {
                    return;
                }

                switch (reparation.Status)
                {
                    case ReparationStatus.Pending:
                    case ReparationStatus.Rejected:
                        StatusColorView.Color = Color.Red;
                        break;
                    case ReparationStatus.Repairing:
                        StatusColorView.Color = (Color) Resources["BlueStrongColor"];
                        break;
                    case ReparationStatus.Repaired:
                        StatusColorView.Color = Color.Green;
                        break;
                }
            }
        }
    }
}