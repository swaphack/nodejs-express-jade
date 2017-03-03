using System;
using System.Collections.Generic;
using Model.Skill;
using UnityEngine;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 技能目标查找
	/// </summary>
	public class SkillTargetFinder : UnitTask
	{
		/// <summary>
		/// 生效的技能
		/// </summary>
		private SkillModel _SkillModel;

		/// <summary>
		/// 目标
		/// </summary>
		private List<Unit> _Units;
		/// <summary>
		/// 生效的技能
		/// </summary>
		public SkillModel SkillModel {
			get { 
				return _SkillModel;
			}
		}
		/// <summary>
		/// 目标
		/// </summary>
		public List<Unit> Units {
			get { 
				return _Units;
			}
		}

		public SkillTargetFinder ()
		{
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task)
		{
			base.InitTask (task);

			SkillEnableChecker skillTask = GetUnitTask<SkillEnableChecker>(task);
			if (skillTask == null) {
				IsFinish = true;
				return;
			}

			_SkillModel = Src.Skill.GetSkillModel(skillTask.SkillIndex);
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update (float dt)
		{
			base.Update (dt);

			FindTargets ();

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
		/// 查找对象
		/// </summary>
		/// <returns><c>true</c>, if targets was found, <c>false</c> otherwise.</returns>
		private bool FindTargets()
		{
			if (_SkillModel == null) {
				return false;
			}

			List<Team> teams = Field.GetOtherAliveTeam (Src.TeamID);
			if (teams.Count == 0) {
				return false;
			}

			int rand = UnityEngine.Random.Range (0, teams.Count);
			Team targetTeam = teams [rand];
			rand = UnityEngine.Random.Range (0, targetTeam.AliveUnits.Count);

			List<Unit> targets = targetTeam.AliveUnits.GetRandomUnits(1);
			if (targets == null || targets.Count == 0) {
				return false;
			}

			_Units = targets;

			return true;
		}
	}
}

