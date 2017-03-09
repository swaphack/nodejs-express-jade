using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Data.Map
{
	/// <summary>
	/// 赛道配置
	/// </summary>
	public class TrackMap
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
		/// 资源包路径
		/// </summary>
		private string _AssetBundle;
		/// <summary>
		/// 配置所在路径
		/// </summary>
		private string _ConfigPath;
		/// <summary>
		/// 配置所在路径
		/// </summary>
		public string ConfigPath { 
			get { 
				return _ConfigPath;
			}
		}
		/// <summary>
		/// 原型
		/// </summary>
		public Dictionary<int, PrefabItem> PrefabItems {
			get { 
				return _PrefabItems;
			}
		}
		/// <summary>
		/// 赛道元素
		/// </summary>
		public List<ElementItem> ElementItems {
			get { 
				return _ElementItems;
			}
		}

		/// <summary>
		/// 资源包路径
		/// </summary>
		/// <value>The asset bundle.</value>
		public string AssetBundlePath {
			get { 
				return _AssetBundle;
			}
		}

		public TrackMap(string configpath)
		{
			_ConfigPath = configpath;
			_PrefabItems = new Dictionary<int, PrefabItem> ();
			_ElementItems = new List<ElementItem> ();
		}


		/// <summary>
		/// 加载原型
		/// </summary>
		/// <returns><c>true</c>, if prefab was loaded, <c>false</c> otherwise.</returns>
		/// <param name="node">Node.</param>
		private void LoadPrefab(XmlNode node)
		{
			if (node == null) {
				return;
			}

			XmlElement element = (XmlElement)node;
			_AssetBundle = element.GetAttribute ("Path");
			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				element = (XmlElement)itemNode;

				PrefabItem item = new PrefabItem();
				item.ID = Int32.Parse(element.GetAttribute ("ID"));
				item.Path = element.GetAttribute ("Path");
				_PrefabItems [item.ID] = item;

				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载元素
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadElement(XmlNode node)
		{
			if (node == null) {
				return;
			}

			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;

				ElementItem item = new ElementItem();
				item.Name = element.GetAttribute ("Name");
				item.PrefabID = Int32.Parse(element.GetAttribute ("PrefabID"));
				item.Position = StringHelp.ConvertToVector3 (element.GetAttribute ("Position"));
				_ElementItems.Add(item);

				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载
		/// </summary>
		public bool Load ()
		{
			XmlNode node = XmlHelp.LoadXMlRoot (ConfigPath);
			if (node == null) {
				return false;
			}

			this.Clear ();

			while (node != null) {
				if (node.Name == "Prefabs") {
					LoadPrefab (node);
				} else if (node.Name == "Elements") {
					LoadElement (node);
				}
				
				node = node.NextSibling;
			}

			return true;
		}
		/// <summary>
		/// 清空
		/// </summary>
		public void Clear ()
		{
			_PrefabItems.Clear ();
			_ElementItems.Clear ();
		}
	}
}

