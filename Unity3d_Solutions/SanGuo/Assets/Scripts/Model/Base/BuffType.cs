using System;

namespace Model.Base
{
	/// <summary>
	/// buff类型
	/// </summary>
	public enum BuffType
	{
		/// <summary>
		/// 影响属性
		/// </summary>
		EffectProperty,
		/// <summary>
		/// 禁止技能攻击
		/// </summary>
		ForbiddenSkillAttack,
		/// <summary>
		/// 静止普通攻击
		/// </summary>
		ForbiddenNormalAttack,
		/// <summary>
		/// 静止行走
		/// </summary>
		ForbiddenWalk,
	}
}

