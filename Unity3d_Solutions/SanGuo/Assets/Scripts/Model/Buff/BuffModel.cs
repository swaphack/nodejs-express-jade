using System;
using Model.Base;

namespace Model.Buff
{
	/// <summary>
	/// 状态
	/// </summary>
	public class BuffModel : ModelItem
	{
		/// <summary>
		/// 是否减益状态
		/// </summary>
		public bool IsDeBuff;
		/// <summary>
		/// 相同状态是否可叠加
		/// </summary>
		public bool CanSuperpose;
		/// <summary>
		/// 影响的属性类型
		/// </summary>
		public PropertyType Type;
		/// <summary>
		/// 固定值
		/// </summary>
		public float FixValue;
		/// <summary>
		/// 百分比
		/// </summary>
		public float PercentValue; 
		/// <summary>
		/// 持续时间
		/// </summary>
		public int Duration;
	}
}

