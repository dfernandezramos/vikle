namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the TitleView definition for every page inside a shell application.
    /// </summary>
    public partial class ShellTitleView
    {
        public ShellTitleView()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }
    }
}