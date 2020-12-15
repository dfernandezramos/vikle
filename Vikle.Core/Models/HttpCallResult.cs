// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Net;

namespace Vikle.Core.Models
{
    /// <summary>
    /// Http call result generic.
    /// </summary>
    public class HttpCallResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request resulted in an error.
        /// </summary>
        /// <value><c>true</c> if error; otherwise, <c>false</c>.</value>
        public bool Error { get; set; } = false;

        /// <summary>
        /// Gets or sets the http status code returned by the server.
        /// </summary>
        /// <value>The http status code.</value>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error message if any.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Http call result generic.
    /// </summary>
    public class HttpCallResult<T> : HttpCallResult
    {
        /// <summary>
        /// Gets or sets the result of the request.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }
    }
}