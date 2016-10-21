using System;
using System.Xml;

namespace Foundation.DataBase
{
	public class DBStep : IDBStep
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
		private IDBTable _Table;

		public DBStep (string tableName, string configPath, IDBTable table)
		{
			this._TableName = tableName;
			this._ConfigPath = configPath;
			this._Table = table;
			this._IsLoaded = false;
		}

		/// <summary>
		/// 获取表名称
		/// </summary>
		/// <returns>表名称</returns>
		public string GetTableName()
		{
			return _TableName;
		}
		/// <summary>
		/// 获取配置路径
		/// </summary>
		/// <returns>配置路径</returns>
		public string GetConfigPath()
		{
			return _ConfigPath;
		}
		/// <summary>
		/// 加载配置
		/// </summary>
		/// <returns><c>true</c> if this instance was loaded successfully; otherwise, <c>false</c>.</returns>
		public void Load()
		{
			if (_IsLoaded == true) 
			{
				return;
			}

			_IsLoaded = true;
			_Table.ClearRecords ();

			XmlDocument documnet = new XmlDocument ();
			documnet.Load (GetConfigPath ());
			XmlNode root = documnet.FirstChild;
			if (root == null) 
			{
				return;			
			}

			XmlNode node = root.FirstChild;

			while (node != null) 
			{
				IDBRecord record = new DBRecord ();

				XmlAttributeCollection attributes = node.Attributes;
				int count = attributes.Count;
				for (int i = 0; i < count; i++) 
				{
					XmlAttribute attribute = attributes [i];
					record.SetProperty (attribute.Name, attribute.Value);
				}

				_Table.AddRecord (record);
			}
		}
		/// <summary>
		/// 是否已加载
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		public bool IsLoaded 
		{ 
			get {  return _IsLoaded; }
		}
	}
}

