using UnityEngine;
using System.Collections.Generic;
using Model.Skill;
using Controller.Battle;
using Controller.AI.Task;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 施法者
	/// </summary>
	public class SpellCaster : UnitTask
	{
		/// <summary>
		/// 技能任务队列
		/// </summary>
		private TaskQueue _SkillTaskQueue;

		public SpellCaster()
		{
			_SkillTaskQueue = new TaskQueue ();
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task)
		{
			base.InitTask (task);

			_SkillTaskQueue.AddTask (new SkillEnableChecker () { Src = Src, Field = Field });
			_SkillTaskQueue.AddTask (new SkillTargetFinder () { Src = Src, Field = Field });
			_SkillTaskQueue.AddTask (new SkillWalker () { Src = Src, Field = Field });
			_SkillTaskQueue.AddTask (new SkillActor () { Src = Src, Field = Field });
		}

		/// <summary>
		/// 更新施法者
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update(float dt)
		{
			base.Update (dt);

			if (_SkillTaskQueue.Count == 0) {
				IsFinish = true;
				return;
			}

			_SkillTaskQueue.Update (dt);
		}

		/// <summary>
		/// 清空
		/// </summary>
		public override void Dispose()
		{
			base.Dispose ();
		}
	}
}