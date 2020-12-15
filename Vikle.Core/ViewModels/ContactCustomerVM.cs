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
using Vikle.Core.Interfaces;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Contact Customer VM
    /// </summary>
    public class ContactCustomerVM : WorkerBaseVM<string>
    {
        private IContactCustomerService _contactCustomerService;
        private User _model;
        private string _plateNumber;
        private string _contactError;
        private bool _showContactError;

        /// <summary>
        /// Gets or sets the user model
        /// </summary>
        public User Model
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
        /// Gets or sets the user name
        /// </summary>
        public string Name
        {
            get => Model?.Name;
            set
            {
                Model.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the user surname
        /// </summary>
        public string SurName
        {
            get => Model?.Surname;
            set
            {
                Model.Surname = value;
                RaisePropertyChanged(() => SurName);
            }
        }
        
        /// <summary>
        /// Gets or sets the user phone
        /// </summary>
        public string Phone
        {
            get => Model?.Phone;
            set
            {
                Model.Phone = value;
                RaisePropertyChanged(() => Phone);
            }
        }
        
        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string Email
        {
            get => Model?.Email;
            set
            {
                Model.Email = value;
                RaisePropertyChanged(() => Email);
            }
        }
        
        /// <summary>
        /// Gets or sets the contact error message.
        /// </summary>
        public string ContactError
        {
            get => _contactError;
            set
            {
                _contactError = value;
                RaisePropertyChanged(() => ContactError);
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether the contact error message has to be shown or not.
        /// </summary>
        public bool ShowContactError
        {
            get => _showContactError;
            set
            {
                _showContactError = value;
                RaisePropertyChanged(() => ShowContactError);
            }
        }
        
        /// <summary>
        /// Gets or sets the call customer command
        /// </summary>
        public MvxAsyncCommand CallCommand { get; set; }
        
        /// <summary>
        /// Gets or sets the call customer command
        /// </summary>
        public MvxAsyncCommand SendEmailCommand { get; set; }
        
        public ContactCustomerVM(IMvxNavigationService mvxNavigationService, IContactCustomerService contactCustomerService) : base(mvxNavigationService)
        {
            _contactCustomerService = contactCustomerService;
            CallCommand = new MvxAsyncCommand(CallCustomer);
            SendEmailCommand = new MvxAsyncCommand(SendEmail);
        }

        public override void Prepare(string plateNumber)
        {
            _plateNumber = plateNumber;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetOwner ();
        }
        
        async Task GetOwner()
        {
            ShowContactError = false;
            Result<User> result = await _contactCustomerService.GetVehicleOwner(_plateNumber);

            if (result.Error)
            {
                ContactError = result.Message;
                ShowContactError = true;
            }
            else
            {
                Model = result.Data;
            }
        }
        
        async Task CallCustomer(CancellationToken arg)
        {
            ShowContactError = false;
            Result result = await _contactCustomerService.CallCustomer(Phone);
            
            if (result.Error)
            {
                ContactError = result.Message;
                ShowContactError = true;
            }
        }
        
        async Task SendEmail(CancellationToken arg)
        {
            ShowContactError = false;
            Result result = await _contactCustomerService.SendEmail(Email);
            
            if (result.Error)
            {
                ContactError = result.Message;
                ShowContactError = true;
            }
        }
    }
}