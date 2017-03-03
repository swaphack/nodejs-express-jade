using System;

namespace Model.Base
{
	/// <summary>
	/// 冷却时间
	/// </summary>
	public class CoolDown
	{
		/// <summary>
		/// 总冷却时间
		/// </summary>
		public float MaxValue;
		/// <summary>
		/// 当前冷却时间
		/// </summary>
		public float CurrentValue;

		public CoolDown ()
		{
			CurrentValue = 0;
			MaxValue = 0;
		}

		/// <summary>
		/// 设置最大值
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetMaxValue(float value)
		{
			MaxValue = value;
		}

		/// <summary>
		/// 重置当前值
		/// </summary>
		public void Reset()
		{
			CurrentValue = 0;
		}
	}
}

