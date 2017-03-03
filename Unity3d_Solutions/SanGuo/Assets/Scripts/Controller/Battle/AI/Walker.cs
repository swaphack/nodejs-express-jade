using System;
using UnityEngine;
using Model.Base;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 行走者
	/// </summary>
	public class Walker : UnitTask
	{
		/// <summary>
		/// 目标
		/// </summary>
		private Transform _Destination;

		public Walker ()
		{
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected override void InitTask(UnitTask task)
		{
			base.InitTask (task);
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
			return Src.Buff.HasType (BuffType.ForbiddenWalk);
		}
	}
}

