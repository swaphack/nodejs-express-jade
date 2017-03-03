using System;
using System.Collections.Generic;
using Model.Skill;
using Model.Base;
using Model.Battle;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 检查技能是否可用
	/// </summary>
	public class SkillEnableChecker : UnitTask
	{
		/// <summary>
		/// 当前正在执行的技能
		/// </summary>
		private SkillIndex _CurrentSkillIndex;
		/// <summary>
		/// 当前正在执行的技能
		/// </summary>
		public SkillIndex SkillIndex {
			get { 
				return _CurrentSkillIndex;
			}
		}

		public SkillEnableChecker ()
		{
			_CurrentSkillIndex = SkillIndex.Max;
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task)
		{
			base.InitTask (task);

			_CurrentSkillIndex = SkillIndex.Max;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update (float dt)
		{
			base.Update (dt);

			// 检查生效技能
			if (!CheckEnableSkill()) {
				return;
			}

			IsFinish = true;
		}

		/// <summary>
		/// 清空
		/// </summary>
		public override void Dispose()
		{
			base.Dispose ();
		}

		/// <summary>
		/// 检查可用技能
		/// </summary>
		/// <returns><c>true</c>, if enable skill was checked, <c>false</c> otherwise.</returns>
		private bool CheckEnableSkill()
		{
			if (_CurrentSkillIndex != SkillIndex.Max) {
				return true;
			}

			for (int i = (int)SkillIndex.NormalAttack; i < (int)SkillIndex.Max; i++) {
				if (IsSkillEnable ((SkillIndex)i)) {
					_CurrentSkillIndex = (SkillIndex)i;
					break;
				}
			}

			return _CurrentSkillIndex != SkillIndex.Max;
		}

		/// <summary>
		/// 检查技能是否可用
		/// </summary>
		/// <returns><c>true</c> if this instance is skill enable the specified skillIndex; otherwise, <c>false</c>.</returns>
		/// <param name="skillIndex">Skill index.</param>
		private bool IsSkillEnable (SkillIndex skillIndex)
		{
			if (Src == null) {
				return false;
			}

			SkillModel skillModel = Src.Skill.GetSkillModel (skillIndex);
			if (skillModel == null) {
				return false;
			}

			UnitSkill.SkillValue skillValue = Src.Skill.GetSkillValue (skillIndex);
			if (skillValue == null) {
				return false;
			}

			bool bOk = false;
			do {
				// 禁止普通攻击？
				if (skillModel.IsNormalAttack && Src.Buff.HasType(BuffType.ForbiddenNormalAttack)) {
					break;
				}
				// 禁止魔法攻击？
				if (!skillModel.IsNormalAttack && Src.Buff.HasType(BuffType.ForbiddenSkillAttack)) {
					break;
				}
				// 冷却完毕？
				if (skillValue.CurrentValue > 0) {
					break;
				}
				// 魔法充足？
				if (Src.Property.GetValue(PropertyType.CurrentManaPoints) < skillModel.CostMana) {
					break;
				}

				bOk = true;

			} while(false);

			return bOk;
		}
	}
}

