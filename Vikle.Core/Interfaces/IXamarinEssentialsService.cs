using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Vikle.Core.Interfaces
{
    /// <summary>
    /// This interface contains the definition of the Xamarin Essentials wrapped service
    /// </summary>
    public interface IXamarinEssentialsService
    {
        /// <summary>
        /// This method uses Xamarin.Essentials to use the phone call method.
        /// </summary>
        /// <param name="phoneNumber">The phone number</param>
        void MakePhoneCall(string phoneNumber);
        
        /// <summary>
        /// This method uses Xamarin.Essentials to use the mailing app to send a message.
        /// </summary>
        /// <param name="destinationEmail">The destination email address</param>
        Task SendEmail(EmailMessage destinationEmail);
    }
}