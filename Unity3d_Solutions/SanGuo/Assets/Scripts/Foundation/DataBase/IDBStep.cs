using System;
using System.Xml;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据库加载步骤
	/// </summary>
	public interface IDBStep
	{
		/// <summary>
		/// 获取表名称
		/// </summary>
		/// <returns>表名称</returns>
		string GetTableName();
		/// <summary>
		/// 获取配置路径
		/// </summary>
		/// <returns>配置路径</returns>
		string GetConfigPath();
		/// <summary>
		/// 加载配置
		/// </summary>
		/// <returns><c>true</c> if this instance was loaded successfully; otherwise, <c>false</c>.</returns>
		void Load();
		/// <summary>
		/// 是否已加载
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		bool IsLoaded 
		{ 
			get; 
		}
	}
}

