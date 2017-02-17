using System;

namespace Model.Base
{
	/// <summary>
	/// 单位动作
	/// </summary>
	public class UnitAction
	{
		/// <summary>
		/// 触发器名称
		/// </summary>
		public class TriggerName
		{
			public const string Stand = "stand";
			public const string Walk = "walk";
			public const string Run = "run";
			public const string Attack = "attack";
			public const string Dead = "die";
			public const string Hurt = "hurt";	
		}

		/// <summary>
		/// 动作名称
		/// </summary>
		public class AnimationTag
		{
			public const string Stand = "stand";
			public const string Walk = "walk";
			public const string Run = "run";
			public const string Attack = "attack";
			public const string Dead = "dead";
			public const string Hurt = "hurt";	 
		}
	}
}

