using System;
using System.IO;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 文件路径
	/// </summary>
	public class FilePathUtility
	{
		public FilePathUtility ()
		{
		}

		/// <summary>
		/// 游戏数据存储路径
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetDataFilePath(string filename)
		{
			string path = "";

			path = Application.dataPath + "/" + filename;

			return path;
		}

		/// <summary>
		/// 流资源路径
		/// </summary>
		/// <returns>The streaming file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetStreamingFilePath (string filename)
		{
			string path = "";


			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer ||
			     Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				path = Application.dataPath + "/StreamingAssets/" + filename;
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				path = Application.dataPath + "/Raw/" + filename;
			else if (Application.platform == RuntimePlatform.Android)
				path = "jar:file://" + Application.dataPath + "!/assets/" + filename;
			else
				path = Application.dataPath + "/config/" + filename;


			return path;
		}


		/// <summary>
		/// 持久性数据路径
		/// </summary>
		/// <returns>The persistent file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetPersistentFilePath (string filename)
		{
			string filepath;


			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer ||
			     Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				filepath = Application.dataPath + "/StreamingAssets/" + filename;
			else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				filepath = Application.persistentDataPath + "/" + filename;
			else {
				filepath = Application.persistentDataPath + "/" + filename;
			}
#if UNITY_IPHONE
    		iPhone.SetNoBackupFlag(filepath);
#endif
			return filepath;
		}
			
		/// <summary>
		/// 获取资源完整路径
		/// </summary>
		/// <returns>The full path.</returns>
		/// <param name="filename">文件名</param>
		public static string GetFullPath (string filename)
		{
			string path = GetDataFilePath (filename);
			if (File.Exists (path)) {
				return path;
			} 

			path = GetStreamingFilePath (filename);
			if (File.Exists (path)) {
				return path;
			}

			path = GetPersistentFilePath (filename);
			if (File.Exists (path)) {
				return path;
			}

			return "";
		}
	}
}

