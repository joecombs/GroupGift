using Plugin.Messaging;
using System;
using System.Text.RegularExpressions;

namespace GroupGift.Helpers
{
    public static class GroupGiftHelper
    {

        public static bool IsEmailValid(string value, bool isRequired = false)
        {
            var email = value;

            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            return (Regex.IsMatch(value, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        public static void SendDonationSms(this ISmsTask smsTask, string phone, string message)
        {
            if (smsTask.CanSendSms)
            {
                smsTask.SendSms(phone, message);
            }
        }

        public static void MakeDonationCall(this IPhoneCallTask phoneCallTask, string phone)
        {
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(phone);
            }
        }

    }
}
