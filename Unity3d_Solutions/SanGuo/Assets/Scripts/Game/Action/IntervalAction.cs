using System;
using UnityEngine;

namespace Game.Action
{
	/// <summary>
	/// 有时间间隔的动作
	/// </summary>
	public class IntervalAction : BaseAction
	{
		/// <summary>
		/// 当前已走时间
		/// </summary>
		protected float _CurrentTime;
		/// <summary>
		/// 总时间
		/// </summary>
		protected float _TotalTime;

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
			UpdateAction (dt);
			if (_CurrentTime >= _TotalTime) {
				IsFinish = true;
				return;
			}
		}

		protected virtual void UpdateAction(float dt)
		{
			
		}
	}

	/// <summary>
	/// 移动到指定目标
	/// </summary>
	public class MoveTo : IntervalAction
	{
		/// <summary>
		/// 目标坐标
		/// </summary>
		private Vector3 _Destination;

		public MoveTo()
		{
			_Destination = new Vector3 ();
		}

		/// <summary>
		/// 创建位移
		/// </summary>
		/// <param name="time">Time.</param>
		/// <param name="destination">Destination.</param>
		public MoveTo(float time, Vector3 destination)
		{
			_TotalTime = time;
			_Destination = destination;
		}

		/// <summary>
		/// 创建位移
		/// </summary>
		/// <param name="time">Time.</param>
		/// <param name="destination">Destination.</param>
		public static MoveTo Create(float time, Vector3 destination)
		{
			return new MoveTo (time, destination);
		}

		protected override void UpdateAction(float dt)
		{
			Target.transform.Translate(_Destination * dt / _TotalTime);
		}
	}
}

