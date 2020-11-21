using Vikle.Core;
using Xamarin.Forms;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition of the reparation element status view.
    /// </summary>
    public partial class ReparationElementStatus
    {
        private bool _enabled;

        /// <summary>
        /// Gets or sets the title of this element
        /// </summary>
        public string Title
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this element is enabled or not
        /// </summary>
        public bool Enabled
        {
            set => DotImage.Source = ImageSource.FromResource(value ? "Vikle.UI.Images.dot_green.png" : "Vikle.UI.Images.dot.png");
        }

        public ReparationElementStatus()
        {
            InitializeComponent();
        }
    }
}