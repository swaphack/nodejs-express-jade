using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
	/// <summary>
	/// 本地文本
	/// </summary>
	public class Message
	{
		/// <summary>
		/// 当前语言
		/// </summary>
		private LanguagueType _LanguagueType;
		/// <summary>
		/// 文本配置
		/// </summary>
		private Dictionary<LanguagueType, string> _FilePaths;
		/// <summary>
		/// 文本
		/// </summary>
		private Dictionary<int, string> _Messages;

		/// <summary>
		/// 配置文件
		/// </summary>
		/// <value>The config path.</value>
		public string ConfigPath { get { return  "DataBase/Config/Language.xml"; } }


		public Message ()
		{
			_LanguagueType = LanguagueType.CHINA;
			_FilePaths = new Dictionary<LanguagueType, string> ();
			_Messages = new Dictionary<int, string> ();
		}

		/// <summary>
		/// 设置当前语言
		/// </summary>
		/// <param name="eLan">E lan.</param>
		public void SetLanguage (LanguagueType eLanguagueType)
		{
			_LanguagueType = eLanguagueType;
			initLanguage ();
		}

		/// <summary>
		/// 获取当前语言
		/// </summary>
		/// <returns>The language.</returns>
		public LanguagueType Language {
			get { 
				return _LanguagueType; 
			}
			set { 
				SetLanguage (value);
			}
		}

		/// <summary>
		/// 获取消息文本
		/// </summary>
		/// <returns>消息文本</returns>
		/// <param name="id">消息编号</param>
		public string GetMessage (int id)
		{
			if (_Messages.ContainsKey (id) == false) {
				return "";
			}

			return _Messages [id];
		}

		/// <summary>
		/// 初始化配置
		/// </summary>
		public void InitConfig ()
		{
			_FilePaths.Clear ();

			string fullpath = FilePathUtility.GetFullPath (ConfigPath);

			if (string.IsNullOrEmpty (fullpath)) {
				return;
			}

			XmlDocument doc = new XmlDocument ();
			doc.Load (fullpath);

			XmlNode root = doc.FirstChild;
			if (root == null) 
			{
				return;
			}

			root = root.NextSibling;
			if (root == null) 
			{
				return;
			}
			XmlNodeList nodeList = root.ChildNodes;

			string value;
			int id = 0;
			foreach (XmlNode node in nodeList) {
				XmlElement element = (XmlElement)node;
				value = element.GetAttribute ("ID");
				if (int.TryParse (value, out id) == false) {
					continue;
				}
				value = element.GetAttribute ("Path");
				if (value == null) {
					continue;
				}

				_FilePaths [(LanguagueType)id] = value;
			}
		}

		/// <summary>
		/// 初始化语言包
		/// </summary>
		private void initLanguage ()
		{
			if (_FilePaths.ContainsKey (_LanguagueType) == false) {
				return;
			}

			_Messages.Clear ();

			string xmlUrl = _FilePaths [_LanguagueType];
			string fullpath = FilePathUtility.GetFullPath (xmlUrl);
			if (string.IsNullOrEmpty (fullpath)) {
				return;
			}

			XmlDocument doc = new XmlDocument ();
			doc.Load (fullpath);

			XmlNode root = doc.FirstChild;
			if (root == null) 
			{
				return;
			}

			root = root.NextSibling;
			if (root == null) 
			{
				return;
			}
			XmlNodeList nodeList = root.ChildNodes;

			int key = 0;
			string value = null;
			foreach (XmlNode node in nodeList) {
				XmlElement element = (XmlElement)node;
				value = element.GetAttribute ("ID");
				if (int.TryParse (value, out key)) {
					_Messages [key] = element.InnerText;
				}
			}
		}
	}
}
