// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
namespace Vikle.Core.Models
{
    /// <summary>
    /// This class contains the definition of the confirmation navigation event parameters.
    /// </summary>
    public class ConfirmationParams
    {
        /// <summary>
        /// Gets or sets the confirmation view title text.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the confirmation view subtitle text.
        /// </summary>
        public string Subtitle { get; set; }
    }
}