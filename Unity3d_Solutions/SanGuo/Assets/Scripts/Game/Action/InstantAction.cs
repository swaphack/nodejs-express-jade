using System;

namespace Game.Action
{
	/// <summary>
	/// 瞬间完成
	/// </summary>
	public class InstantAction : Action
	{
		public InstantAction ()
		{
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update(float dt)
		{
			UpdateAction ();
			IsFinish = true;
		}

		protected virtual void UpdateAction()
		{

		}
	}

	/// <summary>
	/// 执行一次操作
	/// </summary>
	public class InstanceFuncAction : InstantAction
	{
		/// <summary>
		/// 瞬间执行操作
		/// </summary>
		private OnActionCallback _Handler;

		/// <summary>
		/// 瞬间执行操作
		/// </summary>
		public OnActionCallback Handler {
			get { 
				return _Handler;
			}
			set { 
				_Handler = value;
			}
		}

		protected override void UpdateAction()
		{
			if (_Handler != null) {
				_Handler (this);
			}
		}
	}
}

