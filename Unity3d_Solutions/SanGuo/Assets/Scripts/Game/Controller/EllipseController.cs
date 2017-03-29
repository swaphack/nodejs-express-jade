using UnityEngine;
using System.Collections;
using Game.Helper;

namespace Game.Controller
{
	/// <summary>
	/// 椭圆轨迹
	/// 二维，圆心（x0, y0）,角度 A
	/// x = a * sinA + x0
	/// y = b * cosA + y0
	/// 
	/// 三维，圆心（x0, y0, z0）,与x-o-y平面夹角A, 与z轴夹角B。 
	/// -PI<= A <= PI, 0<= B <=PI
	/// x = a * sinA * cosB + x0
	/// y = b * sinA * sinB + y0
	/// z = b * cosA
	/// </summary>
	public class EllipseController : MonoBehaviour
	{
		/// <summary>
		/// 圆心
		/// </summary>
		public Vector3 Center;
		/// <summary>
		/// 长半轴长度
		/// </summary>
		public float Width;
		/// <summary>
		/// 短半轴长度
		/// </summary>
		public float Height;
		/// <summary>
		/// 与z轴夹角
		/// </summary>
		public float AngleZ;
		// 与xoy平面夹角
		public float AngleXOY;
		/// <summary>
		/// 移动速度
		/// </summary>
		public float MoveSpeed;

		private float _RadianZ;
		private float _RadianXOY;
		private Vector3 _Position;

		public EllipseController()
		{
			Center = Vector3.zero;

			Width = 2;
			Height = 1;	

			AngleZ = 0;
			AngleXOY = 0;
			MoveSpeed = 1;

			_RadianZ = 0;
			_RadianXOY = 0;
			_Position = Vector3.zero;
		}

		// Update is called once per frame
		void Update ()
		{
			AngleXOY += MoveSpeed * Time.deltaTime;

			_RadianZ = MathHelp.ConvertToRadian (AngleZ);
			_RadianXOY = MathHelp.ConvertToRadian (AngleXOY);

			_Position.x = Center.x + Width * Mathf.Sin (_RadianXOY) * Mathf.Cos(_RadianZ);
			_Position.y = Center.y + Height * Mathf.Sin (_RadianXOY) * Mathf.Sin(_RadianZ);
			_Position.z = Center.z + Height * Mathf.Cos (_RadianXOY);

			transform.position = _Position;
		}
	}
}