using System;
using UnityEngine;

namespace Game.Listener
{
	/// <summary>
	/// 控制GameObject，实现类似UI功能的控件
	/// </summary>
	public class ActionListener : MonoBehaviour
	{
		/// <summary>
		/// 动作状态
		/// </summary>
		private ActionStatus _ActionStatus;
		/// <summary>
		/// 动作
		/// </summary>
		private SpawnActions _Actions;

		void Start() {
			_Actions = new SpawnActions ();
			_ActionStatus = ActionStatus.Pause;	
		}

		void Update() {
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
		/// 动作
		/// </summary>
		/// <value>The action.</value>
		public SpawnActions Actions {
			get { 
				return _Actions;
			}
		}
	}
}