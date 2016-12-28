using System;
using System.Text;
using System.Collections.Generic;

namespace Foundation.DataBase
{
	public class DataRecord : IDataRecord
	{
		/// <summary>
		/// 编号
		/// </summary>
		private int _ID;
		/// <summary>
		/// 成员
		/// </summary>
		private Dictionary<string, string> _Members;
		/// <summary>
		/// 内部文本
		/// </summary>
		private string _InnerText;

		public DataRecord ()
		{
			_ID = 0;
			_InnerText = "";
			_Members = new Dictionary<string, string> ();
		}

		/// <summary>
		/// 获取编号
		/// </summary>
		public int ID {
			get { 
				return _ID; 
			} 

			set { 
				_ID = value;
			}
		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>值</returns>
		/// <param name="name">名称</param>
		public string GetProperty (string name)
		{
			if (_Members.ContainsKey (name) == true) {
				return _Members [name];
			}

			return null;
		}

		/// <summary>
		/// 设置属性
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="value">值</param>
		public void SetProperty (string name, string value)
		{
			_Members [name] = value;
		}

		/// <summary>
		/// 内部文本
		/// </summary>
		/// <value>The inner text.</value>
		public string InnerText 
		{ 
			get { 
				return _InnerText;
			}
			set {
				_InnerText = value;
			} 
		}

		/// <summary>
		/// 清空所有属性
		/// </summary>
		public void ClearProperties ()
		{
			_Members.Clear ();
		}
	}
}

