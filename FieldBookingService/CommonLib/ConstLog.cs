using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class ConstLog
    {
        public const string ProcessingTime = "Processing time";

        public static string GetLogProcessingTime(DateTime start)
        {
            return $"{ConstLog.ProcessingTime}: {DateTime.Now.Subtract(start).Ticks}";
        }

        public static double GetProcessingMilliseconds(DateTime start)
        {
            return Math.Round(DateTime.Now.Subtract(start).TotalMilliseconds, 2);
        }

        public static long GetProcessingTicks(DateTime start)
        {
            return DateTime.Now.Subtract(start).Ticks;
        }

        public static string GetMethodFullName(MethodBase methodBase)
        {
            return $"{methodBase?.DeclaringType?.FullName}.{methodBase?.Name}";
        }
    }
}
