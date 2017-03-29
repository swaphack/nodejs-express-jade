using System.Collections.Generic;
using Controller.AI.Movement;
using UnityEngine;
using Game.Helper;

namespace Controller.Battle.Member
{
	/// <summary>
	/// 单位运动
	/// </summary>
	public class MemeberMovement : IMoveUnit
	{
		/// <summary>
		/// 单位
		/// </summary>
		private Unit _Target;
		/// <summary>
		/// 单位状态
		/// </summary>
		private UnitState _State;
		/// <summary>
		/// 单位是否独立
		/// </summary>
		private bool _IsIndividual;
		/// <summary>
		/// 当前运动标识
		/// </summary>
		private MoveType _Tag;
		/// <summary>
		/// 小组移动速度
		/// </summary>
		private float _GroupSpeed;
		/// <summary>
		/// 移动命令
		/// </summary>
		private MoveCommand _MoveCommand;
		/// <summary>
		/// 旅行者
		/// </summary>
		private Traveler _Traveler;
		/// <summary>
		/// 下一站
		/// </summary>
		private Vector3 _NextStation;
		/// <summary>
		/// 等待寻路时间
		/// </summary>
		private float _WaitForFindWay;
		/// <summary>
		/// 待旋转角度
		/// </summary>
		private float _Rotation;
		/// <summary>
		/// 旋转角度
		/// </summary>
		private float _RemainRotation;

		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				if (_Target == null) {
					return 0;
				}

