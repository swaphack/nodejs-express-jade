using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Game;

/// <summary>
/// xml数据编辑
/// </summary>
public class XmlDataEditor
{
	/// <summary>
	/// 创建c#文件
	/// </summary>
	/// <param name="savePath">Save path.</param>
	/// <param name="xmlFilePath">Xml file path.</param>
	public static void CreateCSharpStringFile(string savePath, List<string> xmlFilePath)
	{
		StringBuilder builder = new StringBuilder ();
		builder.Append ("using System;\n\n");
		builder.Append ("public class " + XmlDataConstants.ClassName + "\n");
		builder.Append ("{\n");
		foreach(string item in xmlFilePath)
		{
			string target = item;
			string[] strAry = target.Split ('/');
			string fieldName = "";
			foreach (string word in strAry) 
			{
				if (word.Length > 1)
				{
					fieldName = fieldName + word[0].ToString().ToUpper() + word.Substring(1);
				}
				else
				{
					fieldName = fieldName + word.ToUpper();
				}
			}

			int index = fieldName.IndexOf ('.');
			if (index != -1) {
				fieldName = fieldName.Substring(0, index);
			}

			index = target.IndexOf ('.');
			if (index != -1) {
				target = target.Substring(0, index);
			}
			builder.Append ("\tpublic const string " + fieldName + " = \"" + target + "\";\n\t\n");
		}
		builder.Append ("}\n");

		File.WriteAllText (savePath, builder.ToString ());
	}

	/// <summary>
	/// 生成xml路径文件
	/// </summary>
	[MenuItem("Reources/Xml/CreatePathFile")]
	public static void CreateXmlPathFile()
	{
		string searchPath = XmlDataConstants.PackDirectory;
		string savePath = XmlDataConstants.SaveDirectory;
		string withoutPath = XmlDataConstants.LocalPath;

		List<string> filePathList = new List<string> ();
		FilePathHelp.getFilePaths (searchPath, filePathList, withoutPath);

		if (filePathList.Count == 0) {
			return;
		}

		CreateCSharpStringFile (savePath, filePathList);
	}
}

