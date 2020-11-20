namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the definition of the new item option template for the CollectionView.
    /// </summary>
    public partial class NewCollectionItem
    {
        /// <summary>
        /// Gets or sets the button title label text
        /// </summary>
        public string Title
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }
        
        public NewCollectionItem()
        {
            InitializeComponent();
        }
    }
}