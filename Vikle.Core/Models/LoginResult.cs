namespace Vikle.Core.Models
{
    /// <summary>
    /// This class contains the definition of the login action result.
    /// </summary>
    public class LoginResult : Result
    {
        /// <summary>
        /// Gets or sets a boolean indicating if the successful login has worker condition. Otherwise it will have
        /// a client condition.
        /// </summary>
        public bool Worker { get; set; }

        /// <summary>
        /// Gets or sets the user id returned by the API.
        /// </summary>
        public string UserId { get; set; }
    }
}