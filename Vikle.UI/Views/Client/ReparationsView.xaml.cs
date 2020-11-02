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
    /// This class contains the definition of the client vehicle reparations history.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReparationsView : ContentPage
    {
        public ReparationsView()
        {
            InitializeComponent();
            TitleView.Title = Title;
        }
    }
}