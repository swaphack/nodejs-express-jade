using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using Foundation.DataBase;

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
		/// 配置所在路径
		/// </summary>
		public const string ConfigPath = "DataBase/Config/Language";

		public Message ()
		{
			_LanguagueType = LanguagueType.NONE;
			_FilePaths = new Dictionary<LanguagueType, string> ();
			_Messages = new Dictionary<int, string> ();
		}

		/// <summary>
		/// 设置当前语言
		/// </summary>
		/// <param name="eLan">E lan.</param>
		public void SetLanguage (LanguagueType eLanguagueType)
		{
			if (_LanguagueType == eLanguagueType) {
				return;
			}
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

			DataTable table = XmlHelp.LoadXml (ConfigPath);
			if (table == null) {
				return;
			}

			string value;
			int id = 0;
			for (int i = 0; i < table.Count; i++) {
				IDataRecord record = table.At (i);
				value = record.GetProperty ("ID");
				if (int.TryParse (value, out id) == false) {
					continue;
				}
				value = record.GetProperty ("Path");
				if (value == null) {
					continue;
				}

				_FilePaths [(LanguagueType)id] = value;
			}

			Log.Info ("Load Message Config Succesful");
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
			DataTable table = XmlHelp.LoadXml (xmlUrl);
			if (table == null) {
				return;
			}

			int key = 0;
			string value = null;
			for (int i = 0; i < table.Count; i++) {
				IDataRecord record = table.At (i);
				value = record.GetProperty ("ID");
				if (int.TryParse (value, out key) == false) {
					continue;
				}
				value = record.InnerText;
				if (value == null) {
					continue;
				}

				_Messages [key] = value;
			}

			Log.Info ("Load Message Language Succesful");
		}
	}
}
