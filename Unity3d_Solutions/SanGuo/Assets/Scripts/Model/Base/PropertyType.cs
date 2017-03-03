using System;

namespace Model.Base
{
	/// <summary>
	/// 属性类型
	/// </summary>
	public enum PropertyType
	{
		/// <summary>
		/// 非属性类型
		/// </summary>
		None,
		/// <summary>
		/// 物理伤害
		/// </summary>
		AttactDamage,
		/// <summary>
		/// 物理防御
		/// </summary>
		PhysicalDefense,
		/// <summary>
		/// 攻击速度
		/// </summary>
		AttackSpeed,
		/// <summary>
		/// 魔法伤害
		/// </summary>
		MagicDamage,
		/// <summary>
		/// 魔法防御
		/// </summary>
		MagicDefense,
		/// <summary>
		/// 冷却时间
		/// </summary>
		CoolDown,
		/// <summary>
		/// 生命值
		/// </summary>
		HitPoints,
		/// <summary>
		/// 当前生命值
		/// </summary>
		CurrentHitPoints,
		/// <summary>
		/// 魔法值
		/// </summary>
		ManaPoints,
		/// <summary>
		/// 当前魔法值
		/// </summary>
		CurrentManaPoints,
		/// <summary>
		/// 移动速度
		/// </summary>
		MoveSpeed,
		/// <summary>
		/// 暴击率
		/// </summary>
		CritRate,
		/// <summary>
		/// 暴击伤害
		/// </summary>
		CritDamage,
	}
}

