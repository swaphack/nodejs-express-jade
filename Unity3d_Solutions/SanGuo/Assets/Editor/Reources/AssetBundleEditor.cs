using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using Game;

/// <summary>
/// 资源包编辑
/// 打包后默认会生成AssetBundle文件
/// 包名称不可与AssetBundle相同
/// </summary>
public class AssetBundleEditor
{
	/// <summary>
	/// 打包
	/// </summary>
	/// <param name="packName">资源包名称</param>
	/// <param name="searchPath">要打包的路径</param>
	/// <param name="packPath">打包后存放的路径</param>
	/// <param name="target">发布平台</param>
	public static void Pack(string packName, string searchPath, string packPath, BuildTarget target)
	{
		
		string localPackPath = searchPath;
		string localSavePath = packPath;
		string withoutPath = Application.dataPath;

		if (Application.platform == RuntimePlatform.WindowsEditor) {
			string dir = "Assets";
			withoutPath = withoutPath.Substring (0, withoutPath.Length - dir.Length);
		} else if (Application.platform == RuntimePlatform.OSXEditor) {
			string dir = "Data";
			withoutPath = withoutPath.Substring (0, withoutPath.Length - dir.Length);
		}

		List<string> filePathList = new List<string> ();
		FilePathHelp.getFilePaths (localPackPath, filePathList, withoutPath);

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
	[MenuItem("Reources/AssetBundle/Pack (Windows)")]
	public static void PackPrefabsWindows() 
	{
		string packName = AssetBundleConstants.PackageName;
		string searchPath = AssetBundleConstants.PackDirectory;
		string packPath = AssetBundleConstants.SaveDirectory;
		Pack (packName, searchPath, packPath, BuildTarget.StandaloneWindows);
	}

	/// <summary>
	/// 打包原型
	/// </summary>
	[MenuItem("Reources/AssetBundle/Pack (Android)")]
	public static void PackPrefabsAndroid() 
	{
		string packName = AssetBundleConstants.PackageName;
		string searchPath = AssetBundleConstants.PackDirectory;
		string savePath = AssetBundleConstants.SaveDirectory;
		Pack (packName, searchPath, savePath, BuildTarget.Android);
	}
}