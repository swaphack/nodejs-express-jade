using System;

namespace Model.Base
{
	/// <summary>
	/// 模型项接口
	/// </summary>
	public interface IModelItem
	{
		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		int ID { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		/// <value>The name.</value>
		int Name { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		/// <value>The describe.</value>
		int Describe { get; set; }
	}
}

