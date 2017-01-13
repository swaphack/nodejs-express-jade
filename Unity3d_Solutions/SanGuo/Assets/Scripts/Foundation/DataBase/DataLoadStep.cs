using System;
using System.Xml;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据加载回调
	/// </summary>
	public delegate string DataLoadHandler(string filepath);
	/// <summary>
	/// 数据加载步骤
	/// </summary>
	public class DataLoadStep : IDataLoadStep
	{
		/// <summary>
		/// 表名
		/// </summary>
		private string _TableName;
		/// <summary>
		/// 配置路径
		/// </summary>
		private string _ConfigPath;
		/// <summary>
		/// 是否已加载
		/// </summary>
		private bool _IsLoaded;
		/// <summary>
		/// 表
		/// </summary>
		private IDataTable _Table;
		/// <summary>
		/// 数据加载处理
		/// </summary>
		private DataLoadHandler _DataLoadHandler;
		/// <summary>
		/// 主键名称
		/// </summary>
		private string _UniqueName;

		/// <summary>
		/// 获取表名称
		/// </summary>
		/// <returns>表名称</returns>
		public string TableName { get { return _TableName; } }

		/// <summary>
		/// 获取表名称
		/// </summary>
		/// <returns>表名称</returns>
		public string ConfigPath { get { return _ConfigPath; } }

		/// <summary>
		/// 是否已加载
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		public bool IsLoaded { get { return _IsLoaded; } }

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="tableName">表名称</param>
		/// <param name="configPath">配置路径</param>
		/// <param name="table">表</param>
		/// <param name="uniqueName">主键名称</param>
		public DataLoadStep (string tableName, string configPath, IDataTable table, DataLoadHandler handler, string uniqueName = "")
		{
			this._TableName = tableName;
			this._ConfigPath = configPath;
			this._Table = table;
			this._DataLoadHandler = handler;
			this._UniqueName = uniqueName;
			this._IsLoaded = false;
		}

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <returns><c>true</c> if this instance was loaded successfully; otherwise, <c>false</c>.</returns>
		public bool Load ()
		{
			if (_IsLoaded == true) {
				return true;
			}

			if (_DataLoadHandler == null) {
				return false;
			}

			string data = _DataLoadHandler (_ConfigPath);
			if (string.IsNullOrEmpty(data) == true) {
				return false;
			}

			_IsLoaded = true;

			XmlDocument document = new XmlDocument ();
			document.LoadXml (data);
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

			_Table.ClearRecords ();

			while (node != null) {
				IDataRecord record = new DataRecord ();
				int id = _Table.Count + 1;

				XmlAttributeCollection attributes = node.Attributes;
				int count = attributes.Count;
				for (int i = 0; i < count; i++) {
					XmlAttribute attribute = attributes [i];
					record.SetProperty (attribute.Name, attribute.Value);

					if (attribute.Name.Equals (_UniqueName)) {
						id = Convert.ToInt32 (attribute.Value);
					}
				}

				record.ID = id;
				record.InnerText = node.InnerText;

				_Table.AddRecord (record);

				node = node.NextSibling;
			}

			return true;
		}
	}
}

