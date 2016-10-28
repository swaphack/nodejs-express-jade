using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	/// <summary>
	/// UI对象解析
	/// </summary>
	public abstract class UIControl : IUIControl
	{
		/// <summary>
		/// 控件
		/// </summary>
		private Behaviour _Control;
		/// <summary>
		/// xml节点
		/// </summary>
		private XmlNode _Node;
		/// <summary>
		/// 属性
		/// </summary>
		private UIProperty _Property;
		/// <summary>
		/// 名称
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// 游戏对象
		/// </summary>
		public Behaviour Control { 
			get { 
				return _Control ; 
			}
			set { 
				_Control = value;
			}
		}

		/// <summary>
		/// xml节点
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node { 
			get { 
				return _Node;
			}
			set { 
				_Node = value;
			}
		}

		/// <summary>
		/// 属性
		/// </summary>
		/// <value>The property.</value>
		public UIProperty Property {
			get { 
				return _Property;
			}
		}

		public UIControl()
		{
			_Property = new UIProperty ();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public abstract void Init();

		/// <summary>
		/// 解析xml节点
		/// </summary>
		/// <returns><c>true</c>, if xml node was loaded, <c>false</c> otherwise.</returns>
		/// <param name="node">Node.</param>
		public bool LoadXmlNode()
		{
			Init ();

			XmlAttributeCollection collect = Node.Attributes;

			foreach (XmlAttribute attribute in collect) {
				_Property.SetValue (attribute.Name, attribute.Value);
			}

			return true;
		}

		/// <summary>
		/// 生成xml节点
		/// </summary>
		/// <returns><c>true</c>, if xml node was saved, <c>false</c> otherwise.</returns>
		public bool SaveXmlNode()
		{
			if (Control == null || Node == null) {
				return false;
			}
			
			MakeAttributes ();

			return true;
		}

		/// <summary>
		/// 获取属性
		/// </summary>
		/// <returns><c>true</c>, if attribute was gotten, <c>false</c> otherwise.</returns>
		/// <param name="name">名称</param>
		/// <param name="value">值</param>
		protected bool GetAttribute(string name, out string value)
		{
			value = _Property.GetValue (name);

			if (value == null) {
				return false;
			}

			return true;
		}


		/// <summary>
		/// 解析属性
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="value">值</param>
		protected virtual void ParseAttributes()
		{
			
		}

		/// <summary>
		/// 生成属性
		/// </summary>
		protected virtual void MakeAttributes()
		{
		}
	}
}

