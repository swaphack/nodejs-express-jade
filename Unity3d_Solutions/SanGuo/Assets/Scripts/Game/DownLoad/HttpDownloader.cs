using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace Game
{
	/// <summary>
	/// Http 下载.
	/// </summary>
	public class HttpDownloader
	{
		public class HttpRequestItem
		{
			/// <summary>
			/// 下载任务
			/// </summary>
			public DownloadTask Task;
			/// <summary>
			/// web请求
			/// </summary>
			public HttpWebRequest Request;

			public HttpRequestItem(DownloadTask task, HttpWebRequest request)
			{
				Task = task;
				Request = request;
			}
		}

		/// <summary>
		/// 断点续传读取的标记
		/// </summary>
		public const string ReadBlockMark = "bytes";
		/// <summary>
		/// 执行任务
		/// </summary>
		/// <param name="task">Task.</param>
		public static void RunTask(DownloadTask task)
		{
			if (File.Exists (task.Item.FilePath)) {
				task.BytesDownloaded = File.ReadAllBytes (task.Item.FilePath).Length;
			} else {
				task.BytesDownloaded = 0;
				FilePathHelp.CreateDirectoryRecursive (task.Item.FilePath);
			}

			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(task.Item.Url); 
			if (httpRequest == null) {
				task.IsError = true;
				return;
			}

			httpRequest.Timeout = 2000;
			httpRequest.Method = "GET";
			httpRequest.AddRange (ReadBlockMark, -task.BytesDownloaded);

			httpRequest.BeginGetResponse (Downloading, new HttpRequestItem(task, httpRequest));
		}

		internal static void Downloading(IAsyncResult ar)
		{
			HttpRequestItem item = (HttpRequestItem)ar.AsyncState;
			DownloadTask task = item.Task;
			HttpWebResponse httpResponse = (HttpWebResponse)item.Request.EndGetResponse (ar);
			if (httpResponse == null) {
				task.IsError = true;
				return;
			}

			int status = (int)httpResponse.StatusCode;
			if (status != 200 || status != 206) {
				task.IsError = true;
				return;
			}

			task.Size = (int)httpResponse.ContentLength;
			task.IsRunning = false;

			bool existsFile = task.BytesDownloaded > 0;
			int size = (int)httpResponse.GetResponseStream ().Length;
			using (BinaryReader br = new BinaryReader (httpResponse.GetResponseStream (), System.Text.UTF8Encoding.UTF8)) {
				if (existsFile) {
					using (FileStream fs = File.OpenWrite (task.Item.FilePath)) {
						fs.Seek (task.BytesDownloaded, SeekOrigin.Begin);
						fs.Write (br.ReadBytes (size), 0, size);
						fs.Close ();
					}
				} else {
					using (FileStream fs = File.Create (task.Item.FilePath)) {
						fs.Write (br.ReadBytes (size), 0, size);
						fs.Close ();
					}
				}
				task.BytesDownloaded += size;
				// 是否下载完毕
				task.IsFinish = task.BytesDownloaded == task.Size;
			}
		}
	}
}

