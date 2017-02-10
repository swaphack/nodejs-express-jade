using System;
using System.Collections.Generic;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据表
	/// </summary>
	public class DataTable : IDataTable
	{
		/// <summary>
		/// 数据记录
		/// </summary>
		private Dictionary<int, IDataRecord> _Records;
		/// <summary>
		/// 编号记录
		/// </summary>
		private List<int> _UniqueIDs;
		/// <summary>
		/// 表名称
		/// </summary>
		private string _Name;

		public DataTable (string name)
		{
			_Name = name;

			_Records = new Dictionary<int, IDataRecord> ();

			_UniqueIDs = new List<int> ();
		}

		/// <summary>
		/// 获取名称
		/// </summary>
		/// <returns>表名称</returns>
		public string GetName ()
		{
			return _Name;
		}

		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="record">记录</param>
		public void AddRecord (IDataRecord record)
		{
			if (record == null) {
				return;
			}

			_Records [record.ID] = record;

			_UniqueIDs.Add (record.ID);
		}

		/// <summary>
		/// 移除记录
		/// </summary>
		/// <param name="record">记录编号</param>
		public void removeRecord (int recordID)
		{
			if (_Records.ContainsKey (recordID) == false) {
				return;
			}

			_Records.Remove (recordID);

			_UniqueIDs.Remove (recordID);
		}

		/// <summary>
		/// 根据索引查找记录
		/// </summary>
		/// <param name="index">索引</param>
		public IDataRecord At (int index)
		{
			if (_UniqueIDs.Count <= index || index < 0) {
				return null;
			}

			int id = _UniqueIDs [index];

			return _Records [id];
		}

		/// <summary>
		/// 记录数
		/// </summary>
		/// <value>记录数</value>
		public int Count { 
			get { 
				return _Records.Count;
			}
		}

		/// <summary>
		/// 根据编号查找记录
		/// </summary>
		/// <param name="uniqueID">编号</param>
		public IDataRecord Find (int uniqueID)
		{
			if (_Records.ContainsKey (uniqueID) == false) {
				return null;
			}

			return _Records [uniqueID];
		}

		/// <summary>
		/// 清空所有记录
		/// </summary>
		public void Clear ()
		{
			_Records.Clear ();
			_UniqueIDs.Clear ();
		}
	}
}

