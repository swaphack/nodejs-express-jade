using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Foundation.DataBase;

namespace Game.Helper
{
	/// <summary>
	/// 用户本地配置
	/// </summary>
	public class UserDefault
	{
		/// <summary>
		/// 文档名称
		/// </summary>
		public const string FilePath = "UserDefault.xml";
		/// <summary>
		/// 文档名称
		/// </summary>
		public const string FileName= "UserDefault";
		/// <summary>
		/// <summary>
		/// 配置字段
		/// </summary>
		public const string FieldItem = "Item";
		/// <summary>
		/// 用户配置实例
		/// </summary>
		private static UserDefault sUserDefault;

		/// <summary>
		/// 键值对配置
		/// </summary>
		private Dictionary<string, string> _Configs;

		private UserDefault ()
		{
			_Configs = new Dictionary<string, string> ();
		}

		public static UserDefault GetInstance() 
		{
			if (sUserDefault == null) {
				sUserDefault = new UserDefault ();
				sUserDefault.Load ();
			}			

			return sUserDefault;
		}

		/// <summary>
		/// 保存配置
		/// </summary>
		private void Save()
		{
			string fullpath = FilePathHelp.GetWritableFilePath (FilePath);
			if (string.IsNullOrEmpty (fullpath) == true) {
				return;
			}

			XmlDocument doc = new XmlDocument ();
			XmlDeclaration declaration = doc.CreateXmlDeclaration ("1.0", "UTF-8", "yes");
			doc.AppendChild (declaration);
			XmlElement element = doc.CreateElement ("Root");
			doc.AppendChild (element);
			foreach (KeyValuePair<string, string> item in _Configs) {
				XmlElement child = doc.CreateElement(FieldItem);
				child.SetAttribute (item.Key, item.Value);
				element.AppendChild (child);
			}

			doc.Save (fullpath);
		}

		/// <summary>
		/// 加载配置
		/// </summary>
		private void Load()
		{
			IDataTable table = XmlHelp.LoadSimpleXml (FileName);
			if (table == null) {
				return;
			}

			for (int i = 0; i < table.Count; i++) {
				IDataRecord record = table.At (i);
				List<string> keys = record.Keys;
				for (int j = 0; j < keys.Count; j++) {
					_Configs [keys [j]] = record.GetProperty (keys [j]);
				}
			}

			Log.Info ("Load User Info");
		}

		/// <summary>
		/// 设置值
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void Set(string key, string value) 
		{
			if (string.IsNullOrEmpty (key) == true || value == null) {
				return;
			}
			bool bNeedSave = false;
			if (_Configs.ContainsKey (key) == false) {
				bNeedSave = true;
			}
			else if (_Configs [key] != value) {
				bNeedSave = true;
			}
			_Configs [key] = value;
			if (bNeedSave) {
				Save ();
			}
		}

		/// <summary>
		/// 获取值
		/// </summary>
		/// <param name="key">Key.</param>
		public string Get(string key)
		{
			if (string.IsNullOrEmpty (key) == true) {
				return null;
			}

			if (_Configs.ContainsKey (key) == false) {
				return null;
			}

			return _Configs[key];
		}

		/// <summary>
		/// 设置值
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void SetInteger(string key, int value) 
		{
			Set (key, value.ToString ());
		}

		/// <summary>
		/// 获取整形
		/// </summary>
		/// <returns>The integer.</returns>
		/// <param name="key">Key.</param>
		public int GetInteger(string key) 
		{
			string value = Get (key);
			if (string.IsNullOrEmpty (value)) {
				return 0;
			}

			int result = 0;
			if (int.TryParse (value, out result)) {
				return result;
			}

			return 0;
		}

		public void Flush()
		{
			this.Save ();
		}
	}
}

