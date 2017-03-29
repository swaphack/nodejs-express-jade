using UnityEngine;
using System.Collections;

namespace Game.Controller
{
	/// <summary>
	/// 自转
	/// </summary>
	public class RotationController : MonoBehaviour
	{
		/// <summary>
		/// 旋转速度
		/// </summary>
		public float RotationSpeed;
	
		/// <summary>
		/// 旋转轴
		/// </summary>
		public Vector3 Axis;

		public RotationController()
		{
			Axis = Vector3.up;
			RotationSpeed = 360;
		}

		// Use this for initialization
		void Start ()
		{
			
		}

		// Update is called once per frame
		void Update ()
		{
			transform.Rotate(Axis, RotationSpeed * Time.deltaTime);
		}
	}
}