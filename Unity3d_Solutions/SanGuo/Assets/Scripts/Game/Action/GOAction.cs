using System;
using UnityEngine;

namespace Game.Action
{
	/// <summary>
	/// 对象动作
	/// </summary>
	public class GOAction
	{
		/// <summary>
		/// 动作状态
		/// </summary>
		private ActionStatus _ActionStatus;
		/// <summary>
		/// 动作
		/// </summary>
		private SpawnActions _Actions;
		/// <summary>
		/// 目标
		/// </summary>
		private Transform _Target;

		public GOAction() 
		{
			_Actions = new SpawnActions ();
			_ActionStatus = ActionStatus.Pause;	
		}

		/// <summary>
		/// 目标
		/// </summary>
		/// <value>The target.</value>
		public Transform Target {
			get { 
				return _Target;
			}
			set { 
				_Target = value;
			}
		}

		/// <summary>
		/// 动作状态
		/// </summary>
		/// <value>The status.</value>
		public ActionStatus Status {
			set { 
				if (_ActionStatus == value) {
					return;
				}

				if (value == ActionStatus.Pause) {
					GameInstance.GetInstance ().Action.AddAction (_Actions);
				} else if (value == ActionStatus.Running) {
					GameInstance.GetInstance ().Action.RemoveAction (_Actions);
				}

				_ActionStatus = value;
			}
			get { 
				return _ActionStatus;
			}
		}

		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void RunAction(IAction action)
		{
			if (action == null) {
				return;
			}

			action.Target = Target;
			_Actions.AddAction (action);
		}

		/// <summary>
		/// 停止动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void StopAction(IAction action)
		{
			if (action == null) {
				return;
			}
			_Actions.RemoveAction (action);
		}

		/// <summary>
		/// 停止所有动作
		/// </summary>
		public void StopAllActions()
		{
			_Actions.RemoveAllActions ();
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Pause()
		{
			Status = ActionStatus.Pause;
		}

		/// <summary>
		/// 恢复
		/// </summary>
		public void Resume()
		{
			Status = ActionStatus.Running;
		}
	}
}

