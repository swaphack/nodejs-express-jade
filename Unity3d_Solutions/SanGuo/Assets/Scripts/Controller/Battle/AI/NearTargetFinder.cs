using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 最近目标找寻者
	/// </summary>
	public class NearTargetFinder : UnitTask
	{
		/// <summary>
		/// 移动到的目标
		/// </summary>
		private Unit _MoveToTarget;
		/// <summary>
		/// 目标点
		/// </summary>
		private Transform _NearTarget;

		public NearTargetFinder ()
		{
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task) 
		{
			base.InitTask (task);

			_NearTarget = null;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update (float dt)
		{
			base.Update (dt);

			List<Team> teams = Field.GetOtherAliveTeam (Src.TeamID);
			// 无目标
			if (teams == null || teams.Count == 0) {
				IsFinish = true;
				return;
			}
				
			float minDistance = 0;
			// 查找距离最近的单位
			foreach (Team team in teams) {
				foreach (KeyValuePair<int, Unit> item in team.AliveUnits.Units) {
					float dist = Vector3.Distance (Src.TranformObject.Position, item.Value.TranformObject.Position);
					if (_NearTarget == null) {
						_NearTarget = item.Value.TranformObject.Transform;
						minDistance = dist;
					} else if (dist < minDistance) {
						_NearTarget = item.Value.TranformObject.Transform;
						minDistance = dist;
					}
				}
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
	}
}

