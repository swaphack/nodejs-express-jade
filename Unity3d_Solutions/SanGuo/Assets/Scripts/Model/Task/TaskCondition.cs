using System;

namespace Model.Task
{
	/// <summary>
	/// 任务条件
	/// </summary>
	public class TaskCondition
	{
		/// <summary>
		/// 条件类型
		/// </summary>
		public int Type;
		/// <summary>
		/// 目标编号（物品id、怪物id等）
		/// </summary>
		public int TargetID;
		/// <summary>
		/// 数值（数量、次数、时间等）
		/// </summary>
		public int Number;
	}
}

