namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the status bar view definition for the vehicle detail view.
    /// </summary>
    public partial class StatusBar
    {
        /// <summary>
        /// Gets the Pending status element
        /// </summary>
        public StatusElementView Pending => PendingElement;
        
        /// <summary>
        /// Gets the Repairing status element
        /// </summary>
        public StatusElementView Repairing => RepairingElement;
        
        /// <summary>
        /// Gets the Repaired status element
        /// </summary>
        public StatusElementView Repaired => RepairedElement;
        
        public StatusBar()
        {
            InitializeComponent();
        }
    }
}