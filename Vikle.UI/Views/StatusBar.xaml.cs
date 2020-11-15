using Vikle.Core;
using Vikle.Core.Enums;

namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the status bar view definition for the vehicle detail view.
    /// </summary>
    public partial class StatusBar
    {
        private ReparationStatus? _status;

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
    }
}