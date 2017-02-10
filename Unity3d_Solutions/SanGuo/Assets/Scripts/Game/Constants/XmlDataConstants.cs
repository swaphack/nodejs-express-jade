using System;
using UnityEngine;

namespace Game.Constants
{
	/// <summary>
	/// xml数据常量
	/// </summary>
	public class XmlDataConstants
	{
		/// <summary>
		/// 路径文件类名称
		/// </summary>
		public const string PathClassName = "XmlFilePath";
		/// <summary>
		/// 路径文件名称
		/// </summary>
		public const string PathFileName = "XmlFilePath.cs";
		/// <summary>
		/// 名称文件类名称
		/// </summary>
		public const string NameClassName = "XmlFileName";
		/// <summary>
		/// 名称文件名称
		/// </summary>
		public const string NameFileName = "XmlFileName.cs";

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
		public static string SavePathDirectory {
			get { 
				return Application.dataPath + "/Scripts/Data/" + PathFileName;
			}
		}

		/// <summary>
		/// 打包后保存的的目录
		/// </summary>
		public static string SaveNameDirectory {
			get { 
				return Application.dataPath + "/Scripts/Data/" + NameFileName;
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

