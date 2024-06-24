using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class CommonData
    {
        public static string FileAttach = "FileUpload";
        public static string Link_Server_file = "";
        public static string Address_Server_Save_file = "";
        public static string ApiClient_Service = "http:localhost....";
        public static string LinkFileDefError = "../Config/DefError.json";
    }

    public static class User_Type
    {
        public const decimal admin = 0;
        public const decimal user = 1;
    }



    public static class BillDetailType
    {
        public const string product = "PR";
        public const string field = "F";
    }

    public static class FieldBookingStatus
    {
        public const string pending = "P";
        public const string approved = "A";
        public const string done = "D";
        public const string leave_deposit = "L";
        public const string reject = "X";
    }

    public static class Authen_Type
    {
        /// <summary>
        /// Xác thực bằng eKYC
        /// </summary>
        public const decimal eKyc_1 = 1;

        /// <summary>
        /// Xác thực bằng SMS
        /// </summary>
        public const decimal Phone_2 = 2;

        /// <summary>
        /// xác thực bằng email
        /// </summary>
        public const decimal Email_3 = 3;

        /// <summary>
        /// Xác thực nhập thông tin sở hữu
        /// </summary>
        public const decimal Onwer_Qtty_4 = 4;

        /// <summary>
        /// Xác thực bằng tài khoản gốc
        /// </summary>
        public const decimal Account_Source_5 = 5;

    }

    public static class ProfileKeyField
    {
        public const string PRODUCT = "ProductId";
        public const string FIELD = "FieldId";
        public const string FIELDBOOKING = "FieldBookingId";
        public const string CUSTOMER = "CustomerId";
        public const string TIMESLOT = "TimeSlotId";
        public const string BILL = "BillId";
        public const string BILLDETAIL = "BillDetailId";
        public const string STAFF = "StaffId";
        public const string USER = "UserId";
        public const string FUNCTION = "FunctionId";
    }

    public static class DbTable
    {
        public const string PRODUCT = "SanPham";
        public const string FIELD = "SanBong";
        public const string FIELDBOOKING = "DonDatSan";
        public const string CUSTOMER = "KhachHang";
        public const string TIMESLOT = "KhungGio";
        public const string BILL = "HoaDon";
        public const string BILLDETAIL = "ChiTietHoaDon";
        public const string STAFF = "NhanVien";
        public const string USER = "User";
        public const string FUNCTION = "Function";
        public const string ALLCODE = "AllCode";
    }

    public static class AuthenAction
    {
        public const string SEARCH = "SEARCH";
        public const string DETAIL = "DETAIL";
        public const string INSERT = "INSERT";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
    }
    public static class AuthenFunctionId
    {
        public const string PRODUCT = "Product";
        public const string FIELD = "Field";
        public const string FIELDBOOKING = "FieldBooking";
        public const string CUSTOMER = "Customer";
        public const string TIMESLOT = "TimeSlot";
        public const string BILL = "Bill";
        public const string BILLDETAIL = "BillDetail";
        public const string STAFF = "Staff";
        public const string USER = "User";
        public const string FUNCTION = "Function";
    }

}
