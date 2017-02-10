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
		public float ChooseRadius;
		/// <summary>
		/// 选择目标类型
		/// 目标满足条件，才能触发
		/// </summary>
		public TargetType TargetType;

		/// <summary>
		/// 技能效果类型
		/// </summary>
		public EffectType EffectType;
		/// <summary>
		/// 技能效果参数
		/// </summary>
		public List<float> EffectParams;

		/// <summary>
		/// 状态
		/// </summary>
		public List<BuffModel> Buffs;

		public SkillModel()
		{
			EffectParams = new List<float> ();
			Buffs = new List<BuffModel> ();
		}
	}
}

