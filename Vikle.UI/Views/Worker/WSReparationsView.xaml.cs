using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI.Views.Worker
{
    /// <summary>
    /// This class contains the definition of the worker side reparations view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WSReparationsView : ContentPage
    {
        public WSReparationsView()
        {
            InitializeComponent();
            TitleView.Title = Title;
            this.BindingContext = this;
        }
        
        /// <summary>
        /// Gets the current reparations in the workshop for the listview.
        /// </summary>
        public ObservableCollection<Agenda> Reparations { get => GetReparations(); }

        private ObservableCollection<Agenda> GetReparations()
        {
            return new ObservableCollection<Agenda>
            {
                new Agenda { Topic = "All Things Xamarin", Duration = "07:30 UTC - 11:30 UTC", Color = "#B96CBD", Date = new DateTime(2020, 3, 23),
                    Speakers = new ObservableCollection<Speaker>{ new Speaker { Name = "Maddy Leger", Time = "07:30" }, new Speaker { Name = "David Ortinau", Time = "08:30" }, new Speaker { Name = "Gerald Versluis", Time = "10:30" } } }
            };
        }
        
        /// <summary>
        /// TODO: Remove this class when implementing real vehicles on repair at the workshop.
        /// This class defines the Agenda model for displaying its information in the listview.
        /// </summary>
        public class Agenda
        {
            public string Topic { get; set; }
            public string Duration { get; set; }
            public DateTime Date { get; set; }
            public ObservableCollection<Speaker> Speakers { get; set; }
            public string Color { get; set; }
        }

        /// <summary>
        /// TODO: Remove this class when implementing real vehicles on repair at the workshop.
        /// This class defines the Speaker model for displaying its information in the Agenda elements in the listview.
        /// </summary>
        public class Speaker
        {
            public string Name { get; set; }
            public string Time { get; set; }
        }
    }
}