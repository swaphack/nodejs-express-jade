using System;

namespace Game
{
	/// <summary>
	/// 实例单个动作表现
	/// </summary>
	public abstract class Action : IAction
	{
		/// <summary>
		/// 是否结束
		/// </summary>
		private bool _IsFinish;
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

		public Action ()
		{
			_IsFinish = false;
		}

		public abstract void Update(float dt);
	}
}

