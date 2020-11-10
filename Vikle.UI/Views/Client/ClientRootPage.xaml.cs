using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class implements the Client Root page for the MasterDetailPage in the Client side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class ClientRootPage : MvxMasterDetailPage<ClientRootVM>
    {
        public ClientRootPage()
        {
            InitializeComponent();
        }
    }
}