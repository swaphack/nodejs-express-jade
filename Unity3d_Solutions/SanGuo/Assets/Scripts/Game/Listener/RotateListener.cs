using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 旋转监听
	/// </summary>
	public class RotateListener : MonoBehaviour
	{
		/// <summary>
		/// 控制对象
		/// </summary>
		private Transform _Target;
		/// <summary>
		/// 鼠标右键是否点击
		/// </summary>
		private bool _RightMouseDown = false;
		/// <summary>
		/// 最后一次点击的位置
		/// </summary>
		private Vector2 _LastTouchPosition;

		void Start() {
			_Target = this.GetComponent<Transform> ();
			_LastTouchPosition = new Vector3 (-1, -1);
		}

		void Update() {
			if (_Target == null) {
				return;
			}

			CheckGameObjectRotate ();
		}

		/// <summary>
		/// 检查摄像头位置
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
				if (Input.GetMouseButtonDown (1)) {
					_RightMouseDown = true;
				} else if (Input.GetMouseButtonUp (1)) {
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

		private void IncreateGameObjectRotation( Vector2 vector){
			_Target.Rotate (vector.y, vector.x, 0);
		}
	}
}

