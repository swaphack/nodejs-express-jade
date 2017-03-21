using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Controller.AI.Movement
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
		/// 终点
		/// </summary>
		private Vector3 _EndPosition;

		/// <summary>
		/// 是否无站点
		/// </summary>
		/// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
		public bool Empty {
			get { 
				return _WalkPoints.Count == 0;
			}
		}

		/// <summary>
		/// 站点数
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get { 
				return _WalkPoints.Count;
			}
		}

		public Traveler ()
		{
			_WalkPoints = new List<Vector3> ();
			this.Clear ();
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
		/// 终点
		/// </summary>
		/// <value>The end position.</value>
		public Vector3 EndPosition {
			get { 
				return _EndPosition;
			}
		}

		/// <summary>
		/// 下一站点
		/// </summary>
		/// <value>The next position.</value>
		public Vector3 NextPosition {
			get { 
				if (Empty) {
					return _CurrentPosition;
				}

				return _WalkPoints [0];
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

			int count = points.Count;
			for (int i = 0; i < count; i++) {
				_WalkPoints.Add (new Vector3 (points[i].x, points[i].y, points[i].z));
			}

			_CurrentPosition = _WalkPoints [0];
			_EndPosition = _WalkPoints [count - 1];
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

			int count = points.Count;
			for (int i = 0; i < count; i++) {
				_WalkPoints.Add (new Vector3 (points[i].x, y, points[i].y));
			}
			_CurrentPosition = _WalkPoints [0];
			_EndPosition = _WalkPoints [count - 1];
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

			int count = points.Count;
			for (int i = 0; i < count; i++) {
				_WalkPoints.Add (new Vector3 (points[i].x, points[i].y, 0));
			}

			_CurrentPosition = _WalkPoints [0];
			_EndPosition = _WalkPoints [count - 1];
		}

		/// <summary>
		/// 请求下一站点坐标
		/// </summary>
		/// <returns><c>true</c>, if station was gotten, <c>false</c> otherwise.</returns>
		/// <param name="idx">Index.</param>
		/// <param name="position">Position.</param>
		public bool GetStation(int idx, out Vector3 position)
		{
			position = Vector3.zero;
			if (idx < 0 || idx >= _WalkPoints.Count) {
				return false;
			}

			position = _WalkPoints [idx];

			return true;
		}


		/// <summary>
		/// 初始化站点
		/// </summary>
		/// <param name="position">Position.</param>
		private void InitStation(out Vector3 position)
		{
			position = _CurrentPosition;

			if (_WalkPoints.Count == 0) {
				return;
			}

			position = _WalkPoints [0];
			if (_WalkPoints.Count >= 2) {
				float distance = Vector3.Distance (_WalkPoints [1], _WalkPoints [0]);
				if (distance > 0) {
					_CurrentDisplacement = (_WalkPoints [1] - _WalkPoints [0]) / distance;
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
			_WalkPoints.Clear();
			_CurrentDisplacement = Vector3.zero;
			_CurrentPosition = Vector3.zero;
			_EndPosition = Vector3.zero;
		}

		/// <summary>
		/// 获取下一时间段的位移
		/// </summary>
		/// <returns><c>true</c>, if next station was gotten, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		/// <param name="nextPosition">Next position.</param>
		public bool GetNextStation(float dt, out Vector3 nextPosition)
		{
			if (!TryGetNextStation (dt, out nextPosition)) {
				return false;
			}

			_CurrentPosition = nextPosition;

			return true;
		}

		/// <summary>
		/// 尝试获取下次抵达的坐标，不改变内部数据
		/// </summary>
		/// <returns><c>true</c>, if get next station was tryed, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		/// <param name="nextPosition">Next position.</param>
		public bool TryGetNextStation(float dt, out Vector3 nextPosition)
		{
			nextPosition = _CurrentPosition;

			// 抵达目标
			if (_WalkPoints.Count == 0) {
				return false;
			}

			// 抵达当前站点目标
			if (nextPosition == _WalkPoints [0]) {
				InitStation (out nextPosition);
				_WalkPoints.RemoveAt (0);
			}

			if (_WalkPoints.Count == 0) {
				return false;
			}

			Vector3 lastPosition = nextPosition;
			nextPosition = lastPosition + dt * _CurrentDisplacement;

			// 修正
			if (Vector3.Distance(lastPosition, _WalkPoints[0]) < Vector3.Distance(lastPosition, nextPosition)) {
				nextPosition = _WalkPoints [0];
			}

			return true;
		}

		/// <summary>
		/// 显示路径
		/// </summary>
		/// <returns>The path.</returns>
		public void ShowPath()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder ();
			builder.Append ("Walk Path : ");
			for (int i = 0, count = _WalkPoints.Count; i < count; i++) {
				builder.AppendFormat ("({0},{1},{2})=>", _WalkPoints [i].x, _WalkPoints [i].y, _WalkPoints [i].z);
			}

			Log.Info(builder.ToString ());
		}
	}
}

