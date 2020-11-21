using System.Threading.Tasks;
using Vikle.Core.Interfaces;
using Xamarin.Essentials;

namespace Vikle.Core.Services
{
    /// <summary>
    /// This class contains the implementation of the Xamarin Essentials wrapped service
    /// </summary>
    public class XamarinEssentialsService : IXamarinEssentialsService
    {
        /// <summary>
        /// This method uses Xamarin.Essentials to use the phone call method.
        /// </summary>
        /// <param name="phoneNumber">The phone number</param>
        public void MakePhoneCall(string phoneNumber)
        {
            PhoneDialer.Open(phoneNumber);
        }

        /// <summary>
        /// This method uses Xamarin.Essentials to use the mailing app to send a message.
        /// </summary>
        /// <param name="message">The email message</param>
        public async Task SendEmail(EmailMessage message)
        {
            await Email.ComposeAsync(message);
        }
    }
}