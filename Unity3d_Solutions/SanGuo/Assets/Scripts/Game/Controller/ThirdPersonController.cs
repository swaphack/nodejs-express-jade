using System;
using UnityEngine;

namespace Game.Controller
{
	/// <summary>
	/// 第三人称
	/// </summary>
	public class ThirdPersonController : MonoBehaviour
	{
		/// <summary>
		/// 摄像机位置
		/// </summary>
		public Vector3 CameraPosition;
		/// <summary>
		/// 摄像机是否随物体翻转
		/// </summary>
		public bool IsCameraRotation;
		/// <summary>
		/// 初始摄像机父节点
		/// </summary>
		private Transform _CameraParent;
		/// <summary>
		/// 摄像机旋转角度
		/// </summary>
		private Vector3 _CameraRotation;

		public ThirdPersonController ()
		{
			CameraPosition.x = 0;
			CameraPosition.y = 1;
			CameraPosition.z = -5;
		}

		void Start()
		{
			Camera mainCamera = Camera.main;
			_CameraParent = mainCamera.transform.parent;
		}

		void Update()
		{
			Camera mainCamera = Camera.main;

			if (IsCameraRotation == true) {
				mainCamera.transform.SetParent (transform);
				mainCamera.transform.localPosition = CameraPosition;
				mainCamera.transform.LookAt (transform);
			} else {
				mainCamera.transform.SetParent (_CameraParent);
				Vector3 worldVector = transform.position + CameraPosition;
				mainCamera.transform.position = worldVector;
				mainCamera.transform.LookAt (transform);
			}
		}
	}
}

