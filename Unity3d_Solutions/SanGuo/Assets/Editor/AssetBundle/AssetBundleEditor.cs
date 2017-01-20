using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using Game;

/// <summary>
/// 资源包
/// </summary>
public class AssetBundleEditor
{
	/// <summary>
	/// 打包 资源存放于StreamingAssets/AssetBundle目录下
	/// 文件格式
	/// *.prefab 或 (*.prefab|*.txt)
	/// </summary>
	/// <param name="packName">资源包名称</param>
	/// <param name="searchPath">搜索路径</param>
	/// <param name="searchFormat">文件格式</param>
	/// <param name="target">发布平台</param>
	public static void Pack(string packName, string searchPath, string searchFormat, BuildTarget target)
	{
		
		string localPackPath = Application.dataPath + "/" + searchPath;
		string localSavePath = Application.streamingAssetsPath + "/" + AssetBundleConstants.AssetBundlePath;
		string withoutPath = Application.dataPath;
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			string dir = "Assets";
			withoutPath = withoutPath.Substring (0, withoutPath.Length - dir.Length);
		} else if (Application.platform == RuntimePlatform.OSXEditor) {
			string dir = "Data";
			withoutPath = withoutPath.Substring (0, withoutPath.Length - dir.Length);
		}

		List<string> filePathList = new List<string> ();
		FilePathHelp.getFilePathsWithoutBase (localPackPath, filePathList, withoutPath, searchFormat);

		if (filePathList.Count == 0) {
			return;
		}

		AssetBundleBuild[] buildParameters = new AssetBundleBuild[1];
		buildParameters [0].assetBundleName = packName;
		buildParameters [0].assetNames = filePathList.ToArray ();

		BuildPipeline.BuildAssetBundles (localSavePath, buildParameters, BuildAssetBundleOptions.None, target);
	}
	/// <summary>
	/// 打包原型
	/// </summary>
	[@MenuItem("AssetBundle/PackPrefabs (Windows)")]
	public static void PackPrefabsWindows() {
		string packName = AssetBundleConstants.PrefabName;
		string searchPath = AssetBundleConstants.PrefabSearchDir;
		string searchFormat = "*.prefab";
		Pack (packName, searchPath, searchFormat, BuildTarget.StandaloneWindows);
	}

	/// <summary>
	/// 打包原型
	/// </summary>
	[@MenuItem("AssetBundle/PackPrefabs (Android)")]
	public static void PackPrefabsAndroid() {
		string packName = AssetBundleConstants.PrefabName;
		string searchPath = AssetBundleConstants.PrefabSearchDir;
		string searchFormat = "*.prefab";
		Pack (packName, searchPath, searchFormat, BuildTarget.Android);
	}
}