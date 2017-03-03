using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.AI
{
	/// <summary>
	/// 旅行者
	/// </summary>
	public class Traveler
	{
		/// <summary>
		/// 移动的点
		/// </summary>
		private List<Vector3> _WalkPoints;
		/// <summary>
		/// 移动速度， 每一单位的距离/移动完该单位所需的时间
		/// </summary>
		private float _MoveSpeed;
		/// <summary>
		/// 当前位移
		/// </summary>
		private Vector3 _CurrentDisplacement;
		/// <summary>
		/// 当前位置
		/// </summary>
		private Vector3 _CurrentPosition;

		/// <summary>
		/// 是否无站点
		/// </summary>
		/// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
		public bool Empty {
			get { 
				return _WalkPoints.Count == 0;
			}
		}

		public Traveler ()
		{
			_WalkPoints = new List<Vector3> ();
			_MoveSpeed = 1;
			_CurrentDisplacement = Vector3.zero;
			_CurrentPosition = Vector3.zero;
		}

		/// <summary>
		/// 移动速度
		/// </summary>
		public float MoveSpeed {
			get { 
				return _MoveSpeed;
			}
			set { 
				_MoveSpeed = value;
			}
		}

		/// <summary>
		/// 当前位置
		/// </summary>
		/// <value>The current position.</value>
		public Vector3 CurrentPosition {
			get { 
				return _CurrentPosition;
			}
		}

		/// <summary>
		/// 设置3d路线
		/// </summary>
		/// <param name="points">Points.</param>
		public void Set3DWay(List<Vector3> points)
		{
			if (points == null || points.Count == 0) {
				return;
			}

			_WalkPoints.Clear ();

			foreach (Vector3 item in points) {
				_WalkPoints.Add (new Vector3 (item.x, item.y, item.z));
			}

			_CurrentPosition = _WalkPoints [0];
		}

		/// <summary>
		/// 以2d模板设置3d路线
		/// </summary>
		/// <param name="points">Points.</param>
		/// <param name="y">The y coordinate.</param>
		public void Set3DWayBy2D(List<Vector2> points, float y)
		{
			if (points == null || points.Count == 0) {
				return;
			}

			_WalkPoints.Clear ();

			foreach (Vector3 item in points) {
				_WalkPoints.Add (new Vector3 (item.x, 0, item.y));
			}
			_CurrentPosition = _WalkPoints [0];
		}

		/// <summary>
		/// 设置2d路线
		/// </summary>
		/// <param name="points">Points.</param>
		public void Set2DWay(List<Vector2> points)
		{
			if (points == null || points.Count == 0) {
				return;
			}

			_WalkPoints.Clear ();

			foreach (Vector2 item in points) {
				_WalkPoints.Add (new Vector3 (item.x, item.y, 0));
			}

			_CurrentPosition = _WalkPoints [0];
		}


		/// <summary>
		/// 初始化站点
		/// </summary>
		public void InitStation()
		{
			if (_WalkPoints.Count == 0) {
				return;
			}

			_CurrentPosition = _WalkPoints [0];
			if (_WalkPoints.Count >= 2) {
				float distance = Vector3.Distance (_WalkPoints [1], _WalkPoints [0]);
				if (distance > 0) {
					_CurrentDisplacement = _MoveSpeed * (_WalkPoints [1] - _WalkPoints [0]) / distance;
				} else {
					_CurrentDisplacement = Vector3.zero;
				}
			}
		}

		/// <summary>
		/// 清空站点
		/// </summary>
		public void Clear() 
		{
			_WalkPoints.Clear ();
		}

		/// <summary>
		/// 获取下一时间段的位移
		/// </summary>
		/// <returns><c>true</c>, if next way was gotten, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		/// <param name="deltaPosition">Delta position.</param>
		public bool GetNextStation(float dt, out Vector3 deltaPosition)
		{
			deltaPosition = Vector3.zero;

			// 抵达目标
			if (_WalkPoints.Count == 0) {
				return false;
			}

			// 抵达当前站点目标
			if (_CurrentPosition == _WalkPoints [0]) {
				InitStation ();
				_WalkPoints.RemoveAt (0);
			}

			if (_WalkPoints.Count == 0) {
				return false;
			}

			Vector3 lastPosition = _CurrentPosition;
			Vector3 newPosition = lastPosition + dt * _CurrentDisplacement;

			// 修正
			if (Vector3.Distance(lastPosition, _WalkPoints[0]) < Vector3.Distance(lastPosition, newPosition)) {
				newPosition = _WalkPoints [0];
			}

			deltaPosition = newPosition - lastPosition;

			_CurrentPosition = newPosition;

			return true;
		}
	}
}

