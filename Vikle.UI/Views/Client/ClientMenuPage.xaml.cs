using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the implementation of the Menu page as Master for the MasterDetailPage in the Client side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class ClientMenuPage : MvxContentPage<ClientMenuVM>
    {
        public ClientMenuPage()
        {
            InitializeComponent();
        }
    }
}