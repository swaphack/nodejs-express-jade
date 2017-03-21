using System;
using UnityEngine;

namespace Game.Track
{
	/// <summary>
	/// 弧形轨迹
	/// </summary>
	public class Curve
	{
		/// <summary>
		/// 起始点
		/// </summary>
		public Vector3 SrcPosition;
		/// <summary>
		/// 终止点
		/// </summary>
		public Vector3 DestPosition;
		/// <summary>
		/// 高度
		/// </summary>
		public float Height;

		public Curve ()
		{
		}

		/// <summary>
		/// 生成点
		/// </summary>
		/// <returns>The positions.</returns>
		/// <param name="src">起始坐标</param>
		/// <param name="dest">终止坐标</param>
		/// <param name="height">高度</param>
		/// <param name="direction">弧形方向</param>
		/// <param name="count">切割数</param>
		public static Vector3[] GetPositionsByYOrder(Vector3 src, Vector3 dest, float height, int count)
		{
			// 长度
			float length = Vector3.Distance (src, dest);
			// 垂直中心距离两端点的直线距离
			float dh = (length * 0.5f * length * 0.5f - height * height) / (2 * height);

			// 中心点位置
			Vector3 center = (src + dest) * 0.5f;

			// 球心位置
			Vector3 verticalCenter = center;
			verticalCenter.y -= dh;

			// 方向向量
			Vector3 srcVector = src - verticalCenter;
			Vector3 destVector = dest - verticalCenter;

			// 球面插值
			Vector3[] positions = new Vector3[count];
			positions [0] = src;
			for (int i = 1; i <= count - 1; i++) {
				positions[i] = Vector3.Slerp (srcVector, destVector, 1.0f * i / (count - 1));
				positions[i] += verticalCenter;
			}

			return positions;
		}
	}
}

