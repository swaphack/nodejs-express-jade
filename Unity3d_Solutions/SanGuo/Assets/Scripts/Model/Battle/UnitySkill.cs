using System;
using Model.Skill;

namespace Model.Battle
{
	/// <summary>
	/// 单位技能
	/// </summary>
	public class UnitySkill
	{
		/// <summary>
		/// 技能数据
		/// </summary>
		private SkillModel _SkillModel;

		/// <summary>
		/// 冷却时间
		/// 冷却完毕才能触发
		/// </summary>
		private float _CoolDown;

		/// <summary>
		/// 技能数据
		/// </summary>
		public SkillModel SkillModel {
			get { 
				return _SkillModel;
			}
			set { 
				_SkillModel = value;
			}
		}

		public UnitySkill ()
		{
		}
	}
}

