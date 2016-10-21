using System;
using System.Collections.Generic;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据表
	/// </summary>
	public class DBTable : IDBTable
	{
		/// <summary>
		/// 数据记录
		/// </summary>
		private Dictionary<int, IDBRecord> _Records;
		/// <summary>
		/// 表名称
		/// </summary>
		private string _Name;

		public DBTable (string name)
		{
			_Name = name;

			_Records = new Dictionary<int, IDBRecord> ();
		}

		/// <summary>
		/// 获取名称
		/// </summary>
		/// <returns>表名称</returns>
		public string GetName()
		{
			return _Name;
		}
		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="record">记录</param>
		public void AddRecord (IDBRecord record)
		{
			if (record == null) 
			{
				return;
			}

			_Records [record.GetID ()] = record;
		}

		/// <summary>
		/// 移除记录
		/// </summary>
		/// <param name="record">记录编号</param>
		public void removeRecord(int recordID)
		{
			if (_Records.ContainsKey (recordID) == false) 
			{
				return;
			}

			_Records.Remove (recordID);
			
		}
		/// <summary>
		/// 清空所有记录
		/// </summary>
		public void ClearRecords()
		{
			_Records.Clear ();
		}
	}
}

