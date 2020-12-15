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
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the reparations viewmodel for the client side
    /// </summary>
    public class ReparationsVM : ClientBaseVM<string>
    {
        readonly IHistoryService _historyService;
        private string _historyError;
        private bool _showHistoryError;
        private MvxObservableCollection<Reparation> _reparations;

        /// <summary>
        /// Gets or sets the plate number of the vehicle.
        /// </summary>
        public string PlateNumber { get; private set; } 
        
        /// <summary>
        /// Gets or sets the history error message.
        /// </summary>
        public string HistoryError
        {
            get => _historyError;
            set
            {
                _historyError = value;
                RaisePropertyChanged(() => HistoryError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the history error message has to be shown or not.
        /// </summary>
        public bool ShowHistoryError
        {
            get => _showHistoryError;
            set
            {
                _showHistoryError = value;
                RaisePropertyChanged(() => ShowHistoryError);
            }
        }
        
        /// <summary>
        /// Gets or sets the user vehicles.
        /// </summary>
        public MvxObservableCollection<Reparation> Reparations
        {
            get => _reparations;
            set
            {
                _reparations = value;
                RaisePropertyChanged(() => Reparations);
            }
        }
        
        /// <summary>
        /// Gets or sets the reparation detail navigation command
        /// </summary>
        public MvxAsyncCommand<Reparation> ReparationDetailNavigationCommand { get; set; }
        
        public ReparationsVM(IMvxNavigationService mvxNavigationService, IHistoryService historyService) : base(mvxNavigationService)
        {
            _historyService = historyService;
            ReparationDetailNavigationCommand = new MvxAsyncCommand<Reparation>(ReparationDetailNavigation);
        }

        public override void Prepare(string reparation)
        {
            base.Prepare();
            PlateNumber = reparation;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetHistory ();
        }
        
        async Task GetHistory()
        {
            ShowHistoryError = false;
            Result<MvxObservableCollection<Reparation>> result = await _historyService.GetReparations(PlateNumber);

            if (result.Error)
            {
                HistoryError = result.Message;
                ShowHistoryError = true;
            }
            else
            {
                Reparations = result.Data;
            }
        }
        
        async Task ReparationDetailNavigation(Reparation reparation, CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<ReparationDetailVM, Reparation>(reparation, cancellationToken: cancellationToken);
        }
    }
}