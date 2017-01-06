using System;
using System.Collections.Generic;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据项
	/// </summary>
	public interface IDataRecord
	{
		/// <summary>
		/// 获取编号
		/// </summary>
		int ID { get; set; }
		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>值</returns>
		/// <param name="name">名称</param>
		string GetProperty(string name);
		/// <summary>
		/// 设置属性
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="value">值</param>
		void SetProperty(string name, string value);
		/// <summary>
		/// 内部文本
		/// </summary>
		/// <value>The inner text.</value>
		string InnerText { get; set; }
		/// <summary>
		/// 清空所有属性
		/// </summary>
		void ClearProperties();
		/// <summary>
		/// 关键字
		/// </summary>
		/// <value>The keys.</value>
		List<string> Keys { get;}
		/// <summary>
		/// 属性值
		/// </summary>
		/// <value>The values.</value>
		List<string> Values { get;}
	}
}

