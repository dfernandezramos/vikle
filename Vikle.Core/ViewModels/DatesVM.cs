// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Collections.ObjectModel;
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
    /// This class contains the implementation of the Dates ViewModel.
    /// </summary>
    public class DatesVM : ClientBaseVM
    {
        private string _datesError;
        private bool _showDatesError;
        private ObservableCollection<Date> _dates;
        private IDatesService _datesService;

        /// <summary>
        /// Gets the reparation models of the current reparations in the Workshop.
        /// </summary>
        public ObservableCollection<Date> Dates
        {
            get => _dates;
            set
            {
                _dates = value;
                RaisePropertyChanged(() => Dates);
            }
        }

        /// <summary>
        /// Gets or sets the dates error message.
        /// </summary>
        public string DatesError
        {
            get => _datesError;
            set
            {
                _datesError = value;
                RaisePropertyChanged(() => DatesError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the dates error message has to be shown or not.
        /// </summary>
        public bool ShowDatesError
        {
            get => _showDatesError;
            set
            {
                _showDatesError = value;
                RaisePropertyChanged(() => ShowDatesError);
            }
        }
        
        /// <summary>
        /// Gets or sets the command to show the create a new date
        /// </summary>
        public MvxAsyncCommand NewDateDetailsCommand { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating whether this viewmodel is calling the API or not
        /// </summary>
        public bool CallingAPI { get; set; }

        public DatesVM(IMvxNavigationService mvxNavigationService, IDatesService datesService) : base(mvxNavigationService)
        {
            _dates = new ObservableCollection<Date>();
            NewDateDetailsCommand = new MvxAsyncCommand(NewDateNavigation);
            _datesService = datesService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            Task.Run(GetDates);
        }

        async Task GetDates()
        {
            CallingAPI = true;
            ShowDatesError = false;
            Result<MvxObservableCollection<Date>> result = await _datesService.GetUserDates();

            if (result.Error)
            {
                DatesError = result.Message;
                ShowDatesError = true;
            }
            else
            {
                Dates = result.Data;
            }
            CallingAPI = false;
        }

        async Task NewDateNavigation(CancellationToken cancellationToken)
        {
            await _mvxNavigationService.Navigate<DateDetailVM>(cancellationToken: cancellationToken);
        }
    }
}