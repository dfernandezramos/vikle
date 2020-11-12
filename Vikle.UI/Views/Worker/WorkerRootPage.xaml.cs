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

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class implements the Client Root page for the MasterDetailPage in the Worker side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class WorkerRootPage : MvxMasterDetailPage<WorkerRootVM>
    {
        public WorkerRootPage()
        {
            InitializeComponent();
        }
    }
}