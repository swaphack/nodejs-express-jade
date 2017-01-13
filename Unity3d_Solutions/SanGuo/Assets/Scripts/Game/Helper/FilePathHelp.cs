using System;
using System.IO;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 文件路径工具
	/// 
	/// windows
	/// ================================================================
	/// dataPath	D:/Documents/Xuporter/Assets
	/// persistentDataPath	C:/Users/Administrator/AppData/LocalLow/mj/path
	/// streamingAssetsPath	D:/Documents/Xuporter/Assets/StreamingAssets
	/// temporaryCachePath	C:/Users/ADMINI~1/AppData/Local/Temp/mj/pat
	/// ================================================================
	/// 
	/// android
	/// ================================================================
	/// dataPath	/data/app/com.mi.path-1.apk	无权限
	/// persistentDataPath	/data/data/com.mi.path/files	读写，强推荐
	/// streamingAssetsPath	jar:file:///data/app/com.mi.path-1.apk!/assets	只读
	/// temporaryCachePath	/data/data/com.mi.path/cache	读写
	/// 
	/// ios
	/// ================================================================
	/// dataPath	/var/mobile/Containers/Bundle/Application/AFE239B4-2FE5-48B5-8A31-FC23FEDA0189/ad.app/Data 	无权限
	/// persistentDataPath	/var/mobile/Containers/Data/Application/FFEEF1E0-E15E-4BC0-9E8F-78084A2085A0/Documents 读写，强推荐
	/// streamingAssetsPath	/var/mobile/Containers/Bundle/Application/AFE239B4-2FE5-48B5-8A31-FC23FEDA0189/ad.app/Data/Raw	只读
	/// temporaryCachePath	/var/mobile/Containers/Data/Application/FFEEF1E0-E15E-4BC0-9E8F-78084A2085A0/Library/Caches	读写
	/// </summary>
	public class FilePathHelp
	{
		private FilePathHelp ()
		{
		}

		/// <summary>
		/// 显示默认路径
		/// </summary>
		public static void ShowPath()
		{
			Log.Info ("Application.dataPath : " + Application.dataPath);
			Log.Info ("Application.persistentDataPath : " + Application.persistentDataPath);
			Log.Info ("Application.streamingAssetsPath : " + Application.streamingAssetsPath);
			Log.Info ("Application.temporaryCachePath : " + Application.temporaryCachePath);
		}

		/// <summary>
		/// 可读写路径
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetWritableFilePath(string filename)
		{
			string path = "";

			path = Application.persistentDataPath + "/" + filename;

			return path;
		}

		/// <summary>
		/// 临时可读写路径
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetTempFilePath(string filename)
		{
			string path = "";

			path = Application.temporaryCachePath + "/" + filename;

			return path;
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

		/// <summary>
		/// 获取文本文件数据
		/// 先从内部获取，找不到的话，再读取外部
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
			}

			if (string.IsNullOrEmpty (format) == false) {
				filepath = filepath + "." + format;
			}

			string fullpath = GetWritableFilePath (filepath);
			if (string.IsNullOrEmpty (fullpath) == true || File.Exists(fullpath) == false) {
				return null;
			}

			Log.Info ("Read Text File Data From Outter : " + fullpath);

			return File.ReadAllText (fullpath);
		}

		/// <summary>
		/// 获取字节文件数据
		/// 先从内部获取，找不到的话，再读取外部
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
			string fullpath = GetWritableFilePath (filepath);
			if (string.IsNullOrEmpty (fullpath) == true || File.Exists(fullpath) == false) {
				return null;
			}

			Log.Info ("Read Byte File Data From Outter : " + fullpath);

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

