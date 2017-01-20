using System;

namespace Game
{
	/// <summary>
	/// 下载项
	/// </summary>
	public class DownloadItem
	{
		/// <summary>
		/// 请求的url
		/// </summary>
		public string Url;
		/// <summary>
		/// 本地保存的文件路径
		/// </summary>
		public string FilePath;


		public DownloadItem(string url, string filePath)
		{
			Url = url;
			FilePath = filePath;
		}
	}
}

