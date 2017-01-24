using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 资源包常量
	/// 
	/// 需要打包的文件路径为Iutput目录下的所有文件
	/// 打包后的资源会存放到StreamingAssets目录下
	/// </summary>
	public class AssetBundleConstants
	{
		/// <summary>
		/// 打包后的包名称
		/// 需小写字母
		/// </summary>
		public const string PackageName = "assets.ab";
		/// <summary>
		/// 路径
		/// </summary>
		public const string AssetBundleDirectory = "AssetBundle";
		/// <summary>
		/// 需要打包的目录
		/// </summary>
		public static string PackDirectory {
			get { 
				return Application.dataPath + "/AssetBundle/"; 
			}
		}

		/// <summary>
		/// 目录标识
		/// 用于解析配置中路径的前缀
		/// </summary>
		/// <value>The directory mark.</value>
		public static string DirectoryMark {
			get { 
				if (Application.platform == RuntimePlatform.WindowsEditor) {
					return "Assets/AssetBundle/";
				} else if (Application.platform == RuntimePlatform.OSXEditor) {
					return "Data/AssetBundle/";
				} else {
					return "Assets/AssetBundle/";
				}
			}
		}

		/// <summary>
		/// 打包后保存的的目录
		/// </summary>
		public static string SaveDirectory {
			get { 
				if (Application.platform == RuntimePlatform.WindowsEditor) {
					return "Assets/StreamingAssets/" + AssetBundleDirectory;
				} else if (Application.platform == RuntimePlatform.OSXEditor) {
					return "Data/Raw/"+ AssetBundleDirectory;
				} else {
					return "Assets/StreamingAssets/"+ AssetBundleDirectory;
				}
			}
		}

		/// <summary>
		/// 打包后存放的内部资源路径
		/// </summary>
		public static string AssetBundlePath {
			get {
				return FilePathHelp.GetStreamingFilePath("AssetBundle/" + PackageName);
			}
		}

		/// <summary>
		/// 打包后存放的外部资源路径
		/// </summary>
		public static string AssetBundlePath2 {
			get {
				return FilePathHelp.GetWritableFilePath("AssetBundle/" + PackageName);
			}
		}
	}
}

