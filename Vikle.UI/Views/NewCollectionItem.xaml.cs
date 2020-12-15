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
    /// This class contains the definition of the new item option template for the CollectionView.
    /// </summary>
    public partial class NewCollectionItem
    {
        /// <summary>
        /// Gets or sets the button title label text
        /// </summary>
        public string Title
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }
        
        public NewCollectionItem()
        {
            InitializeComponent();
        }
    }
}