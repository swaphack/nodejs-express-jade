using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game;

namespace UI
{
	/// <summary>
	/// xml格式的ui代理
	/// </summary>
	public class UIProxy
	{
		/// <summary>
		/// 实例
		/// </summary>
		private static UIProxy s_Proxy;
		/// <summary>
		/// 控件解析方法
		/// </summary>
		private Dictionary<string, IUIControl> _ControlParsers;

		private UIProxy ()
		{
			_ControlParsers = new Dictionary<string, IUIControl> ();
		}

		/// <summary>
		/// 代理实例
		/// </summary>
		/// <returns>The instance.</returns>
		public static UIProxy GetInstance() 
		{
			if (s_Proxy == null) {
				s_Proxy = new UIProxy ();
			}

			return s_Proxy;
		}

		/// <summary>
		/// 注册canvas节点解析器
		/// </summary>
		/// <param name="parser">canvas节点解析方法</param>
		public void RegisterCanvasResolover(IUIControl parser)
		{
			if (parser == null || string.IsNullOrEmpty (parser.Name) == true) {
				return;
			}

			_ControlParsers [parser.Name] = parser;
		}

		/// <summary>
		/// 注销canvas节点解析器
		/// </summary>
		/// <param name="name">canvas节点名称</param>
		public void UnregisterCanvasResolover(string name)
		{
			_ControlParsers.Remove (name);
		}

		/// <summary>
		/// 加载文件
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		public Behaviour Load(string filepath)
		{
			string fullpath = FilePathUtility.GetFullPath (filepath);
			if (fullpath == "") {
				return null;
			}

			XmlDocument doc = new XmlDocument ();
			doc.Load (fullpath);

			return LoadXmlDocument(doc);
		}

		/// <summary>
		/// 保存节点到文件中
		/// </summary>
		/// <param name="control">控件</param>
		/// <param name="filePath">保存文件</param>
		public bool Save(UIBehaviour control, string filePath)
		{
			if (control == null) {
				return false;
			}

			string fullpath = FilePathUtility.GetDataFilePath (filePath);

			FileStream fileStream = File.Create (fullpath);
			fileStream.Close ();

			XmlDocument doc = new XmlDocument ();
			XmlNode node = SaveXmlNode (control, doc);
			if (node != null) {
				doc.AppendChild (node);
			}
			doc.Save (fullpath);

			return true;
		}


		/// <summary>
		/// 加载xml配置
		/// </summary>
		/// <returns>游戏对象</returns>
		/// <param name="doc">xml配置</param>
		public Behaviour LoadXmlDocument(XmlDocument doc)
		{
			if (doc == null) {
				return null;
			}

			XmlNode node = doc.FirstChild;
			if (node == null) {
				return null;	
			}

			return LoadXmlNode (node);
		}

		/// <summary>
		/// 加载节点
		/// </summary>
		/// <returns>The xml node.</returns>
		/// <param name="node">Node.</param>
		protected Behaviour LoadXmlNode(XmlNode node)
		{
			if (node == null) {
				return null;
			}

			if (_ControlParsers.ContainsKey (node.Name) == false) {
				return null;
			}

			IUIControl control = _ControlParsers [node.Name];
			control.Node = node;

			if (control.LoadXmlNode () == false) {
				return null;
			}

			Behaviour gameObject = control.Control;

			XmlNode child = node.FirstChild;

			while (child != null) {
				Behaviour childObject = LoadXmlNode (child);
				if (childObject) {
					childObject.transform.SetParent (gameObject.transform);
				}

				child = child.NextSibling;
			}


			return gameObject;
		}

		/// <summary>
		/// 将节点转成xml格式
		/// </summary>
		/// <returns>The xml node.</returns>
		/// <param name="control">控件</param>
		/// <param name="doc">xml文档</param>
		protected XmlNode SaveXmlNode(UIBehaviour control, XmlDocument doc)
		{
			if (control == null || doc == null) {
				return null;
			}

			Type type = control.GetType ();

			if (_ControlParsers.ContainsKey (type.Name) == false) {
				return null;
			}

			XmlElement node = doc.CreateElement (type.Name);

			IUIControl creater = _ControlParsers [type.Name];
			creater.Control = control;
			creater.Node = node;

			if (creater.SaveXmlNode () == false) {
				return null;
			}

			int count = control.transform.childCount;

			for (int i = 0; i < count; i++) {
				UIBehaviour child = control.transform.GetChild (i).GetComponent<UIBehaviour> ();
				if (child) {
					XmlNode childNode = SaveXmlNode (child, doc);
					if (childNode != null) {
						node.AppendChild (childNode);
					}
				}
			}

			return node;
		}
	}
}

