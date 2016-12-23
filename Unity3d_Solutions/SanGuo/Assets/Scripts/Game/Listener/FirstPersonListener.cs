using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 第一人称
	/// </summary>
	public class FirstPersonListener : MonoBehaviour
	{
		public FirstPersonListener ()
		{
		}

		void Start()
		{
			Transform transform = this.GetComponent<Transform> ();

			Camera mainCamera = Camera.main;
			mainCamera.transform.SetParent(null);
			mainCamera.transform.SetParent (transform);
			mainCamera.transform.localPosition = Vector3.zero;
		}

		void Update()
		{
			
		}
	}
}

