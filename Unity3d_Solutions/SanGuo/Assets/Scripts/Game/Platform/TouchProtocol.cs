using System;
using UnityEngine;

namespace Game.Platform
{
	/// <summary>
	/// 点击监听
	/// 物体须有碰撞器组件
	/// 挂在物体上
	/// </summary>
	public class TouchProtocol : ITouchDispatcher
	{
		/// <summary>
		/// 是否可点击
		/// </summary>
		private bool _TouchEnabled;
		/// <summary>
		/// 目标
		/// </summary>
		protected Collider _Target;

		/// <summary>
		/// 点击开始
		/// </summary>
		public OnTouchCallBack OnTouchBegan;

		/// <summary>
		/// 滑动
		/// </summary>
		public OnTouchCallBack OnTouchMoved;

		/// <summary>
		/// 点击离开
		/// </summary>
		public OnTouchCallBack OnTouchEnded;

		/// <summary>
		/// 获取对象
		/// </summary>
		/// <value>The target.</value>
		public Collider Target {
			set { 
				_Target = value; 
			}
			get { 
				return _Target; 
			}
		}

		/// <summary>
		/// 是否可点击
		/// </summary>
		/// <value><c>true</c> if this instance is touch enable; otherwise, <c>false</c>.</value>
		public bool IsTouchEnable {
			set {
				if (value == _TouchEnabled) {
					return;
				}
				_TouchEnabled = value;
				if (_TouchEnabled == true) {
					GameInstance.GetInstance ().Device.AddTouchDispatcher (this);
				} else {
					GameInstance.GetInstance ().Device.RemoveTouchDispatcher (this);
				}

			}
			get { 
				return _TouchEnabled;
			}
		}

		/// <summary>
		/// 点击生效
		/// </summary>
		/// <param name="state">点击状态</param>
		/// <param name="vector">点击点</param>
		public void OnDispatchTouch (TouchPhase state, Vector2 vector)
		{
			if (state == TouchPhase.Began) {
				if (OnTouchBegan != null)
					OnTouchBegan (vector);
			} else if (state == TouchPhase.Moved) {
				if (OnTouchMoved != null)
					OnTouchMoved (vector);
			} else if (state == TouchPhase.Ended) {
				if (OnTouchBegan != null)
					OnTouchBegan (vector);
			}
		}
	}
}

