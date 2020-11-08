namespace Vikle.Core.Models
{
    /// <summary>
    /// This class contains the definition of the signup data the user fills in the signup page.
    /// </summary>
    public class SignupData
    {
        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the user surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Gets or sets the user phone
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Gets or sets the user repeated password
        /// </summary>
        public string RepeatedPassword { get; set; }

        /// <summary>
        /// Gets a boolean indicating whether this data model is complete or not
        /// </summary>
        public bool IsComplete => !string.IsNullOrEmpty(Name) &&
                                  !string.IsNullOrEmpty(Surname) &&
                                  !string.IsNullOrEmpty(Phone) &&
                                  !string.IsNullOrEmpty(Email) &&
                                  !string.IsNullOrEmpty(Password) &&
                                  !string.IsNullOrEmpty(RepeatedPassword);
    }
}