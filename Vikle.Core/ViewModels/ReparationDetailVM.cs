// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using MvvmCross.Navigation;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the reparation detail viewmodel for the client side
    /// </summary>
    public class ReparationDetailVM : ClientBaseVM<Reparation>
    {
        private Reparation _model;

        /// <summary>
        /// Gets or sets the vehicle data model.
        /// </summary>
        public Reparation Model
        {
            get => _model;
            set
            {
                _model = value;
                RaiseAllPropertiesChanged();
                RaisePropertyChanged(() => Model);
            }
        }

        public ReparationDetailVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
        }

        public override void Prepare(Reparation reparation)
        {
            base.Prepare(reparation);

            Model = reparation;
        }
    }
}