using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the client reparation detail view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReparationDetailView : ContentPage
    {
        public ReparationDetailView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}