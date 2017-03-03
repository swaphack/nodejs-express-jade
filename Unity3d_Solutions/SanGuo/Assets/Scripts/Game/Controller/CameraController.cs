using System;
using UnityEngine;

namespace Game.Controller
{
	/// <summary>
	/// 摄像头控制
	/// 挂在摄像机上
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		/// <summary>
		/// 滑轮转化视角系数
		/// </summary>
		public float SrollExchangeViewFieldRate = 0.5f;

		/// <summary>
		/// 触屏转化视角系数
		/// </summary>
		public float TouchExchangeViewFieldRate = 0.1f;

		/// <summary>
		/// 是否可用
		/// </summary>
		private bool _IsEnabled = false;

		/// <summary>
		/// 保留最近一次两只手指在屏幕的距离
		/// </summary>
		private float _LastTowTouchDistance = -1;

		void Start() {
			Camera camera = this.GetComponent<Camera> ();
			if (camera == null) {
				_IsEnabled = false;
			} else {
				_IsEnabled = true;
			}
		}

		void Update() {
			if (_IsEnabled == false) {
				return;
			}

			// 检查摄像机视野
			CheckCameraFieldView ();
		}

		/// <summary>
		/// 检查摄像机视野
		/// </summary>
		internal void CheckCameraFieldView() {
			float scrollWheel = 0;
			if (Input.touchSupported == true && Input.touchSupported == true) {
				if (Input.touchCount == 0 || Input.touchCount != 2) {
					_LastTowTouchDistance = -1;
					return;
				}

				Touch touch1 = Input.GetTouch (0);
				Touch touch2 = Input.GetTouch (1);

				float distance = Vector3.Distance (touch1.position, touch2.position);
				if (_LastTowTouchDistance == -1) {
					_LastTowTouchDistance = distance;
				} else {
					scrollWheel = (_LastTowTouchDistance - distance) * TouchExchangeViewFieldRate;
					_LastTowTouchDistance = distance;
				}
			} else if(Input.mousePresent == true){
				scrollWheel = Input.mouseScrollDelta.y * SrollExchangeViewFieldRate;
			}

			IncreaseCameraFieldView (scrollWheel);
		}

		/// <summary>
		/// 增加摄像机的视野
		/// </summary>
		/// <param name="value">Value.</param>
		private void IncreaseCameraFieldView(float value) {
			if (value == 0) {
				return;
			}
			Camera camera = this.GetComponent<Camera> ();
			float newValue = camera.fieldOfView;
			newValue += value;
			if (newValue < 1) {
				newValue = 1;
			}
			if (newValue > 179) {
				newValue = 179;
			}
			camera.fieldOfView = newValue;
		}
	}
}

