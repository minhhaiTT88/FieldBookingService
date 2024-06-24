using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class Utils
    {
        private static readonly string _afAccNoAllowedChars = "0123456789";
        private static readonly Random _random = new();
        private static readonly string _emailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

        public static string GenCustId()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                sb.Append(_afAccNoAllowedChars[_random.Next(0, _afAccNoAllowedChars.Length)]);
            }
            return sb.ToString();
        }
        //
        public static string GenGuidStringN()
        {
            return Utils.GenGuidString("N");
        }
        public static string GenGuidString(string format = "")
        {
            if (!string.IsNullOrEmpty(format))
            {
                return Guid.NewGuid().ToString(format);
            }
            return Guid.NewGuid().ToString();
        }
        //
        public static string SubString(string input, int startIndex)
        {
            return Utils.SubString(input, startIndex, (input?.Length ?? 0) - startIndex);
        }
        public static string SubString(string input, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(input)
                || startIndex < 0
                || startIndex >= input.Length
                || length <= 0
                )
            {
                return string.Empty;
            }

            length = Math.Min(length, input.Length - startIndex);

            //
            if (length > 0)
            {
                return input.AsSpan().Slice(startIndex, length).ToString();
            }
            return input.AsSpan().Slice(startIndex).ToString();
        }

        public static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz";
            const string validUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string validNumber = "1234567890";
            const string validSymbol = "!?*.";
            StringBuilder res = new();

            res.Append(validUpper[RandomNumberGenerator.GetInt32(validUpper.Length)]);
            res.Append(validNumber[RandomNumberGenerator.GetInt32(validNumber.Length)]);
            res.Append(validSymbol[RandomNumberGenerator.GetInt32(validSymbol.Length)]);
            length -= 3;
            while (0 < length--)
            {
                res.Append(valid[RandomNumberGenerator.GetInt32(valid.Length)]);
            }
            return res.ToString();
        }
        //
        public static string ReplaceUnicodeString(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        //
        public static DateTime GetStartEndDateOfMonth(int year, int month, out DateTime endDate)
        {
            endDate = DateTime.MinValue;

            if (year < 1 || year > 9999 || month < 1 || month > 12) return DateTime.MinValue;

            DateTime startDate = new DateTime(year, month, 1);
            if (month == 12)
            {
                endDate = new DateTime(year, month, 31);
            }
            else
            {
                endDate = startDate.AddMonths(1).AddDays(-1);
            }

            return startDate;
        }
        public static DateTime GetStartEndDateOfQuarter(int year, int quarter, out DateTime endDate)
        {
            endDate = DateTime.MinValue;

            if (year < 1 || year > 9999 || quarter < 1 || quarter > 4) return DateTime.MinValue;

            DateTime startDate = new DateTime(year, ((quarter - 1) * 3) + 1, 1);
            if (quarter == 4)
            {
                endDate = new DateTime(year, 12, 31);
            }
            else
            {
                endDate = startDate.AddMonths(3).AddDays(-1);
            }

            return startDate;
        }
        public static DateTime GetStartEndDateOfYear(int year, out DateTime endDate)
        {
            endDate = DateTime.MinValue;

            if (year < 1 || year > 9999) return DateTime.MinValue;

            endDate = new DateTime(year, 12, 31);
            return new DateTime(year, 1, 1);
        }

        public static bool IsEmailAddress(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;

            return (new Regex(_emailExpression, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(2))).IsMatch(value);
        }


        #region Convert DateTime sang Number, Number sang DateTime

        public static int DateToNumber(DateTime date)
        {
            _ = int.TryParse(date.ToString("yyyyMMdd"), out var dateInNumber);
            return dateInNumber;
        }

        public static DateTime DateFromNumber(int dateInNumber)
        {
            _ = DateTime.TryParseExact(dateInNumber.ToString("0").PadLeft(8, '0'), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date);
            return date;
        }

        #endregion

    }
}
