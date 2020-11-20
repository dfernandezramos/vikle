namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the definition of the secondary header view.
    /// </summary>
    public partial class SecondaryHeader
    {
        /// <summary>
        /// Gets or sets the header title text
        /// </summary>
        public string Title
        {
            get => HeaderTitle.Text;
            set => HeaderTitle.Text = value;
        }
        
        public SecondaryHeader()
        {
            InitializeComponent();
        }
    }
}