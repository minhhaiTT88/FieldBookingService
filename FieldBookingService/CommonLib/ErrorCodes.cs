using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static partial class ErrorCodes
    {
        public const int Success = 1;
        //
        public const int Err_Unknown = -1;
        public const int Err_Exception = -2;
        public const int Err_NotFound = -3;
        public const int Err_Existed = -4;
        public const int Err_InvalidData = -5;
        public const int Err_InvalidArgument = -6;
        public const int Err_DataNull = -7;
        public const int Err_Undefined = -8;

        //
        public static bool IsCommonError(long errorCode)
        {
            switch (errorCode)
            {
                case Err_Unknown:
                case Err_Exception:
                case Err_NotFound:
                case Err_Existed:
                case Err_InvalidData:
                case Err_InvalidArgument:
                case Err_DataNull:
                case Err_Undefined:
                case Success:
                    return true;
                default:
                    return false;
            }
        }

        public static string GetErrorDesc(long errorCode, string language)
        {
            bool isEn = language?.Equals("EN", System.StringComparison.OrdinalIgnoreCase) == true;

            switch (errorCode)
            {
                case Err_Unknown: return isEn ? "Unknown error" : "Lỗi không xác định";
                case Err_Exception: return isEn ? "Exception error" : "Lỗi exception";
                case Err_NotFound: return isEn ? "No data found" : "Không tìm thấy dữ liệu";
                case Err_Existed: return isEn ? "Data existed" : "Dữ liệu đã tồn tại";
                case Err_InvalidData: return isEn ? "Invalid data" : "Dữ liệu không hợp lệ";
                case Err_InvalidArgument: return isEn ? "Invalid argument" : "Tham số không hợp lệ";
                case Err_DataNull: return isEn ? "Data null" : "Dữ liệu null";
                case Err_Undefined: return isEn ? "Undefined" : "Chức năng chưa được định nghĩa";
                case Success: return isEn ? "Success" : "Thành công";
                default:
                    break;
            }

            return "-";
        }
    }
}
