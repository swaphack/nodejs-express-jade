using System;
using UnityEngine;

namespace Game.Helper
{
	/// <summary>
	/// 数学工具
	/// </summary>
	public class MathHelp
	{
		private MathHelp ()
		{
		}

		public static Quaternion QuaternionFromMatrix(Matrix4x4 m) 
		{ 
			return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1)); 
		}

		/// <summary>
		/// 3d坐标转为2d坐标
		/// </summary>
		/// <returns>The D to2 d.</returns>
		/// <param name="position">Position.</param>
		public static Vector2 Convert3DTo2D(Vector3 position)
		{
			return new Vector2 (position.x, position.z);
		}

		/// <summary>
		/// 求两向量间的面积
		/// </summary>
		/// <param name="point0">Point0.</param>
		/// <param name="point1">Point1.</param>
		/// <param name="point2">Point2.</param>
		public static float GetVector2DArea(Vector2 vector0, Vector2 vector1)
		{
			return 0.5f * Math.Abs (vector0.x * vector1.y - vector0.y * vector1.x);
		}

		/// <summary>
		/// 求三点间的面积
		/// </summary>
		/// <param name="point0">Point0.</param>
		/// <param name="point1">Point1.</param>
		/// <param name="point2">Point2.</param>
		public static float GetPoint2DArea(Vector2 point0, Vector2 point1, Vector2 point2)
		{
			return GetVector2DArea (point1 - point0, point2 - point0);
		}

		/// <summary>
		/// 求两向量间的面积
		/// </summary>
		/// <returns>The vector3 D area.</returns>
		/// <param name="vector0">Vector0.</param>
		/// <param name="vector1">Vector1.</param>
		public static float GetVector3DArea(Vector3 vector0, Vector3 vector1)
		{
			return 0.5f * Math.Abs (
				vector0.x *( vector1.y - vector1.z) 
				+ vector0.y * (vector1.z - vector1.x)
				+ vector0.z * (vector1.x - vector1.y));
		}

		/// <summary>
		/// 求三点间的面积
		/// </summary>
		/// <returns>The point3 D area.</returns>
		/// <param name="point0">Point0.</param>
		/// <param name="point1">Point1.</param>
		/// <param name="point2">Point2.</param>
		public static float GetPoint3DArea(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			return GetVector3DArea (point1 - point0, point2 - point0);
		}

		/// <summary>
		///  获取从开始位置到目标位置的朝向旋转角度
		/// </summary>
		/// <returns>方向向量</returns>
		/// <param name="direction">Direction.</param>
		public static double GetRotation(Vector3 direction)
		{
			return Math.Atan2 (direction.z, direction.x) * 180 / Math.PI;
		}
	}
}

