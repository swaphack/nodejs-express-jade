using System;
using System.Collections.Generic;

namespace Game.Action
{
	/// <summary>
	/// 并行动作
	/// </summary>
	public class SpawnActions : BaseAction
	{
		/// <summary>
		/// 动作集
		/// </summary>
		private List<IAction> _Actions;

		public SpawnActions()
		{
			_Actions = new List<IAction> ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update(float dt)
		{
			int count = _Actions.Count;
			if (count < 0) {
				return;
			}

			List<IAction> removeActions = new List<IAction> ();

			foreach (IAction action in _Actions) {
				action.Update (dt);
				if (action.IsFinish == true) {
					removeActions.Add (action);
				}
			}

			foreach (IAction action in removeActions) {
				this.RemoveAction (action);
			}

			IsFinish = _Actions.Count == 0;
		}

		/// <summary>
		/// 添加动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void AddAction(IAction action)
		{
			if (action == null) {
				return;
			}

			_Actions.Add (action);
		}

		/// <summary>
		/// 移除动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void RemoveAction(IAction action)
		{
			if (action == null) {
				return;
			}

			_Actions.Remove (action);
		}

		/// <summary>
		/// 移除所有动作
		/// </summary>
		public void RemoveAllActions ()
		{
			_Actions.Clear ();
		}
	}
}

