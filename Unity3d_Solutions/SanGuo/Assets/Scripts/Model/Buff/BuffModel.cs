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
		/// 状态类型
		/// </summary>
		public BuffType BuffType;
		/// <summary>
		/// 相同状态是否可叠加
		/// </summary>
		public bool CanSuperpose;
		/// <summary>
		/// 持续时间
		/// </summary>
		public float Duration;
		/// <summary>
		/// 间隔
		/// </summary>
		public float Interval;

		/// <summary>
		/// 是否减益状态
		/// </summary>
		public bool IsDeBuff;
		/// <summary>
		/// 影响的属性类型
		/// </summary>
		public PropertyType PropertyType;
		/// <summary>
		/// 固定值
		/// </summary>
		public float FixValue;
		/// <summary>
		/// 百分比
		/// </summary>
		public float PercentValue; 
	}
}

