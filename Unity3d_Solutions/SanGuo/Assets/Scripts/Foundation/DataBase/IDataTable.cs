using System;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据表
	/// </summary>
	public interface IDataTable
	{
		/// <summary>
		/// 获取名称
		/// </summary>
		/// <returns>表名称</returns>
		string GetName();
		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="record">记录</param>
		void AddRecord (IDataRecord record);
		/// <summary>
		/// 移除记录
		/// </summary>
		/// <param name="recordID">记录编号</param>
		void removeRecord(int recordID);
		/// <summary>
		/// 根据索引查找记录
		/// </summary>
		/// <param name="index">索引</param>
		IDataRecord At (int index);
		/// <summary>
		/// 记录数
		/// </summary>
		/// <value>记录数</value>
		int Count { get; }
		/// <summary>
		/// 根据编号查找记录
		/// </summary>
		/// <param name="uniqueID">编号</param>
		IDataRecord Find (int uniqueID);
		/// <summary>
		/// 清空所有记录
		/// </summary>
		void Clear();
	}
}

