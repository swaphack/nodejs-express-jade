using System;
using System.Collections;
using System.Collections.Generic;
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
		/// wwww 标记
		/// </summary>
#if UNITY_ANDROID
		public const string WWWMark = "";
#else
		public const string WWWMark = "file://";
#endif

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
		/// 外部可读写路径
		/// </summary>
		/// <returns>The file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetWritableFilePath(string filename)
		{
			string path = Application.persistentDataPath + "/" + filename;

#if UNITY_IPHONE
			iPhone.SetNoBackupFlag(filepath);
#endif
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
		/// 流资源路径
		/// </summary>
		/// <returns>The streaming file path.</returns>
		/// <param name="filename">Filename.</param>
		public static string GetStreamingFilePath (string filename)
		{
			string filepath = Application.streamingAssetsPath + "/" + filename;

			return filepath;
		}

		/// <summary>
		/// 递归创建文件夹
		/// </summary>
		/// <returns>The directory recursive.</returns>
		/// <param name="relativePath">Relative path.</param>
		public static string CreateDirectoryRecursive(string relativePath)
		{
			string path = relativePath.Replace ('\\', '/');
			string[] list = path.Split('/');
			string temp = "";
			for (int i=0;i<list.Length-1;i++)
			{
				string dir = list[i];
				if (string.IsNullOrEmpty(dir))
				{
					continue;
				}
				temp += "/" + dir;
				if (!Directory.Exists(temp))
				{
					Directory.CreateDirectory(temp);
				}
			}

			return temp;
		}


		/// <summary>
		/// 获取指定目录下的所有文件路径
		/// </summary>
		/// <returns><c>true</c>, if file paths was gotten, <c>false</c> otherwise.</returns>
		/// <param name="dir">Dir.</param>
		/// <param name="filepathList">Filepath list.</param>
		public static void getFilePaths(string dir, List<string> filepathList)
		{
			if (string.IsNullOrEmpty (dir) || filepathList == null) {
				return;
			}

			if (!Directory.Exists (dir)) {
				return;
			}

			string[] filepaths = Directory.GetFiles (dir);			
			string[] dirpaths = Directory.GetDirectories (dir);

			foreach (string filename in filepaths) {
				filepathList.Add (dir + "/" + filename);
			}

			foreach (string dirname in dirpaths) {
				getFilePaths (dirname, filepathList);
			}
		}


		/// <summary>
		/// 获取指定目录下的所有文件路径
		/// 文件格式
		/// *.prefab 或 (*.prefab|*.txt)
		/// </summary>
		/// <returns><c>true</c>, if file paths was gotten, <c>false</c> otherwise.</returns>
		/// <param name="dir">Dir.</param>
		/// <param name="filePathList">Filepath list.</param>
		/// <param name="baseDir">路径名称要排除的根目录</param>
		/// <param name="format">文本格式</param>
		public static void getFilePathsWithoutBase(string dir, List<string> filePathList, string baseDir, string format)
		{
			if (string.IsNullOrEmpty (baseDir) || string.IsNullOrEmpty (dir) || filePathList == null) {
				return;
			}

			if (!dir.Contains (baseDir)) {
				return;
			}

			if (!Directory.Exists (dir) || !Directory.Exists (baseDir)) {
				return;
			}

			string[] filePaths;
			if (string.IsNullOrEmpty (format)) {
				filePaths = Directory.GetFiles (dir);	
			} else {
				filePaths = Directory.GetFiles (dir, format);	
			}		
			string[] dirPaths = Directory.GetDirectories (dir);

			foreach (string filename in filePaths) {
				string realdir = filename.Substring (baseDir.Length);
				realdir.Replace('\\', '/');
				filePathList.Add (realdir);
			}

			foreach (string dirpath in dirPaths) {
				getFilePathsWithoutBase (dirpath, filePathList, baseDir, format);
			}
		}
	}
}

