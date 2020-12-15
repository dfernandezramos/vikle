// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
namespace Vikle.UI.Views
{
    /// <summary>
    /// This class contains the definition of each element in the status bar view for the vehicle detail view.
    /// </summary>
    public partial class StatusElementView
    {
        /// <summary>
        /// Get or sets a boolean indicating whether this element is active or not
        /// </summary>
        public bool Active
        {
            set
            {
                Underline.IsVisible = value;
                GreenDotImage.IsVisible = value;
                YellowDotImage.IsVisible = !value;
            }
        }
        
        /// <summary>
        /// Gets or sets the element name
        /// </summary>
        public string Title
        {
            get => ElementName.Text;
            set => ElementName.Text = value;
        }
        
        public StatusElementView()
        {
            InitializeComponent();
        }
    }
}