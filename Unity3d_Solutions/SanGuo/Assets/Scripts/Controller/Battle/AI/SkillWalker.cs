using System;
using System.Collections.Generic;
using Model.Skill;
using Model.Base;
using UnityEngine;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 技能行走者
	/// </summary>
	public class SkillWalker : UnitTask
	{
		/// <summary>
		/// 移动到的目标
		/// </summary>
		private Unit _MoveToTarget;
		/// <summary>
		/// 技能模型
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

		public SkillWalker ()
		{
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task)
		{
			base.InitTask (task);

			SkillTargetFinder skillTask = GetUnitTask<SkillTargetFinder>(task);
			if (skillTask == null || skillTask.Units == null || skillTask.Units.Count == 0) {
				IsFinish = true;
				return;
			}

			_SkillModel = skillTask.SkillModel;
			_Units = skillTask.Units;

			float minDistance = 0;
			foreach (Unit item in skillTask.Units) {
				float dist = Vector3.Distance (Src.TranformObject.Position, item.TranformObject.Position);
				if (_MoveToTarget == null) {
					_MoveToTarget = item;
					minDistance = dist;
				} else if (dist < minDistance) {
					_MoveToTarget = item;
					minDistance = dist;
				}
			}

			Src.Walker.Clear ();

			List<Vector2> points = Field.Map.FindWay(Src.GameObject.transform, _MoveToTarget.GameObject.transform); 
			if (points != null) {
				Src.Walker.Set3DWayBy2D (points, Src.TranformObject.Position.y);
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update (float dt)
		{
			base.Update (dt);

			if (!CheckEnableWalk ()) {
				IsFinish = true;
				return;
			}

			if (CheckInSkillRadius ()) {
				Src.Walker.Clear ();
				IsFinish = true;
				return;
			}

			// 停止走路，站立
			if (Src.Walker.Empty) {
				RunStandAction ();
				IsFinish = true;
				return;
			}

			Vector3 walkByPosition;
			if (!Src.Walker.GetNextStation (dt, out walkByPosition)) {
				IsFinish = true;
				return;
			}

			Src.TranformObject.WalkBy (walkByPosition);
			RunWalkAction ();
		}

		/// <summary>
		/// 清空
		/// </summary>
		public override void Dispose()
		{
			base.Dispose ();
		}

		/// <summary>
		/// 是否在攻击范围内
		/// </summary>
		/// <returns><c>true</c>, if in skill radius was checked, <c>false</c> otherwise.</returns>
		private bool CheckInSkillRadius()
		{
			if (_SkillModel == null || _MoveToTarget == null) {
				return false;
			}

			float distance = Vector3.Distance (Src.TranformObject.Position, _MoveToTarget.TranformObject.Position);
			if (_SkillModel.Radius < distance) {
				return false;
			}
			return true;
		}

		/// <summary>
		/// 行走
		/// </summary>
		private void RunWalkAction()
		{
			if (!Src.TranformObject.IsPlay (UnitAction.AnimationTag.Walk)) {
				Src.TranformObject.PlayWalk ();
			}
		}

		/// <summary>
		/// 站立
		/// </summary>
		private void RunStandAction()
		{
			if (!Src.TranformObject.IsPlay (UnitAction.AnimationTag.Stand)) {
				Src.TranformObject.PlayStand ();
			}
		}

		/// <summary>
		/// 禁止行走
		/// </summary>
		/// <returns><c>true</c>, if enable walk was checked, <c>false</c> otherwise.</returns>
		private bool CheckEnableWalk() 
		{
			return !Src.Buff.HasType (BuffType.ForbiddenWalk);
		}
	}
}

