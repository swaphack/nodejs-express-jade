using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Game.Helper;

namespace Game.DownLoad
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
		/// 服务信任
		/// </summary>
		public static void Init()
		{
			System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
		}

		/// <summary>
		/// 断点续传读取的标记
		/// </summary>
		public const string ReadBlockMark = "bytes";
		/// <summary>
		/// 每次读取的长度
		/// </summary>
		public const int ReadBlockSize = 1024 * 60;
		/// <summary>
		/// 执行任务
		/// </summary>
		/// <param name="task">Task.</param>
		public static void RunTask(DownloadTask task)
		{
			if (File.Exists (task.Item.FilePath)) {
				FileStream fs = File.OpenWrite (task.Item.FilePath);
				task.BytesDownloaded = (int)fs.Length;
				fs.Close ();
				fs.Dispose ();
			} else {
				task.BytesDownloaded = 0;
				FilePathHelp.CreateDirectoryRecursive (task.Item.FilePath);
			}

			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(task.Item.Url); 
			if (httpRequest == null) {
				task.IsError = true;
				return;
			}

			httpRequest.Timeout = 5000;
			httpRequest.Method = "GET";
			httpRequest.AddRange (ReadBlockMark, task.BytesDownloaded);
			httpRequest.Proxy = null;

			httpRequest.BeginGetResponse ((IAsyncResult ar) => {
				try {
					WebResponse response = httpRequest.EndGetResponse (ar);
					HttpWebResponse httpResponse = (HttpWebResponse)response;
					EndDownload(task, httpResponse);	
				}catch (Exception e) {
					Log.Warning(e.Message);
				}
			}, null);
		}

		/// <summary>
		/// 下载后反馈
		/// </summary>
		/// <param name="ar">Ar.</param>
		internal static void EndDownload(DownloadTask task, HttpWebResponse httpResponse)
		{
			if (task == null) {
				return;
			}
			if (httpResponse == null) {
				task.IsError = true;
				return;
			}

			int status = (int)httpResponse.StatusCode;
			if (status != 200 && status != 206) {
				task.IsError = true;
				return;
			}

			task.Size = (int)httpResponse.ContentLength;

			bool existsFile = File.Exists (task.Item.FilePath);
			FileStream fs;
			if (existsFile) {
				fs = File.OpenWrite (task.Item.FilePath);
				fs.Seek (task.BytesDownloaded, SeekOrigin.Begin);
			} else {
				fs = File.Create (task.Item.FilePath);
			}

			byte[] bytes = new byte[ReadBlockSize];
			Stream stream = httpResponse.GetResponseStream ();
			using (BinaryReader br = new BinaryReader (stream, System.Text.UTF8Encoding.UTF8)) {
				while (true) {
					int size = br.Read (bytes, 0, ReadBlockSize);
					if (size <= 0) {
						break;
					}
					if (task.Size - task.BytesDownloaded > 0) {
						fs.Write (bytes, 0, size);
					}

					task.BytesDownloaded += size;
					// 是否下载完毕
					task.IsFinish = task.BytesDownloaded >= task.Size;
				}
				fs.Close ();
				fs.Dispose ();
				task.IsRunning = false;
			}
		}
	}
}

