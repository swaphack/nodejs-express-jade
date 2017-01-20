using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
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
			task.IsRunning = true;

			if (File.Exists (task.Item.FilePath)) {
				task.BytesDownloaded = File.ReadAllBytes (task.Item.FilePath).Length;
			} else {
				task.BytesDownloaded = 0;
				FilePathHelp.CreateDirectoryRecursive (task.Item.FilePath);
			}

			Utility.RunCoroutine (Downloading (task));
		}
			
		internal static IEnumerator Downloading(DownloadTask task)
		{
			bool existsFile = task.BytesDownloaded > 0;
			WWWForm from = new WWWForm ();
			from.AddField (ReadBlockMark, task.BytesDownloaded.ToString () + "-");
			WWW www = new WWW (task.Item.Url, from);
			yield return www;
			if (www.isDone) {
				if (string.IsNullOrEmpty (www.error)) {
					task.BytesDownloaded = www.bytesDownloaded;
					task.Size = www.size;
					task.IsRunning = false;

					if (existsFile) {
						using (FileStream fs = File.OpenWrite (task.Item.FilePath)) {
							fs.Seek (task.BytesDownloaded, SeekOrigin.Begin);
							fs.Write (www.bytes, 0, www.bytes.Length);
							fs.Close ();
						}
					} else {
						using (FileStream fs = File.Create (task.Item.FilePath)) {
							fs.Write (www.bytes, 0, www.bytes.Length);
							fs.Close ();
						}
					}
					// 是否下载完毕
					task.IsFinish = task.BytesDownloaded == task.Size;
				} else {
					task.IsError = true;
				}
			} else {
				task.BytesDownloaded = www.bytesDownloaded;
				task.Size = www.size;
			}
		}
	}
}

