using System;
using System.IO;
using UnityEngine;
using Game.Helper;

namespace Game.Local
{
	/// <summary>
	/// 普通Assets资源加载
	/// </summary>
	public class AssetLoader
	{
		/// <summary>
		/// 静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		private static AssetLoader sAssetLoader;

		private AssetLoader ()
		{
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		public static AssetLoader GetInstance()
		{
			if (sAssetLoader == null) {
				sAssetLoader = new AssetLoader ();
			}

			return sAssetLoader;
		}

		/// <summary>
		/// 获取内部资源完整路径
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <param name="handler">返回存在路径处理</param>
		public static void GetAssetFullPath (string filePath, OnStringCallback handler)
		{
			if (handler == null) {
				Log.Warning ("null callback in GetFullPath");
				return;
			}
			if (string.IsNullOrEmpty(filePath)) {
				Log.Warning ("null file path in GetFullPath");
				handler (null);
				return;
			}

			// 先判断外部是否有资源
			string path = FilePathHelp.GetWritableFilePath (filePath);
			if (File.Exists (path)) {
				handler (path);
				return;
			}

			// 再判断内部资源Assets
			string fileName;
			int index = filePath.IndexOf ('.');
			if (index >= 0) {
				fileName = filePath.Substring (0, index);
			} else {
				fileName = filePath;
			}

			UnityEngine.Object obj = Resources.Load(fileName);
			if (obj != null) {
				handler (fileName);
				return;
			}

			handler (null);
		}
		/// <summary>
		/// 获取Resources文本文件数据
		/// 先从外部获取，找不到的话，再读取内部
		/// </summary>
		/// <returns>The file text data.</returns>
		/// <param name="filepath">文件路径</param>
		/// <param name="format">文本格式</param>
		public static string GetTextData(string filepath, string format)
		{
			if (string.IsNullOrEmpty (filepath) == true) {
				return null;
			}

			string fullpath = filepath;
			if (string.IsNullOrEmpty (format) == false) {
				fullpath = filepath + format;
			}

			fullpath = FilePathHelp.GetWritableFilePath (fullpath);
			if (string.IsNullOrEmpty (fullpath) == false && File.Exists(fullpath) == true) {
				return File.ReadAllText (fullpath);
			}

			TextAsset asset = Resources.Load<TextAsset> (filepath);
			if (asset != null) {
				return asset.text;
			}

			return null;
		}

		/// <summary>
		/// 获取字节文件数据
		/// 先从外部获取，找不到的话，再读取内部
		/// </summary>
		/// <returns>The bytes file data.</returns>
		/// <param name="filepath">文件路径</param>
		/// <param name="format">文本格式</param>
		public static byte[] GetByteData(string filepath, string format)
		{
			if (string.IsNullOrEmpty (filepath) == true) {
				return null;
			}

			string fullpath = filepath;
			if (string.IsNullOrEmpty (format) == false) {
				fullpath = filepath + "." + format;
			}

			fullpath = FilePathHelp.GetWritableFilePath (fullpath);
			if (string.IsNullOrEmpty (fullpath) == false && File.Exists(fullpath) == true) {
				return File.ReadAllBytes (fullpath);
			}

			TextAsset asset = Resources.Load<TextAsset> (filepath);
			if (asset != null) {
				return asset.bytes;
			}
			return null;
		}

	}
}

