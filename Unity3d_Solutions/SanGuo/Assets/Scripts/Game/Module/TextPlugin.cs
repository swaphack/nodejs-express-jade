using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using Foundation.DataBase;
using Game;
using Game.Helper;
using Foundation.Plugin;

namespace Game.Module
{
	/// <summary>
	/// 本地文本
	/// Afrikaans = 0,
	/// Arabic = 1,
	/// Basque = 2,
	/// Belarusian = 3,
	/// Bulgarian = 4,
	/// Catalan = 5,
	/// Chinese = 6,
	/// Czech = 7,
	/// Danish = 8,
	/// Dutch = 9,
	/// English = 10,
	/// Estonian = 11,
	/// Faroese = 12,
	/// Finnish = 13,
	/// French = 14,
	/// German = 15,
	/// Greek = 16,
	/// Hebrew = 17,
	/// Hugarian = 18,
	/// Icelandic = 19,
	/// Indonesian = 20,
	/// Italian = 21,
	/// Japanese = 22,
	/// Korean = 23,
	/// Latvian = 24,
	/// Lithuanian = 25,
	/// Norwegian = 26,
	/// Polish = 27,
	/// Portuguese = 28,
	/// Romanian = 29,
	/// Russian = 30,
	/// SerboCroatian = 31,
	/// Slovak = 32,
	/// Slovenian = 33,
	/// Spanish = 34,
	/// Swedish = 35,
	/// Thai = 36,
	/// Turkish = 37,
	/// Ukrainian = 38,
	/// Vietnamese = 39,
	/// ChineseSimplified = 40,
	/// ChineseTraditional = 41,
	/// Unknown = 42,
	/// Hungarian = 18
	/// </summary>
	public class TextPlugin : IPlugin
	{
		/// <summary>
		/// 当前语言
		/// </summary>
		private SystemLanguage _SystemLanguage;
		/// <summary>
		/// 文本配置
		/// </summary>
		private Dictionary<SystemLanguage, string> _FilePaths;
		/// <summary>
		/// 文本
		/// </summary>
		private Dictionary<int, string> _Messages;

		/// <summary>
		/// 配置所在路径
		/// </summary>
		public const string ConfigPath = XmlFilePath.DataBaseConfigLanguage;

		public TextPlugin ()
		{
			_FilePaths = new Dictionary<SystemLanguage, string> ();
			_Messages = new Dictionary<int, string> ();
			_SystemLanguage = SystemLanguage.Unknown;
		}

		/// <summary>
		/// 获取当前语言
		/// </summary>
		/// <returns>The language.</returns>
		public SystemLanguage Language {
			get { 
				return _SystemLanguage; 
			}
			set { 
				_SystemLanguage = value;
				LoadLanguageLib ();
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
		/// 初始化语言包
		/// </summary>
		private void LoadLanguageLib ()
		{
			if (_FilePaths.ContainsKey (_SystemLanguage) == false) {
				return;
			}

			_Messages.Clear ();

			string xmlUrl = _FilePaths [_SystemLanguage];
			IDataTable table = XmlHelp.LoadSimpleXml (xmlUrl);
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

		/// <summary>
		/// 加载语言配置
		/// </summary>
		private void LoadLanguageConfig()
		{
			_FilePaths.Clear ();

			IDataTable table = XmlHelp.LoadSimpleXml (ConfigPath);
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

				_FilePaths [(SystemLanguage)id] = value;
			}

			Log.Info ("Load Message Config Succesful");
		}

		/// <summary>
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.Text;
			} 
		}

		/// <summary>
		/// 初始化配置
		/// </summary>
		public void Init ()
		{
			LoadLanguageConfig ();
			Language = Application.systemLanguage;
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			
		}
		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_FilePaths.Clear ();
			_Messages.Clear ();
		}
	}
}
