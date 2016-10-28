using System;

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
		/// 获取属性值
		/// </summary>
		/// <returns>值</returns>
		/// <param name="name">名称</param>
		T GetProperty<T>(string name);
		/// <summary>
		/// 设置属性
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="value">值</param>
		void SetProperty(string name, string value);
		/// <summary>
		/// 清空所有属性
		/// </summary>
		void ClearProperties();
	}
}

