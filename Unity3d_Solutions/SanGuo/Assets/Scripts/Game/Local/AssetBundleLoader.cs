using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Game.Local
{
	/// <summary>
	/// AssetBundle资源包加载
	/// </summary>
	public class AssetBundleLoader
	{
		/// <summary>
		/// 资源包项
		/// </summary>
		public struct ABItem
		{
			/// <summary>
			/// 资源路径
			/// </summary>
			public string FilePath;
			/// <summary>
			/// 资源包
			/// </summary>
			public AssetBundle AssetBundle;
		}
		/// <summary>
		/// 静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		private static AssetBundleLoader sAssetBundleLoader;

		private Dictionary<string, ABItem> _LoadItems;

		private AssetBundleLoader ()
		{
			_LoadItems = new Dictionary<string, ABItem> ();
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		/// <returns>The instance.</returns>
		public static AssetBundleLoader GetInstance()
		{
			if (sAssetBundleLoader == null) {
				sAssetBundleLoader = new AssetBundleLoader ();
			}

			return sAssetBundleLoader;
		}

		/// <summary>
		/// 加载资源包
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="handler">Handler.</param>
		public void LoadAssetBundle(string filePath, OnBooleanCallback handler)
		{
			if (handler == null) {
				Log.Warning ("null call back In LoadAssetBundle");
				return;
			}
			if (string.IsNullOrEmpty (filePath)) {
				handler (false);
				return;
			}

			FilePathLoader.GetInstance().Load(filePath, (WWW www) =>{
				if (www == null) {
					handler (false);
					return;	
				}

				string[] allAssetNames = www.assetBundle.GetAllAssetNames();
				int count = allAssetNames.Length;
				for (int i = 0; i < count; i++) {
					Log.Info ("AssetName : " + allAssetNames[i]);
				}

				ABItem item = new ABItem ();
				item.FilePath = filePath;
				item.AssetBundle = www.assetBundle;
				_LoadItems[filePath] = item;

				handler(true);
			});
		}

		/// <summary>
		/// 卸载资源
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		public void UnloadAssetBundle(string filepath)
		{
			if (string.IsNullOrEmpty(filepath)) {
				Log.Warning ("null file path in UnloadAssetBundle");
				return;
			}

			if (!_LoadItems.ContainsKey (filepath)) {
				return;
			}

			if (_LoadItems [filepath].AssetBundle) {
				_LoadItems [filepath].AssetBundle.Unload (false);
				_LoadItems.Remove (filepath);
			}
		}


		/// <summary>
		/// 从资源包中读取对象
		/// 需在调用此函数之前，调用加载filepath函数LoadAssetBundle
		/// </summary>
		/// <param name="filePath">配置路径</param>
		/// <param name="itemName">物件名称</param>
		/// <param name="handler">加载prefab回调</param>
		public GameObject LoadGameObject(string filePath, string itemName)
		{
			if (string.IsNullOrEmpty (filePath) || string.IsNullOrEmpty (itemName)) {
				return null;
			}
			if (!_LoadItems.ContainsKey (filePath)) {
				return null;
			}

			ABItem instance = _LoadItems [filePath];
			AssetBundle assetBundle = instance.AssetBundle;
			if (assetBundle)
				return assetBundle.LoadAsset<GameObject> (itemName);
			else
				return null;
		}

		/// <summary>
		/// 获取内部资源完整路径
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <param name="handler">文件名</param>
		public static void GetAssetBundleFullPath (string filepath, OnStringCallback handler)
		{
			if (handler == null) {
				Log.Warning ("null call back in GetAssetBundleFullPath");
				return;
			}
			if (string.IsNullOrEmpty(filepath)) {
				Log.Warning ("null file path in GetFullPath");
				handler (null);
				return;
			}

			// 先判断外部是否有资源
			string path = FilePathHelp.GetWritableFilePath (filepath);
			if (File.Exists (path)) {
				handler (path);
				return;
			}

			// 再判断内部资源StreamingAssets
			path = FilePathHelp.GetStreamingFilePath (filepath);
			FilePathLoader.GetInstance().Load(path, (WWW www) =>{
				if (www != null) {
					handler(path);
				} else {
					handler(null);
				}
			});
		}
	}
}

