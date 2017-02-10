using System;
using System.Xml;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据库加载步骤
	/// </summary>
	public interface IDataLoadStep
	{
		/// <summary>
		/// 获取表名称
		/// </summary>
		string TableName { get; }
		/// <summary>
		/// 获取配置路径
		/// </summary>
		/// <returns>配置路径</returns>
		string ConfigPath { get; }
		/// <summary>
		/// 是否已加载
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		bool IsLoaded { get; }
		/// <summary>
		/// 表数据
		/// </summary>
		/// <value>The table data.</value>
		IDataTable TableData { get;}
		/// <summary>
		/// 加载配置
		/// </summary>
		bool Load();
	}
}

