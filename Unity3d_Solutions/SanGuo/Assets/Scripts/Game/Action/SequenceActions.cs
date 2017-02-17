using System;
using System.Collections.Generic;

namespace Game.Action
{
	/// <summary>
	/// 串行动作
	/// </summary>
	public class SequenceActions : BaseAction
	{
		/// <summary>
		/// 动作集
		/// </summary>
		private List<IAction> _Actions;

		public SequenceActions()
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

			IAction action = _Actions [0];
			if (action == null) {
				_Actions.RemoveAt (0);
			}

			action.Update(dt);

			if (action.IsFinish == true) {
				_Actions.RemoveAt (0);
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

