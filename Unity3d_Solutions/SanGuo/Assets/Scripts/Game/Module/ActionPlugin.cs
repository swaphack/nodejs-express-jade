using System;
using UnityEngine;
using System.Collections.Generic;
using Foundation.Plugin;
using Game.Action;

namespace Game.Module
{
	/// <summary>
	/// 动作管理中心
	/// </summary>
	public class ActionPlugin : IPlugin
	{
		/// <summary>
		/// 动作集
		/// </summary>
		private List<IAction> _Actions;

		public ActionPlugin()
		{
			_Actions = new List<IAction> ();
		}

		/// <summary>
		/// 添加动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void AddAction( IAction action)
		{
			if (action == null) {
				return;
			}

			if (_Actions.Contains (action) == true) {
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

		/// <summary>
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.Action;
			} 
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			int count = _Actions.Count;
			if (count < 0) {
				return;
			}

			List<IAction> removeActions = new List<IAction> ();
			for (int i = 0; i < count; i++) {
				_Actions[i].Update (dt);

				if (_Actions[i].IsFinish == true) {
					removeActions.Add (_Actions[i]);
				}
			}

			count = removeActions.Count;
			for (int i = 0; i < count; i++) {
				this.RemoveAction (removeActions[i]);
			}
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_Actions.Clear ();
		}
	}
}

