using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the client's vehicles view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class VehiclesView : MvxContentPage<VehiclesVM>
    {
        public VehiclesView()
        {
            InitializeComponent();
            TitleView.Title = Title;
            TitleView.HomeButtonVisible = false;
        }
    }
}