namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the definition of each element in the status bar view for the vehicle detail view.
    /// </summary>
    public partial class StatusElementView
    {
        /// <summary>
        /// Get or sets a boolean indicating whether this element is active or not
        /// </summary>
        public bool Active
        {
            set
            {
                Underline.IsVisible = value;
                GreenDotImage.IsVisible = value;
                YellowDotImage.IsVisible = !value;
            }
        }
        
        /// <summary>
        /// Gets or sets the element name
        /// </summary>
        public string Title
        {
            get => ElementName.Text;
            set => ElementName.Text = value;
        }
        
        public StatusElementView()
        {
            InitializeComponent();
        }
    }
}