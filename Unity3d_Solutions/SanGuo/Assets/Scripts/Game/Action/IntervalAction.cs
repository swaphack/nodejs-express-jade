using System;

namespace Game.Action
{
	/// <summary>
	/// 有时间间隔的动作
	/// </summary>
	public class IntervalAction : Action
	{
		/// <summary>
		/// 当前已走时间
		/// </summary>
		private float _CurrentTime;
		/// <summary>
		/// 总时间
		/// </summary>
		public float TotalTime;

		public IntervalAction ()
		{
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public override void Update(float dt)
		{
			_CurrentTime += dt;
			UpdateAction ();
			if (_CurrentTime >= TotalTime) {
				IsFinish = true;
				return;
			}
		}

		protected virtual void UpdateAction()
		{
			
		}
	}
}

