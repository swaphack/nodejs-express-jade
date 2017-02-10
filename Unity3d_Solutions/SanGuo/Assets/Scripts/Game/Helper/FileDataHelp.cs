using System;
using System.IO;
using UnityEngine;
using Game.Helper;
using Game.Local;
using Game.Constants;

namespace Game.Helper
{
	/// <summary>
	/// 文件数据
	/// </summary>
	public class FileDataHelp
	{
		private FileDataHelp ()
		{
		}

		/// <summary>
		/// 获取xml文本数据
		/// </summary>
		/// <returns>The xml file data.</returns>
		/// <param name="filepath">Filepath.</param>
		public static string GetXmlFileData(string filepath)
		{
			return AssetLoader.GetTextData (filepath, FormatConstants.XmlFormat);
		}

		/// <summary>
		/// 创建Prefab的实例化对象
		/// </summary>
		/// <param name="assetBundleName">资源包名称</param>
		/// <param name="filePath">资源路径|标识</param>
		/// <param name="handler">加载回调</param>
		public static void LoadAssetBundle(string assetBundleName, OnBooleanCallback handler)
		{
			string abPath = AssetBundleConstants.AssetBundleDirectory + "/" + assetBundleName;
			AssetBundleLoader.GetAssetBundleFullPath (abPath, (string fullPath)=>{
				if (!string.IsNullOrEmpty (fullPath)) {
					AssetBundleLoader.GetInstance ().LoadAssetBundle (fullPath, handler);
				} else {
					handler(true);
				}
			});
		}

		/// <summary>
		/// 创建Prefab的实例化对象
		/// </summary>
		/// <param name="assetBundleName">资源包名称</param>
		/// <param name="fileMark">资源路径|标识</param>
		/// <param name="handler">加载prefab回调</param>
		public static bool CreatePrefabFromAssetBundle(string assetBundleName, string fileMark, OnGameObjectCallback handler)
		{
			if (handler == null) {
				return false;
			}
			if (string.IsNullOrEmpty (assetBundleName) || string.IsNullOrEmpty (fileMark)) {
				return false;
			}
			string abPath = AssetBundleConstants.AssetBundleDirectory + "/" + assetBundleName;
			AssetBundleLoader.GetAssetBundleFullPath (abPath, (string fullPath)=>{
				if (!string.IsNullOrEmpty (fullPath)) {
					string assetPath = AssetBundleConstants.DirectoryMark + fileMark;
					GameObject prefab = AssetBundleLoader.GetInstance ().LoadGameObject (fullPath, assetPath);
					handler (prefab);
				} else {
					handler (null);
				}
			});

			return true;
		}

		/// <summary>
		/// 创建Prefab的实例化对象
		/// </summary>
		/// <param name="filePath">资源路径|标识</param>
		/// <param name="handler">加载prefab回调</param>
		public static bool CreatePrefabFromAsset(string filePath, OnGameObjectCallback handler)
		{
			if (handler == null) {
				return false;
			}
			if (string.IsNullOrEmpty (filePath)) {
				return false;
			}
			AssetLoader.GetAssetFullPath (filePath, (string fullPath)=>{
				if (!string.IsNullOrEmpty (fullPath)) {
					Log.Info ("Prefab File Name : " + fullPath);
					if (fullPath.Contains(".")){
						// 可读写路径文件
						FilePathLoader.GetInstance().Load(fullPath, (WWW www)=>{
							if (www == null) {
								handler(null);
							}
							AssetBundle ab = www.assetBundle;
							if (ab == null) {
								handler(null);
							}
							GameObject prefab = ab.LoadAsset<GameObject>(ab.GetAllAssetNames()[0]);
							handler(prefab);	
						});
					} else {
						// 包内数据
						GameObject prefab = (GameObject)Resources.Load<GameObject> (fullPath);	
						handler (prefab);
					}
				} else {
					handler (null);
				}
			});

			return true;
		}
	}
}

