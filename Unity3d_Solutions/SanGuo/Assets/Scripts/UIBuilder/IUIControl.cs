using System;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	/// <summary>
	/// 控件解析接口
	/// </summary>
	public interface IUIControl
	{
		/// <summary>
		/// 游戏对象
		/// </summary>
		string Name { get; }

		/// <summary>
		/// xml节点
		/// </summary>
		/// <value>The node.</value>
		XmlNode Node { get; set; }

		/// <summary>
		/// 控件
		/// </summary>
		Behaviour Control { get; set; }

		/// <summary>
		/// 解析xml节点
		/// </summary>
		/// <returns><c>true</c>, if xml node was loaded, <c>false</c> otherwise.</returns>
		/// <param name="node">Node.</param>
		bool LoadXmlNode();

		/// <summary>
		/// 生成xml节点
		/// </summary>
		/// <returns><c>true</c>, if xml node was saved, <c>false</c> otherwise.</returns>
		bool SaveXmlNode();
	}
}

