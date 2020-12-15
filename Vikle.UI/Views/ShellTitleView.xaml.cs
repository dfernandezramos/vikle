// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Windows.Input;
using Xamarin.Forms;

namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the TitleView definition for every page inside a shell application.
    /// </summary>
    public partial class ShellTitleView
    {
        public ShellTitleView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the title view title text
        /// </summary>
        public string Title
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }

        /// <summary>
        /// Gets or sets the HomeBtn command
        /// </summary>
        public ICommand HomeButtonCommand
        {
            get => HomeBtn.Command;
            set => HomeBtn.Command = value;
        }

        /// <summary>
        /// Gets or sets a value indicating if the home button is visible or not
        /// </summary>
        public bool HomeButtonVisible
        {
            get => HomeBtn.IsVisible;
            set => HomeBtn.IsVisible = value;
        }
    }
}