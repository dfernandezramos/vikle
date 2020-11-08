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