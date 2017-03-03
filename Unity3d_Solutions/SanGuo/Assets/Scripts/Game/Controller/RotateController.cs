using System;
using UnityEngine;

namespace Game.Controller
{
	/// <summary>
	/// 鼠标键
	/// </summary>
	public enum MouseKey
	{
		/// <summary>
		/// 鼠标左键
		/// </summary>
		Left = 0,
		/// <summary>
		/// 鼠标右键
		/// </summary>
		Right = 1,
	}
	/// <summary>
	/// 旋转监听
	/// 
	/// 挂在物体上
	/// </summary>
	public class RotateController : MonoBehaviour
	{
		/// <summary>
		/// 鼠标右键是否点击
		/// </summary>
		private bool _RightMouseDown = false;
		/// <summary>
		/// 最后一次点击的位置
		/// </summary>
		private Vector2 _LastTouchPosition;

		void Start() {
			_LastTouchPosition = new Vector3 (-1, -1);
		}

		void Update() {
			CheckGameObjectRotate ();
		}

		/// <summary>
		/// 检查物体位置
		/// </summary>
		internal void CheckGameObjectRotate() {
			Vector2 rotation = new Vector2();
			if (Input.touchSupported == true && Input.touchSupported == true) {
				if (Input.touchCount == 0 || Input.touchCount != 1) {
					_LastTouchPosition.x = -1;
					_LastTouchPosition.y = -1;
					return;
				}

				Touch touch = Input.GetTouch (0);
				if (_LastTouchPosition.x == -1 && _LastTouchPosition.y == -1) {
					_LastTouchPosition.x = touch.position.x;
					_LastTouchPosition.y = touch.position.y;
				} else {
					rotation.x = touch.position.x - _LastTouchPosition.x;
					rotation.y = touch.position.y - _LastTouchPosition.y;
					_LastTouchPosition = touch.position;
				}
			} else if(Input.mousePresent == true) {
				// 右键点击
				if (Input.GetMouseButtonDown ((int)MouseKey.Right)) {
					_RightMouseDown = true;
				} else if (Input.GetMouseButtonUp ((int)MouseKey.Right)) {
					_RightMouseDown = false;
				}

				if (_RightMouseDown == false) {
					_LastTouchPosition.x = -1;
					_LastTouchPosition.y = -1;
					return;
				}

				Vector3 vector = Input.mousePosition;

				if (_LastTouchPosition.x == -1 && _LastTouchPosition.y == -1) {
					_LastTouchPosition.x = vector.x;
					_LastTouchPosition.y = vector.y;
				} else {
					rotation.x = vector.x - _LastTouchPosition.x;
					rotation.y = vector.y - _LastTouchPosition.y;
					_LastTouchPosition.x = vector.x;
					_LastTouchPosition.y = vector.y;
				}
			}

			if (rotation.Equals (Vector2.zero) == false) {
				IncreateGameObjectRotation (rotation);
			}
		}

		private void IncreateGameObjectRotation( Vector2 vector) {
			transform.Rotate (vector.y, vector.x, 0);
		}
	}
}

