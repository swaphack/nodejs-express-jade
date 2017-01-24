using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// xml数据常量
	/// </summary>
	public class XmlDataConstants
	{
		/// <summary>
		/// 类名称
		/// </summary>
		public const string ClassName = "XmlFilePath";
		/// <summary>
		/// 文件名称
		/// </summary>
		public const string FileName = "XmlFilePath.cs";

		/// <summary>
		/// 需要打包的目录
		/// </summary>
		public static string PackDirectory {
			get { 
				return Application.dataPath + "/Resources/DataBase/"; 
			}
		}

		/// <summary>
		/// 打包后保存的的目录
		/// </summary>
		public static string SaveDirectory {
			get { 
				return Application.dataPath + "/Scripts/Data/" + FileName;
			}
		}

		/// <summary>
		/// 保存的本地路径
		/// </summary>
		/// <value>The without path.</value>
		public static string LocalPath {
			get { 
				return Application.dataPath + "/Resources/";
			}
		}
	}
}

