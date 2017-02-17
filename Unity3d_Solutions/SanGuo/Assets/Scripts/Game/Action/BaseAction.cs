using System;
using UnityEngine;

namespace Game.Action
{
	/// <summary>
	/// 实例单个动作表现
	/// </summary>
	public abstract class BaseAction : IAction
	{
		/// <summary>
		/// 是否结束
		/// </summary>
		private bool _IsFinish;
		/// <summary>
		/// 对象
		/// </summary>
		private Transform _Target;

		/// <summary>
		/// 是否完成动作，如果完成移除
		/// </summary>
		/// <value><c>true</c> if this instance is finish; otherwise, <c>false</c>.</value>
		public bool IsFinish { 
			get { 
				return _IsFinish;
			}
			protected set { 
				_IsFinish = value;
			}
		}

		/// <summary>
		/// 对象
		/// </summary>
		/// <value>The transform.</value>
		public Transform Target { 
			get { 
				return _Target;
			}
			set { 
				_Target = value;
			}
		}

		public BaseAction ()
		{
			_IsFinish = false;
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public abstract void Update(float dt);
	}
}

