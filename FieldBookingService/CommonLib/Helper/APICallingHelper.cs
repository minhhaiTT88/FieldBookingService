using log4net;
using Newtonsoft.Json;
using ObjectInfo;
using System.Net;
using System.Reflection;
using System.Text;

namespace CommonLib.Helper
{
	public static class APICallingHelper
	{
		/// <summary>
		/// 	Logger
		/// </summary>
		private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static List<V> GetListFromAPI<T, V>(T requestObj, string sendOrderURL, string method = WebRequestMethods.Http.Get)
		{
			var result = new List<V>();
			var requestContent = JsonConvert.SerializeObject(requestObj, Formatting.Indented);

			_logger.DebugFormat("GetListResult: url:{0} - requestContent:{1}", sendOrderURL, requestContent);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
			httpWebRequest.Method = method;
			httpWebRequest.Headers.Clear();
			httpWebRequest.Accept = "text/json";
			httpWebRequest.ContentType = "application/json";

			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				var bytes = Encoding.UTF8.GetBytes(requestContent);
				stream.Write(bytes, 0, bytes.Length);
			}

			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string responseContent = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					responseContent = sr.ReadToEnd();
				}
			}
			var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
			_logger.Debug(messNotice);
			response.Close();

			var data = JsonConvert.DeserializeObject<ListResponeMessage<V>>(responseContent);
			_logger.Debug($"Data DeserializeObject: {data}");

			if (data != null)
			{
				result.AddRange(data.Data);
				if (!data.IsSuccess)
				{
					var mess = $"{data.Err.MsgCode} - {data.Err.MsgString}";
					_logger.Error(mess);
					throw new Exception(mess);
				}
			}

			return result;
		}
		public static List<V> GetListFromAPI_NoLog<T, V>(T requestObj, string sendOrderURL, string method = WebRequestMethods.Http.Get)
		{
			var result = new List<V>();
			var requestContent = JsonConvert.SerializeObject(requestObj, Formatting.Indented);

			_logger.DebugFormat("GetListResult: url:{0} - requestContent:{1}", sendOrderURL, requestContent);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
			httpWebRequest.Method = method;
			httpWebRequest.Headers.Clear();
			httpWebRequest.Accept = "text/json";
			httpWebRequest.ContentType = "application/json";

			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				var bytes = Encoding.UTF8.GetBytes(requestContent);
				stream.Write(bytes, 0, bytes.Length);
			}

			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string responseContent = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					responseContent = sr.ReadToEnd();
				}
			}
			response.Close();

			var data = JsonConvert.DeserializeObject<ListResponeMessage<V>>(responseContent);

			if (data != null)
			{
				result.AddRange(data.Data);
				if (!data.IsSuccess)
				{
					var mess = $"{data.Err.MsgCode} - {data.Err.MsgString}";
					_logger.Error(mess);
					throw new Exception(mess);
				}
			}

			return result;
		}
		public static TResponse GetSingleResultFromAPI<TRequest, TResponse>(TRequest requestObj, string sendOrderURL, string method = WebRequestMethods.Http.Post)
		{
			TResponse result = default(TResponse);
			var requestContent = JsonConvert.SerializeObject(requestObj, Formatting.Indented);

			_logger.DebugFormat("GetListResult: url:{0} - requestContent:{1}", sendOrderURL, requestContent);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
			httpWebRequest.Method = method;
			httpWebRequest.Headers.Clear();
			httpWebRequest.Accept = "text/json";
			httpWebRequest.ContentType = "application/json";

			if (method != WebRequestMethods.Http.Get)
			{
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					var bytes = Encoding.UTF8.GetBytes(requestContent);
					stream.Write(bytes, 0, bytes.Length);
				}
			}

			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string responseContent = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					responseContent = sr.ReadToEnd();
				}
			}
			var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
			_logger.Debug(messNotice);


			var data = JsonConvert.DeserializeObject<TResponse>(responseContent);
			_logger.Debug($"Data DeserializeObject: {data}");

			if (data != null)
			{
				result = data;
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new Exception(responseContent);
				}
			}
			response.Close();
			return result;
		}

		public static TResponse GetSingleResultFromAPI<TResponse>(string sendOrderURL, string method = WebRequestMethods.Http.Post)
		{
			TResponse result = default(TResponse);

			_logger.DebugFormat("GetListResult: url:{0}", sendOrderURL);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
			httpWebRequest.Method = method;
			httpWebRequest.Headers.Clear();
			httpWebRequest.Accept = "*/*";
			httpWebRequest.ContentType = "application/json";

			if (method != WebRequestMethods.Http.Get)
			{
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					var bytes = Encoding.UTF8.GetBytes("{}");
					stream.Write(bytes, 0, bytes.Length);
				}
			}

			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string responseContent = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					responseContent = sr.ReadToEnd();
				}
			}

			var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
			_logger.Debug(messNotice);
			response.Close();

			var data = JsonConvert.DeserializeObject<SingleResponeMessage<TResponse>>(responseContent);
			_logger.Debug($"Data DeserializeObject: {data}");

			if (data != null)
			{
				result = data.Item;
				if (!data.IsSuccess)
				{
					var mess = $"{data.Err.MsgCode} - {data.Err.MsgString}";
					_logger.Error(mess);
					throw new Exception(mess);
				}
			}

			return result;
		}

		public static ActionMessage ExecuteAPI<T>(T requestObj, string sendOrderURL)
		{
			var requestContent = JsonConvert.SerializeObject(requestObj, Formatting.Indented);

			_logger.DebugFormat("ExecuteAPI: url:{0} - requestContent:{1}", sendOrderURL, requestContent);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
			httpWebRequest.Method = WebRequestMethods.Http.Post;
			httpWebRequest.Headers.Clear();
			httpWebRequest.Accept = "text/json";
			httpWebRequest.ContentType = "application/json";

			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				var bytes = Encoding.UTF8.GetBytes(requestContent);
				stream.Write(bytes, 0, bytes.Length);
			}

			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string responseContent = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					responseContent = sr.ReadToEnd();
				}
			}
			var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
			_logger.Debug(messNotice);

			response.Close();
			var data = JsonConvert.DeserializeObject<ActionMessage>(responseContent);
			_logger.Debug($"Data DeserializeObject: {data}");
			if (data != null)
			{
				if (!data.IsSuccess)
				{
					_logger.Error($"{data.Err.MsgCode}: {data.Err.MsgString}");
				}
				return data;
			}

			return null;
		}

		public static V GetSingleResult<V>(string sendOrderURL, string method = WebRequestMethods.Http.Get)
		{
			V result = default(V);

			_logger.DebugFormat("GetListResult: url:{0}", sendOrderURL);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
			httpWebRequest.Method = method;
			httpWebRequest.Headers.Clear();
			httpWebRequest.Accept = "text/json";
			httpWebRequest.ContentType = "application/json";

			if (method != WebRequestMethods.Http.Get)
			{
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					var bytes = Encoding.UTF8.GetBytes("{}");
					stream.Write(bytes, 0, bytes.Length);
				}
			}
			//System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
			if (sendOrderURL.Contains("https"))
			{
				//ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
				//ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 | SecurityProtocolType.Tls;
				//System.Net.ServicePointManager.Expect100Continue = false;

				ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			}
			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string responseContent = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					responseContent = sr.ReadToEnd();
				}
			}

			var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
			_logger.Debug(messNotice);
			response.Close();
			result = JsonConvert.DeserializeObject<V>(responseContent);

			return result;
		}

		public static ApiResponse<V> GetRequestAPI<V>(string sendOrderURL, string method = WebRequestMethods.Http.Get, object data = null, string authorization = null, string contentType = "application/json", string accept = "application/json", string mediaType = "application/json", string XClientAppType = "Hospital")
		{
			var result = new ApiResponse<V>();

			_logger.DebugFormat("GetRequestAPI: url:{0}", sendOrderURL);
			string responseContent = string.Empty;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
				if (sendOrderURL.Contains("https"))
				{
					ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
				}
				httpWebRequest.Method = method;
				httpWebRequest.Headers.Clear();
				if (!string.IsNullOrEmpty(authorization))
				{
					httpWebRequest.Headers.Add("Authorization", authorization);
				}

				if (!string.IsNullOrEmpty(contentType))
				{
					httpWebRequest.ContentType = contentType;
				}
				if (!string.IsNullOrEmpty(mediaType))
				{
					httpWebRequest.MediaType = mediaType;
				}
				if (XClientAppType != null)
				{
					httpWebRequest.Headers.Add("X-Client-App-Type", XClientAppType);
				}
				if (!string.IsNullOrEmpty(accept))
				{
					httpWebRequest.Accept = accept;
				}
				httpWebRequest.KeepAlive = false;
				httpWebRequest.Proxy = null;

				if (method != WebRequestMethods.Http.Get)
				{
					string jsonString = JsonConvert.SerializeObject(data);
					_logger.Debug("GetRequestAPI jsonString: " + jsonString);
					var bytes = Encoding.UTF8.GetBytes(jsonString);

					httpWebRequest.ContentLength = bytes.Length;

					using (Stream stream = httpWebRequest.GetRequestStream())
					{
						stream.Write(bytes, 0, bytes.Length);
					}
				}

				HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();

				using (Stream stream = response.GetResponseStream())
				{
					using (StreamReader sr = new StreamReader(stream))
					{
						responseContent = sr.ReadToEnd();
					}
				}

				var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
				_logger.Debug(messNotice);
				response.Close();
				//responseContent=responseContent.Replace("\\","");
				//responseContent=responseContent.Substring(1, responseContent.Length-1);
				//responseContent = responseContent.Substring(0, responseContent.Length - 1);
				result.Content = responseContent;

				try
				{
					result.Data = JsonConvert.DeserializeObject<V>(responseContent);
					result.IsSuccess = true;
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
					throw new Exception("Lỗi khi map dữ liệu từ api: " + ex.Message);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				result.Message = ex.Message;
			}

			return result;
		}
		public static ApiResponse<V> POSTRequestAPI<V>(string sendOrderURL, string method = WebRequestMethods.Http.Get, Dictionary<string, object> data = null, string authorization = null, string contentType = "application/json", string accept = "application/json", string mediaType = "application/json", string XClientAppType = "Hospital")
		{

			var result = new ApiResponse<V>();

			_logger.DebugFormat("GetRequestAPI: url:{0}", sendOrderURL);
			string responseContent = string.Empty;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sendOrderURL);
				httpWebRequest.Method = method;
				httpWebRequest.Headers.Clear();
				if (!string.IsNullOrEmpty(authorization))
				{
					httpWebRequest.Headers.Add("Authorization", authorization);
				}

				if (!string.IsNullOrEmpty(contentType))
				{
					httpWebRequest.ContentType = contentType;
				}
				if (!string.IsNullOrEmpty(mediaType))
				{
					httpWebRequest.MediaType = mediaType;
				}
				if (XClientAppType != null)
				{
					httpWebRequest.Headers.Add("X-Client-App-Type", XClientAppType);
				}
				if (!string.IsNullOrEmpty(accept))
				{
					httpWebRequest.Accept = accept;
				}
				httpWebRequest.KeepAlive = false;
				httpWebRequest.Proxy = null;

				if (method != WebRequestMethods.Http.Get)
				{
					if (data != null)
					{
						string postData = string.Empty;

						foreach (var item in data.Select((value, i) => new { value, i }))
						{
							if (item.i == 0)
							{
								postData += string.Format("{0} = {1}", item.value.Key, item.value.Value);
							}
							else
							{
								postData += string.Format("&{0} = {1}", item.value.Key, item.value.Value);
							}

						}

						var bytes = Encoding.UTF8.GetBytes(postData);
						httpWebRequest.ContentLength = bytes.Length;

						using (Stream stream = httpWebRequest.GetRequestStream())
						{
							stream.Write(bytes, 0, bytes.Length);
						}
					}



				}

				HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();

				using (Stream stream = response.GetResponseStream())
				{
					using (StreamReader sr = new StreamReader(stream))
					{
						responseContent = sr.ReadToEnd();
					}
				}

				var messNotice = $"Kết quả gọi API: {sendOrderURL}. StatusCode: {response.StatusCode} - contentResponse:{responseContent}";
				_logger.Debug(messNotice);
				response.Close();

				result.Content = responseContent;

				try
				{
					result.Data = JsonConvert.DeserializeObject<V>(responseContent);
					result.IsSuccess = true;
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
					throw new Exception("Lỗi khi map dữ liệu từ api: " + ex.Message);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				result.Message = ex.Message;
			}

			return result;
		}


	}

}
