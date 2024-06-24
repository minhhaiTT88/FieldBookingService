using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class ApiResponse<T>
	{
		public T Data { get; set; }
		/// <summary>
		/// Is Error Or Response  Content when parse model is failed
		/// </summary>
		public string Content { get; set; }
		public string Message { get; set; }
		public bool IsSuccess { get; set; }
	}
}
