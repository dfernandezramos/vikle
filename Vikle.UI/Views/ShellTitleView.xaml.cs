using System.Windows.Input;
using Xamarin.Forms;

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

        /// <summary>
        /// Gets or sets the title view title text
        /// </summary>
        public string Title
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }

        /// <summary>
        /// Gets or sets the HomeBtn command
        /// </summary>
        public ICommand HomeButtonCommand
        {
            get => HomeBtn.Command;
            set => HomeBtn.Command = value;
        }

        /// <summary>
        /// Gets or sets a value indicating if the home button is visible or not
        /// </summary>
        public bool HomeButtonVisible
        {
            get => HomeBtn.IsVisible;
            set => HomeBtn.IsVisible = value;
        }
    }
}