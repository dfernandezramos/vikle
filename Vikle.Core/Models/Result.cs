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
    /// This class contains the definition of an action result.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets a boolean indicating if the result has errors or not.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Gets or sets a text message that indicates the login error.
        /// </summary>
        public string Message { get; set; }
    }
    
    /// <summary>
    /// This class contains the definition of an action result with a data model of type T.
    /// </summary>
    public class Result<T> : Result
    {
        /// <summary>
        /// Gets or sets a model of data of type T.
        /// </summary>
        public T Data { get; set; }
    }
}