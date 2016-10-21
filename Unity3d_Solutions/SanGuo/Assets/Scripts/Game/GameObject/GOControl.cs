using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 触摸委托
	/// </summary>
	public delegate void TouchHandler (ITouchDispatcher dispatcher, TouchPhase state, Vector2 vector);
	/// <summary>
	/// 控制GameObject，实现类似UI功能的控件
	/// </summary>
	public class GOControl : ITouchDispatcher
	{
		/// <summary>
		/// 目标
		/// </summary>
		private GameObject _Target = null;
		/// <summary>
		/// 是否可点击
		/// </summary>
		private bool _TouchEnabled = false;

		/// <summary>
		/// 触摸事件
		/// </summary>
		public event TouchHandler OnTouch;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="target">Target.</param>
		public GOControl (GameObject target)
		{
			_Target = target;
		}

		/// <summary>
		/// 获取组件
		/// </summary>
		/// <returns>The componet.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetComponet<T> ()
		{
			return _Target.GetComponent<T>() ;
		}

		/// <summary>
		/// 获取对象
		/// </summary>
		/// <value>The target.</value>
		public GameObject Target {
			set { _Target = value; }
			get { return _Target; }
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
					GameInstance.GetInstance ().Touch.AddDispatcher (this);
				} else {
					GameInstance.GetInstance ().Touch.RemoveDispatcher (this);
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
			OnTouch (this, state, vector);
		}
	}
}