				return _Target.ID;
			} 
		}
		/// <summary>
		/// 单位状态
		/// </summary>
		/// <value>The state.</value>
		public UnitState State { 
			get {
				return _State;
			} 
		}
		/// <summary>
		/// 位置
		/// </summary>
		/// <value>The position.</value>
		public Vector3 Position {
			get {
				if (_Target == null) {
					return Vector3.zero;
				}

				return _Target.MemberTransform.Position;
			}
		} 
		/// <summary>
		/// 朝向
		/// </summary>
		/// <value>The orientation.</value>
		public Vector3 Orientation {
			get {
				if (_Target == null) {
					return Vector3.zero;
				}

				return _Target.MemberTransform.Orientation;
			}
		} 
		/// <summary>
		/// 单位是否独立
		/// </summary>
		/// <value><c>true</c> if this instance is individual; otherwise, <c>false</c>.</value>
		public bool IsIndividual { 
			get {
				return _IsIndividual;
			}
			set {
				_IsIndividual = value;
			}
		}
		/// <summary>
		/// 是否是碰撞体
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool IsCollider { 
			get {
				if (_Target == null) {
					return false;
				}
				return _Target.IsCollider;
			}
		}
		/// <summary>
		/// 移动速度
		/// </summary>
		/// <value>The move speed.</value>
		public float MoveSpeed { 
			get {
				return _Target.Property.GetValue(Model.Base.PropertyType.MoveSpeed);
			}
		}
		/// <summary>
		/// 小组移动速度
		/// </summary>
		/// <value>The group speed.</value>
		public float GroupSpeed { 
			get { 
				return _GroupSpeed;
			}
			set { 
				_GroupSpeed = value;
			}
		}

		/// <summary>
		/// 单位
		/// </summary>
		/// <value>The target.</value>
		public Unit Target {
			get { 
				return _Target;
			}
			set { 
				_Target = value;
			}
		}

		/// <summary>
		/// 旅行者
		/// </summary>
		/// <value>The walker.</value>
		public Traveler Traveler {
			get { 
				return _Traveler;
			}
		}

		/// <summary>
		/// 是否在等待
		/// </summary>
		/// <value><c>true</c> if this instance is wait for walk; otherwise, <c>false</c>.</value>
		public bool IsWaitForWalk {
			get { 
				return _WaitForFindWay > 0;
			}	
		}

		/// <summary>
		/// 当前运动标识
		/// </summary>
		/// <value>The tag.</value>
		public MoveType Tag {
			get { 
				return _Tag;
			}
		}

		public MemeberMovement ()
		{
			_MoveCommand = new MoveCommand ();
			_Traveler = new Traveler ();
			_IsIndividual = true;

			_WaitForFindWay = 0;
		}

		/// <summary>
		/// 是否与其他单位碰撞
		/// </summary>
		/// <returns><c>true</c> if this instance is collide the specified unit; otherwise, <c>false</c>.</returns>
		/// <param name="unit">Unit.</param>
		public bool IsCollideWith (IMoveUnit unit)
		{
			if (unit == null || _Target == null) {
				return false;
			}

			MemeberMovement member = unit as MemeberMovement;
			if (member == null || member.Target == null) {
				return false;
			}

			return _Target.MemberTransform.IsCollideWith (member.Target.MemberTransform);
		}

		/// <summary>
		/// 移动到指定目标
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="bIndividual">If set to <c>true</c> b individual.</param>
		/// <param name="tag">Tag.</param>
		public void MoveTo (Vector3 position, bool bIndividual = true, MoveType tag = MoveType.Normal)
		{
			bool isIndividual;
			MoveType nTag;
			if (!_MoveCommand.GetDestination (out _NextStation, out isIndividual, out nTag)) {
				_MoveCommand.AddDestination (position, bIndividual, tag);
				return;
			}
			if (_NextStation != position) {
				_MoveCommand.AddDestination (position, bIndividual, tag);
				return;
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update (float dt)
		{
			if (_WaitForFindWay > 0) {
				_WaitForFindWay -= dt;
				_Target.StopWalk ();
				return;
			}

			if (Empty) {
				return;
			}

			if (!_Traveler.Empty) {
				UpdateWalk (dt);
				return;
			}

			if (_MoveCommand.Empty) {
				return;
			}

			FindWay ();
		}


		/// <summary>
		/// 是否在行走中
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateWalk(float dt) 
		{
			if (_RemainRotation != 0) {
				float detla = _Rotation * dt;
				if (detla > _RemainRotation) {
					detla = _RemainRotation;
				}
				_Target.MemberTransform.RotateBy (new Vector3(0, detla, 0));
				_RemainRotation -= detla;
				return;
			}

			Vector3 nextPosition;
			if (!_Target.MemeberMovement.GetNextStation (dt, out nextPosition)) {
				_Target.StopWalk ();
				return;
			}

			Vector3 nextStation;
			_Target.MemeberMovement.Traveler.GetStation (0, out nextStation);

			Vector3 v0 = _Target.MemberTransform.Orientation;
			Vector3 v1 = nextStation - nextPosition;
			float angle = Quaternion.FromToRotation (v0, v1).eulerAngles.y;
			angle = 0;
			if (angle != 0) { // 不共线，调整方向
				if (angle > 180 ) {
					angle = -(360 - angle);
				}
				_Rotation = angle;
				_RemainRotation = angle;
			} else { // 共线
				_Target.MemberModel.PlayWalk ();
				_Target.MemberTransform.LookAt (nextPosition);
				_Target.MemberTransform.WalkTo (nextPosition);
			}
		}

		/// <summary>
		/// 寻路
		/// </summary>
		private void FindWay()
		{
			if (_MoveCommand.Empty) {
				return;
			}

			bool isIndividual;
			MoveType tag;
			if (!_MoveCommand.GetDestination (out _NextStation, out isIndividual, out tag)) {
				return;
			}
				
			if (!isIndividual) {
				_State = UnitState.Forming;
			} else {
				_State = UnitState.Broken;
			}

			/*if (Vector3.Distance(Position, _NextStation) <= 0.01f) {*/
			if (Position == _NextStation) { // 抵达目标
				_Traveler.Clear ();
				_MoveCommand.ArrivedDestination ();
				if (!isIndividual) {
					_State = UnitState.Formed;
				}
				_Tag = 0;
			} else {
				Field field = BattleHelp.Field;
				List<Vector2> path = field.Map.FindWay (Position, _NextStation, !isIndividual);
				if (path != null && path.Count != 0) {
					_IsIndividual = isIndividual;
					_Tag = tag;
					if (_IsIndividual) {
						_Traveler.MoveSpeed = MoveSpeed;
					} else {
						_Traveler.MoveSpeed = GroupSpeed;
					}

					_Traveler.Set3DWayBy2D (path, Position.y);
				} else {
					ResetWaitForFindWayTime ();
					Log.Error ("No Way Found From " + Position + " To " + _NextStation);
				}
			}
		}

		/// <summary>
		/// 重置等待时间
		/// </summary>
		public void ResetWaitForFindWayTime()
		{
			_WaitForFindWay = Random.Range (2.0f, 3.0f);
		}

		/// <summary>
		/// 重新寻路
		/// </summary>
		public void RefindWay()
		{
			_Traveler.Clear ();

			FindWay ();
		}

		/// <summary>
		/// 获取下一时间段的位移
		/// </summary>
		/// <returns><c>true</c>, if next station was gotten, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		/// <param name="nextPosition">Next position.</param>
		public bool GetNextStation(float dt, out Vector3 nextPosition)
		{
			return _Traveler.GetNextStation(dt, out nextPosition);
		}

		/// <summary>
		/// 尝试获取下次抵达的坐标，不改变内部数据
		/// </summary>
		/// <returns><c>true</c>, if get next station was tryed, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		/// <param name="nextPosition">Next position.</param>
		public bool TryGetNextStation(float dt, out Vector3 nextPosition)
		{
			return _Traveler.TryGetNextStation(dt, out nextPosition);
		}

		/// <summary>
		/// 目标是否在路线上
		/// </summary>
		/// <returns><c>true</c> if this instance is target on path the specified dt position radius; otherwise, <c>false</c>.</returns>
		/// <param name="dt">Dt.</param>
		/// <param name="position">Position.</param>
		/// <param name="radius">Radius.</param>
		public bool IsTargetOnPath(float dt, Vector3 position, float radius)
		{
			if (_Target == null || _Traveler.Empty) {
				return false;
			}

			Vector3 destPos;
			if (!TryGetNextStation (dt, out destPos)) {
				return false;
			}

			float targetRadius = _Target.MemberTransform.CollisionRadius;

			if (Vector3.Distance (position, destPos) <= targetRadius + radius) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// 是否为空
		/// </summary>
		/// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
		public bool Empty {
			get { 
				return _Traveler == null || (_Traveler.Empty && _MoveCommand.Empty);
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear()
		{
			_Traveler.Clear ();
			_MoveCommand.Clear ();
		}
	}
}

