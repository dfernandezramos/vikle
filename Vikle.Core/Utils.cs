using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Vikle.Core
{
    /// <summary>
    /// This class contains the implementation of the util methods that can be
    /// shared between classes.
    /// </summary>
    public static class Utils
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method is a workaround for a bug in MVVMCross that does not hide the menu after a navigation.
        /// </summary>
        /// <param name="navigationService">The navigation service</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <typeparam name="T">The destination viewmodel type</typeparam>
        public static async Task MenuNavigation<T>(IMvxNavigationService navigationService, CancellationToken cancellationToken)
            where T : MvxViewModel
        {
            if(Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false; 
            }
            else if(Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
            
            await navigationService.Navigate<T>(cancellationToken: cancellationToken);
        }
    }
}