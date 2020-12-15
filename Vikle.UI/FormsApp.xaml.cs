// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vikle.UI
{
    /// <summary>
    /// This class contains the Main App class for the application. It will initialize the main components of the
    /// application and handle the main events of the app.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormsApp : Application
    {
        public FormsApp()
        {
            InitializeComponent();

#if DEBUG
            HotReloader.Current.Run(this); 
#endif
            Device.SetFlags(new string[] { "Expander_Experimental" });
        }
    }
}