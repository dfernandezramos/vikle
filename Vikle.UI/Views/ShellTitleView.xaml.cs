namespace Vikle.UI.Views
{
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