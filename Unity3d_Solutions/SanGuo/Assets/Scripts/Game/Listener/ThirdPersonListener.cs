using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 第三人称
	/// </summary>
	public class ThirdPersonListener : MonoBehaviour
	{
		public ThirdPersonListener ()
		{
			
		}

		void Start()
		{
			Transform transform = this.GetComponent<Transform> ();

			Camera mainCamera = Camera.main;
			mainCamera.transform.SetParent(null);
			mainCamera.transform.SetParent (transform);
		}

		void Update()
		{

		}
	}
}

