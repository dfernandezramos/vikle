using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Vikle.Core.ViewModels;

namespace Vikle.UI.Views.Client
{
    /// <summary>
    /// This class contains the definition for the date detail view.
    /// </summary>
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class DateDetailView : MvxContentPage<DateDetailVM>
    {
        public DateDetailView()
        {
            InitializeComponent();
            
            TitleView.Title = Title;
            ReasonPicker.ItemsSource = new List<string> {"Maintenance", "Reparation", "Other"};
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            TitleView.HomeButtonCommand = ViewModel.HomeNavigationCommand;
        }
    }
}