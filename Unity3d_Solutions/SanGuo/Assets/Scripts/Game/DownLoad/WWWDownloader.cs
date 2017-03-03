using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Helper;
//using UnityEngine.Experimental.Networking;  

namespace Game.DownLoad
{
	/// <summary>
	/// www下载
	/// </summary>
	public class WWWDownloader
	{
		/// <summary>
		/// 断点续传读取的标记
		/// </summary>
		public const string ReadBlockMark = "bytes=";

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

			Utility.RunCoroutine (Downloading (task));
		}
			
		internal static IEnumerator Downloading(DownloadTask task)
		{
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add (ReadBlockMark, task.BytesDownloaded.ToString () + "-");
			WWW www = new WWW (task.Item.Url, null, headers);
			yield return www;
			if (www.isDone) {
				if (string.IsNullOrEmpty (www.error)) {
					bool existsFile = File.Exists (task.Item.FilePath);
					FileStream fs;  
					if (existsFile) {
						fs = File.OpenWrite (task.Item.FilePath);
						fs.Seek (task.BytesDownloaded, SeekOrigin.Begin);
					} else {
						fs = File.Create (task.Item.FilePath);
					}

					if (www.bytes.Length - task.BytesDownloaded > 0) {
						fs.Write (www.bytes, task.BytesDownloaded, www.bytes.Length - task.BytesDownloaded);
					}

					task.BytesDownloaded = www.bytesDownloaded;
					task.Size = www.size;

					// 是否下载完毕
					task.IsFinish = task.BytesDownloaded >= task.Size;

					fs.Close ();
					fs.Dispose ();
				} else {
					Log.Warning(www.error);
					task.IsError = true;
				}
				task.IsRunning = false;
			}
		}
	}
}

