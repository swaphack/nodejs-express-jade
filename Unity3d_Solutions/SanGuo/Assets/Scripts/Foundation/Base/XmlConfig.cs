using System;
using System.Xml;

namespace Foundation
{
	/// <summary>
	/// xml配置简单获取
	/// </summary>
	public class XmlConfig
	{
		private XmlDocument _Document;

		public XmlConfig ()
		{
			_Document = new XmlDocument ();
		}

		/// <summary>
		/// 加载xml数据
		/// </summary>
		/// <param name="fileData">配置数据</param>
		public void LoadData(string fileData)
		{
			_Document.LoadXml (fileData);
		}

		/// <summary>
		/// 获取节点属性值
		/// </summary>
		/// <returns>属性值</returns>
		/// <param name="nodepath">节点递归路径,路径："a.b.c"</param>
		/// <param name="attribute">属性名称</param>
		public string GetValue(string nodepath, string attribute)
		{
			XmlNode node = _Document.FirstChild;
			if (node == null) {
				return null;
			}

			node = node.NextSibling;
			if (node == null) {
				return null;
			}

			nodepath.Replace ('.', '/');
			nodepath.Insert (0, "/");
			nodepath += "[@";
			nodepath += attribute;
			nodepath += "]";

			XmlNode dest = node.SelectSingleNode (nodepath);
			if (dest == null) {
				return null;
			}

			return dest.Value;
		}
	}
}

