using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 永远朝向摄像机
	/// </summary>
	public class FaceCameraLisener : MonoBehaviour
	{
		public FaceCameraLisener ()
		{
		}

		void Start()
		{
		}

		void Update()
		{
			Transform transform = this.GetComponent<Transform> ();

			Camera camera = Camera.main;


		}
	}
}

