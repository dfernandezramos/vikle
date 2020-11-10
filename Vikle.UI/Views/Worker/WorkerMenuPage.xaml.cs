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
    /// This class contains the implementation of the Menu page as Master for the MasterDetailPage in the Worker side
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class WorkerMenuPage : MvxContentPage<WorkerMenuVM>
    {
        public WorkerMenuPage()
        {
            InitializeComponent();
        }
    }
}