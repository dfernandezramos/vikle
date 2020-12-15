// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Vikle.Core.Enums;
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Reparation Detail VM for the Worker side
    /// </summary>
    public class WSReparationDetailVM : WorkerBaseVM<Reparation>
    {
        private Reparation _model;
        private bool _newReparation;
        private IWSReparationDetailService _reparationDetailService;
        private string _detailError;
        private bool _showDetailError;

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

        /// <summary>
        /// Gets or sets a boolean indicating whether this vm is working with a new model or not
        /// </summary>
        public bool NewReparation
        {
            get => _newReparation;
            set
            {
                _newReparation = value;
                RaisePropertyChanged(() => NewReparation);
            }
        }

        /// <summary>
        /// Gets or sets the reparation date
        /// </summary>
        public DateTime ReparationDate
        {
            get => new DateTime(Model.Date).ToLocalTime();
            set
            {
                Model.Date = value.Ticks;
                RaisePropertyChanged(() => ReparationDate);
            }
        }

        /// <summary>
        /// Gets or sets the reparation type
        /// </summary>
        public ReparationType ReparationType
        {
            get => Model.Type;
            set
            {
                Model.Type = value;
                RaisePropertyChanged(() => ReparationType);
            }
        }
        
        /// <summary>
        /// Gets or sets the reparation status
        /// </summary>
        public ReparationStatus ReparationStatus
        {
            get => Model.Status;
            set
            {
                Model.Status = value;
                RaisePropertyChanged(() => ReparationStatus);
            }
        }


        public string PlateNumber
        {
            get => Model.PlateNumber;
            set
            {
                Model.PlateNumber = value;
                RaisePropertyChanged(() => PlateNumber);
            }
        }
        
        /// <summary>
        /// Gets or sets the detail error message.
        /// </summary>
        public string DetailError
        {
            get => _detailError;
            set
            {
                _detailError = value;
                RaisePropertyChanged(() => DetailError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the detail error message has to be shown or not.
        /// </summary>
        public bool ShowDetailError
        {
            get => _showDetailError;
            set
            {
                _showDetailError = value;
                RaisePropertyChanged(() => ShowDetailError);
            }
        }
        
        /// <summary>
        /// Gets or sets the cancel command
        /// </summary>
        public MvxAsyncCommand CancelCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the continue command
        /// </summary>
        public MvxAsyncCommand ContinueCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the contact command
        /// </summary>
        public MvxAsyncCommand ContactCommand { get; set; }
        
        public WSReparationDetailVM(IMvxNavigationService mvxNavigationService, IWSReparationDetailService reparationDetailService) : base(mvxNavigationService)
        {
            CancelCommand = new MvxAsyncCommand(Close);
            ContinueCommand = new MvxAsyncCommand(UpdateReparation);
            ContactCommand = new MvxAsyncCommand(ContactCustomer);
            _reparationDetailService = reparationDetailService;
        }

        public override void Prepare(Reparation reparation)
        {
            base.Prepare(reparation);

            Model = reparation;
            Model.WorkshopId = "1";
            NewReparation = string.IsNullOrEmpty(Model.PlateNumber);
        }
        
        async Task Close(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Close(this, cancellationToken);
        }

        async Task UpdateReparation(CancellationToken cancellationToken)
        {
            ShowDetailError = false;
            
            if (NewReparation && string.IsNullOrEmpty(PlateNumber))
            {
                DetailError = Strings.AllFieldsAreRequired;
                ShowDetailError = true;
                return;
            }
            
            Result result = await _reparationDetailService.UpdateReparation(Model);
            
            if (result.Error)
            {
                DetailError = result.Message;
                ShowDetailError = true;
            }
            else
            {
                await Close(cancellationToken);
            }
        }

        async Task ContactCustomer(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<ContactCustomerVM, string>(Model.PlateNumber, cancellationToken: cancellationToken);
        }
    }
}