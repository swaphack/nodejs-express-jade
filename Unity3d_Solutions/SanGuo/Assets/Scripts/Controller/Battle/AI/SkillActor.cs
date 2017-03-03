using System;
using Model.Base;
using Model.Skill;
using System.Collections.Generic;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 技能表演者
	/// </summary>
	public class SkillActor : UnitTask
	{
		/// <summary>
		/// 技能模型
		/// </summary>
		private SkillModel _SkillModel;

		/// <summary>
		/// 目标
		/// </summary>
		private List<Unit> _Units;

		public SkillActor ()
		{
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task)
		{
			base.InitTask (task);

			SkillWalker skillTask = GetUnitTask<SkillWalker>(task);
			if (skillTask == null || skillTask.Units == null || skillTask.Units.Count == 0) {
				IsFinish = true;
				return;
			}

			_SkillModel = skillTask.SkillModel;
			_Units = skillTask.Units;


			Src.TranformObject.OnActionEnd += OnEndAction;
		}

		/// <summary>
		/// 更新施法者
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update(float dt)
		{
			base.Update (dt);

			RunSkillAction ();	
		}

		/// <summary>
		/// 清空
		/// </summary>
		public override void Dispose()
		{
			base.Dispose ();

			Src.TranformObject.OnActionEnd -= OnEndAction;
		}

		/// <summary>
		/// 动作播放完回调
		/// </summary>
		/// <param name="tag">Tag.</param>
		private void OnEndAction(string tag)
		{
			if (tag == UnitAction.AnimationTag.Attack) {
			}
		}

		/// <summary>
		/// 播放动画
		/// </summary>
		private void RunSkillAction()
		{
			if (!Src.TranformObject.IsPlay (UnitAction.AnimationTag.Attack)) {
				Src.TranformObject.PlayAttack ();
			}
		}
	}
}

