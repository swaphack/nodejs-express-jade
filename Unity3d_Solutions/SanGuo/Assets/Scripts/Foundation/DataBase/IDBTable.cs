using System;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据表
	/// </summary>
	public interface IDBTable
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
		void AddRecord (IDBRecord record);
		/// <summary>
		/// 移除记录
		/// </summary>
		/// <param name="recordID">记录编号</param>
		void removeRecord(int recordID);
		/// <summary>
		/// 清空所有记录
		/// </summary>
		void ClearRecords();
	}
}

