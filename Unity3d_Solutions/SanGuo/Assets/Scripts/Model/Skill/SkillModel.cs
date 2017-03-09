using System;
using System.Collections.Generic;
using Model.Base;
using Model.Buff;

namespace Model.Skill
{
	/// <summary>
	/// 技能模型
	/// </summary>
	public class SkillModel : ModelItem
	{
		/// <summary>
		/// 是否普通攻击
		/// 否则技能攻击
		/// </summary>
		public bool IsNormalAttack;
		/// <summary>
		/// 冷却时间
		/// 冷却完毕才能触发
		/// </summary>
		public float CoolDown;
		/// <summary>
		/// 消耗魔法
		/// 魔法足够才能触发
		/// </summary>
		public int CostMana;

		/// <summary>
		/// 选择半径
		/// 在范围内有目标，才能触发
		/// </summary>
		public float Radius;
		/// <summary>
		/// 选择目标类型
		/// 目标满足条件，才能触发
		/// </summary>
		public TargetType TargetType;
		/// <summary>
		/// 数量
		/// </summary>
		public int Count;

		/// <summary>
		/// 技能效果类型
		/// </summary>
		public EffectType EffectType;
		/// <summary>
		/// 技能效果参数
		/// </summary>
		public List<float> EffectParams;

		/// <summary>
		/// 触发状态 {状态编号， 概率}
		/// </summary>
		public Dictionary<int, float> Buffs;

		public SkillModel()
		{
			EffectParams = new List<float> ();
			Buffs = new Dictionary<int, float> ();
		}
	}
}

