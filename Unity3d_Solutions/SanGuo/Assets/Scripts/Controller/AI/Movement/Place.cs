using System.Collections.Generic;
using UnityEngine;

namespace Controller.AI.Movement
{
	/// <summary>
	/// 阵型
	/// </summary>
	public class Place
	{
		/// <summary>
		/// 信息
		/// </summary>
		internal struct PlaceInfo 
		{
			/// <summary>
			/// 位置
			/// </summary>
			public Vector3 Position;
			/// <summary>
			/// 朝向
			/// </summary>
			public Vector3 Orientation;

			public PlaceInfo(Vector3 position)
			{
				Position = position;
				Orientation = Vector3.zero;
			}

			public PlaceInfo(PlaceInfo information)
			{
				Position = information.Position;
				Orientation = information.Orientation;
			}
		}

		/// <summary>
		/// 单位信息
		/// </summary>
		private List<PlaceInfo> _UnitInfos;
		/// <summary>
		/// 实时信息
		/// </summary>
		private PlaceInfo[] _RealTimeInfos;
		/// <summary>
		/// 信息数
		/// </summary>
		/// <value>The information count.</value>
		public int InfoCount {
			get { 
				return _UnitInfos.Count;
			}
		}

		public Place ()
		{
			_UnitInfos = new List<PlaceInfo> ();
		}

		/// <summary>
		/// 设置阵型单位信息
		/// </summary>
		/// <param name="positions">Positions.</param>
		public void SetInfos (Vector3[] positions)
		{
			if (positions == null || positions.Length == 0) {
				return;
			}

			_UnitInfos.Clear ();
			int length = positions.Length;
			for (int i = 0; i < length; i++) {
				_UnitInfos.Add (new PlaceInfo(positions[i]));
			}

			_RealTimeInfos = new PlaceInfo[length];
		}


		/// <summary>
		/// 获取位置
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="index">Index.</param>
		public Vector3 GetPosition(int index)
		{
			if (_RealTimeInfos == null || index < 0 || index >= _RealTimeInfos.Length) {
				return Vector3.zero;
			}

			return _RealTimeInfos [index].Position;
		}

		/// <summary>
		/// 获取朝向
		/// </summary>
		/// <returns>The rotation.</returns>
		/// <param name="index">Index.</param>
		public Vector3 GetRotation(int index)
		{
			if (index < 0 || index >= _RealTimeInfos.Length) {
				return Vector3.zero;
			}

			return _RealTimeInfos [index].Orientation;
		}


		/// <summary>
		///  集结
		/// </summary>
		/// <param name="center">目标点</param>
		/// <param name="direction">朝向</param>
		public void CalConcentrate(Vector3 center,  Vector3 direction)
		{
			float angle = Mathf.Atan2 (direction.z, direction.x);
			float sinA = Mathf.Sin (angle);
			float cosA = Mathf.Cos (angle);
			
			int count = _UnitInfos.Count;
			for (int i = 0; i < count; i++) {
				_RealTimeInfos [i].Position.x = _UnitInfos[i].Position.x * cosA - _UnitInfos[i].Position.z * sinA;
				_RealTimeInfos [i].Position.z = _UnitInfos[i].Position.x * sinA + _UnitInfos[i].Position.z * cosA;

				_RealTimeInfos [i].Position += center;
				_RealTimeInfos [i].Orientation.y = angle;
			}
		}
	}
}

