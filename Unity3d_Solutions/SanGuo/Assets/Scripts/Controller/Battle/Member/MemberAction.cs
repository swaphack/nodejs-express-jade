using System;
using UnityEngine;
using Game.Action;

namespace Controller.Battle.Member
{
	/// <summary>
	/// 动作对象
	/// </summary>
	public class MemberAction
	{
		/// <summary>
		/// The action.
		/// </summary>
		private GOAction _Action;

		public MemberAction ()
		{
			_Action = new GOAction ();
		}

		/// <summary>
		/// 设置变换对象
		/// </summary>
		/// <param name="target">Transform.</param>
		public void SetTranform(Transform target)
		{
			_Action.Target = target;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void RunAction(IAction action) 
		{
			if (action == null) {
				return;
			}

			_Action.RunAction (action);
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Pause()
		{
			_Action.Pause ();
		}

		/// <summary>
		/// 恢复
		/// </summary>
		public void Resume()
		{
			_Action.Resume ();
		}
	}
}

