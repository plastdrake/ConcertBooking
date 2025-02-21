using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI
{
    public static class Constants
    {
        // URL of REST service (Android does not use localhost)
        // Use http cleartext for local deployment. Change to https for production
        // Change "192.168.1.148" to your local computer's IP address
        // if your debug target is a physical android device
        public static string LocalhostUrl =
        DeviceInfo.Platform == DevicePlatform.Android
        ? (DeviceInfo.DeviceType == DeviceType.Physical ? "192.168.100.123" : "10.0.2.2")
        : "localhost";
        public static string Scheme = "https"; // or http
        public static string Port = "5001"; // or 5000
        public static string RestUrl = $"{Scheme}://{LocalhostUrl}:{Port}/api";

        // REST URLs for endpoints
        public static string BookingUrl = $"{RestUrl}/booking/{{0}}";
        public static string CreateBookingUrl = $"{RestUrl}/booking";
        public static string CustomerRegisterUrl = $"{RestUrl}/customer/register";
        public static string CustomerLoginUrl = $"{RestUrl}/customer/login";
        public static string ConcertUrl = $"{RestUrl}/concert/{{0}}";
        public static string CustomerProfileUrl = $"{RestUrl}/customer/getById";
        public static string CustomerUpdateUrl = $"{RestUrl}/customer/update";
    }
}