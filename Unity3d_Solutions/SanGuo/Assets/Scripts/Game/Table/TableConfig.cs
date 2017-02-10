using System;
using Foundation.DataBase;
using Game.Helper;
using Data;

namespace Game.Table
{
	/// <summary>
	/// 数据配置
	/// </summary>
	public class TableConfig : DataLoadStep
	{
		/// <summary>
		/// 主键
		/// </summary>
		public const string UniqueName = "ID";

		public TableConfig(string tableName, string configPath)
		{
			_TableName = tableName;
			_ConfigPath = configPath;
			_TableData = new DataTable (tableName);
			_DataLoadHandler = delegate (string filepath) {
				return FileDataHelp.GetXmlFileData (filepath);
			};

			_UniqueName = UniqueName;
		}
	}
}

