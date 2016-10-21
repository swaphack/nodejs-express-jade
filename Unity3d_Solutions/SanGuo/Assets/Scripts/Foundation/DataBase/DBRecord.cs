using System;
using System.Text;
using System.Collections.Generic;

namespace Foundation.DataBase
{
	public class DBRecord : IDBRecord
	{
		/// <summary>
		/// 编号
		/// </summary>
		private int _ID;
		/// <summary>
		/// 成员
		/// </summary>
		private Dictionary<string, string> _Members;

		public DBRecord ()
		{
			_ID = 0;
			_Members = new Dictionary<string, string> ();
		}

		/// <summary>
		/// 获取编号
		/// </summary>
		/// <returns>记录编号</returns>
		public int GetID()
		{
			return _ID;
		}
		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>值</returns>
		/// <param name="name">名称</param>
		public string GetProperty(string name)
		{
			if (_Members.ContainsKey (name) == true) 
			{
				return _Members [name];
			}

			return null;
		}
		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>值</returns>
		/// <param name="name">名称</param>
		public T GetProperty<T>(string name)
		{
			string value = GetProperty (name);
			if (string.IsNullOrEmpty (value)) 
			{
				return default(T);
			}

			byte[] data = Encoding.UTF8.GetBytes (value);

			return (T)ConvertHelp.BytesToStruct (data, typeof(T));
		}
		/// <summary>
		/// 设置属性
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="value">值</param>
		public void SetProperty(string name, string value)
		{
			_Members [name] = value;
		}
		/// <summary>
		/// 清空所有属性
		/// </summary>
		public void ClearProperties()
		{
			_Members.Clear ();
		}
	}
}

