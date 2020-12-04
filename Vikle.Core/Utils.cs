using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Vikle.Core.Enums;
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
        public static void CloseMenu()
        {
            if(Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false; 
            }
            else if(Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
        }

        /// <summary>
        /// Gets the vehicle image resource name with the provided vehicle type
        /// </summary>
        /// <returns></returns>
        public static string GetVehicleImageName(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                default:
                    return "Vikle.UI.Images.blue_car.png";
                case VehicleType.MotorCycle:
                    return "Vikle.UI.Images.blue_motorbike.png";
                case VehicleType.Truck:
                    return "Vikle.UI.Images.blue_truck.png";
            }
        }
        
        /// <summary>
        /// This method normalizes the provided plate number.
        /// </summary>
        /// <param name="plateNumber">The plate number</param>
        /// <returns>The normalized plate number</returns>
        public static string NormalizePlateNumber(string plateNumber)
        {
            if (string.IsNullOrEmpty(plateNumber.Trim()))
            {
                return string.Empty;
            }
            
            plateNumber = plateNumber.Replace('-', ' ');
            plateNumber = String.Concat(plateNumber.Where(c => !Char.IsWhiteSpace(c)));
            return plateNumber;
        }
    }
}