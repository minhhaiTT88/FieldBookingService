using CommonLib;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Net;
using ObjectInfo;
using RestSharp;
using Newtonsoft.Json;
using log4net;

namespace FieldBookingService
{
    public class UploadHelpers
    {
        /// upload file  thẳng nên máy chứa code, cần fpt
        /// 
        public static string upload_file(Stream fileStream, string fileName)
        {
            string status_upfile = "";

            if (fileStream != null && fileStream.Length > 0)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), CommonData.FileAttach);

                var filePath = Path.Combine(folder, fileName); // Thay đổi đường dẫn và tên tệp cần lưu

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.CopyTo(stream);
                }
                status_upfile = Path.Combine(CommonData.FileAttach, fileName);
            }
            return status_upfile;


        }
    }
}
