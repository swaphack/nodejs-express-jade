using System;
using System.Xml;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Data
{
	/// <summary>
	/// 赛道配置
	/// </summary>
	public class TrackConfig : IConfig
	{
		/// <summary>
		/// 原型项
		/// </summary>
		public struct PrefabItem
		{
			/// <summary>
			/// 编号
			/// </summary>
			public int ID;
			/// <summary>
			/// 资源路径
			/// </summary>
			public string Path;
		}

		/// <summary>
		/// 赛道元素项
		/// </summary>
		public struct ElementItem
		{
			/// <summary>
			/// 资源路径
			/// </summary>
			public string Name;
			/// <summary>
			/// 编号
			/// </summary>
			public int PrefabID;
			/// <summary>
			/// 位置
			/// </summary>
			public Vector3 Position;
		}
			
		/// <summary>
		/// 原型
		/// </summary>
		private Dictionary<int, PrefabItem> _PrefabItems;
		/// <summary>
		/// 赛道元素
		/// </summary>
		private List<ElementItem> _ElementItems;
		/// <summary>
		/// 配置所在路径
		/// </summary>
		public string ConfigPath { 
			get { 
				return "DataBase/Map/Trace";
			}
		}

		public TrackConfig()
		{
			_PrefabItems = new Dictionary<int, PrefabItem> ();
			_ElementItems = new List<ElementItem> ();
		}

		/// <summary>
		/// 加载
		/// </summary>
		public bool Load ()
		{
			string xmlData = FilePathHelp.GetXmlFileData (ConfigPath);
			if (string.IsNullOrEmpty (xmlData)) {
				return false;
			}

			XmlDocument document = new XmlDocument ();
			document.LoadXml (xmlData);
			XmlNode root = document.FirstChild;
			if (root == null) {
				return false;		
			}

			XmlNode node = root.NextSibling;
			if (node == null) {
				return false;		
			}

			node = node.FirstChild;
			if (node == null) {
				return false;		
			}

			this.Clear ();


			return true;
		}
		/// <summary>
		/// 清空
		/// </summary>
		public bool Clear ()
		{
			_PrefabItems.Clear ();
			_ElementItems.Clear ();
			return true;
		}
	}
}

