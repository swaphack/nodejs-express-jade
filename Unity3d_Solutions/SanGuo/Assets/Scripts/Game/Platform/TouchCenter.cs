using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Platform
{
	/// <summary>
	/// 触摸管理
	/// </summary>
	public class TouchCenter
	{
		/// <summary>
		/// 点击派发事件
		/// </summary>
		private List<ITouchDispatcher> _TouchDispatchers;

		public TouchCenter ()
		{
			_TouchDispatchers = new List<ITouchDispatcher> ();
		}

		/// <summary>
		/// 添加点击派发
		/// </summary>
		/// <param name="dispatcher">Dispatcher.</param>
		public void AddDispatcher (ITouchDispatcher dispatcher)
		{
			if (dispatcher == null) {
				return;
			}

			_TouchDispatchers.Add (dispatcher);
		}
		/// <summary>
		/// 移除点击派发
		/// </summary>
		/// <param name="dispatcher">Dispatcher.</param>
		public void RemoveDispatcher (ITouchDispatcher dispatcher)
		{
			if (dispatcher == null) {
				return;
			}

			_TouchDispatchers.Remove (dispatcher);
		}

		/// <summary>
		/// 检测点击状态
		/// </summary>
		public void Update ()
		{
			if (Camera.main == null) {
				return;
			}
			Vector3 vect;
			TouchPhase phase;
			Vector2 position;

			if (GetTouchPosition (out vect, out phase, out position) == false) {
				return;
			}

			Ray ray = Camera.main.ScreenPointToRay (vect);
			RaycastHit hitInfo;

			if (Physics.Raycast (ray, out hitInfo) == false) {
				return;
			}

			foreach (ITouchDispatcher dispather in _TouchDispatchers) {
				if (dispather.Target.enabled == true && hitInfo.collider == dispather.Target) {
					dispather.OnDispatchTouch (phase, position);
				}
			}
		}

		/// <summary>
		/// 获取点击坐标
		/// </summary>
		/// <returns>点击坐标</returns>
		private bool GetTouchPosition(out Vector3 vector, out TouchPhase phase, out Vector2 position)
		{
			vector = new Vector3 ();
			phase = TouchPhase.Began;
			position = new Vector2 ();

			if (Input.touchSupported == true && Input.touchSupported == true) {
				if (Input.touchCount == 0) {
					return false;
				}

				Touch touch = Input.GetTouch (0);

				vector = new Vector3 (touch.position.x, touch.position.y);
				phase = touch.phase;
				position = touch.position;

				return true;

			} else if (Input.mousePresent) {
				if (Input.GetMouseButtonDown (0)) {
					phase = TouchPhase.Began;
				} else if (Input.GetMouseButtonUp (0)) {
					phase = TouchPhase.Ended;
				} else if (Input.GetMouseButton (0)) {
					phase = TouchPhase.Moved;
				} else {
					return false;
				}

				vector = Input.mousePosition;
				position = new Vector2 (vector.x, vector.y);
				return true;
			}

			return false;
		}

		public void Clear()
		{
			_TouchDispatchers.Clear ();
		}
	}
}