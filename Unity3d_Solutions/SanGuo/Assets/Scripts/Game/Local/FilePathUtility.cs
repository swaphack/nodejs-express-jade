using System;
using System.IO;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 文件路径工具
	/// </summary>
	public class FilePathUtility
	{
		private FilePathUtility ()
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
			} else {
				Log.Write ("Read Text File Data From GetDataFilePath Failure : " + path);
			}

			path = GetStreamingFilePath (filename);
			if (File.Exists (path)) {
				return path;
			} else {
				Log.Write ("Read Text File Data From GetStreamingFilePath Failure : " + path);
			}

			path = GetPersistentFilePath (filename);
			if (File.Exists (path)) {
				return path;
			} else {
				Log.Write ("Read Text File Data From GetPersistentFilePath Failure : " + path);
			}

			return "";
		}

		/// <summary>
		/// 获取文本文件数据
		/// 先从内部获取，超找不到的话，再读取外部
		/// </summary>
		/// <returns>The file text data.</returns>
		/// <param name="filepath">文件路径</param>
		/// <param name="format">文本格式</param>
		public static string GetTextFileData(string filepath, string format)
		{
			if (string.IsNullOrEmpty (filepath) == true) {
				return null;
			}

			TextAsset asset = Resources.Load<TextAsset> (filepath);
			if (asset != null) {
				return asset.text;
			} else {
				Log.Write ("Read Text File Data From Inner Failure : " + filepath);
			}

			if (string.IsNullOrEmpty (format) == false) {
				filepath = filepath + "." + format;
			}

			string fullpath = GetFullPath (filepath);
			if (string.IsNullOrEmpty (fullpath) == true) {
				return null;
			} else {
				Log.Write ("Read Text File Data From Outter Failure : " + filepath);
			}

			return File.ReadAllText (fullpath);
		}

		/// <summary>
		/// 获取字节文件数据
		/// 先从内部获取，超找不到的话，再读取外部
		/// </summary>
		/// <returns>The bytes file data.</returns>
		/// <param name="filepath">文件路径</param>
		/// <param name="format">文本格式</param>
		public static byte[] GetBytesFileData(string filepath, string format)
		{
			if (string.IsNullOrEmpty (filepath) == true) {
				return null;
			}

			TextAsset asset = Resources.Load<TextAsset> (filepath);
			if (asset != null) {
				return asset.bytes;
			}

			if (string.IsNullOrEmpty (format) == false) {
				filepath = filepath + "." + format;
			}
			string fullpath = GetFullPath (filepath + "." + format);
			if (string.IsNullOrEmpty (fullpath) == true) {
				return null;
			}

			return File.ReadAllBytes (fullpath);
		}

		/// <summary>
		/// 获取xml文本数据
		/// </summary>
		/// <returns>The xml file data.</returns>
		/// <param name="filepath">Filepath.</param>
		public static string GetXmlFileData(string filepath)
		{
			return GetTextFileData (filepath, "xml");
		}
	}
}

