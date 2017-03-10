using System;
using UnityEngine;
using Game.Action;
using Model.Base;
using Game.Helper;

namespace Controller.Battle.Member
{
	/// <summary>
	/// 空间变换对象
	/// </summary>
	public class MemberTransform
	{
		/// <summary>
		/// The transform.
		/// </summary>
		private Transform _Transform;
		/// <summary>
		/// 碰撞体
		/// </summary>
		private Collider _Collider;
		/// <summary>
		/// 碰撞检测半径
		/// </summary>
		private float _CollisionRadius;

		/// <summary>
		/// Gets the transform.
		/// </summary>
		/// <value>The transform.</value>
		public Transform Transform {
			get { 
				return _Transform;
			}
		}

		/// <summary>
		/// 碰撞体
		/// </summary>
		/// <value>The collider.</value>
		public Collider Collider {
			get { 
				return _Collider;
			}
		}

		/// <summary>
		/// 碰撞检测半径
		/// </summary>
		public float CollisionRadius {
			get { 
				return _CollisionRadius;
			}
		}


		/// <summary>
		/// 位置
		/// </summary>
		/// <value>The position.</value>
		public Vector3 Position {
			get { 
				if (_Transform == null) {
					return Vector3.zero;
				} else {
					return _Transform.position;
				}
			}
		}

		/// <summary>
		/// 旋转角度
		/// </summary>
		/// <value>The position.</value>
		public Vector3 Rotation {
			get { 
				if (_Transform == null) {
					return Vector3.zero;
				} else {
					return _Transform.eulerAngles;
				}
			}
		}

		public MemberTransform()
		{
			
		}

		/// <summary>
		/// 设置变换对象
		/// </summary>
		/// <param name="target">Transform.</param>
		public void SetTranform(Transform target)
		{
			if (target == null) {
				return;
			}
			_Transform = target;
			_Collider = target.GetComponent<Collider> ();
			if (_Collider == null) {
				return;
			}

			Bounds bounds = _Collider.bounds;
			_CollisionRadius = Mathf.Sqrt (Mathf.Pow (bounds.extents.x, 2) + Mathf.Pow (bounds.extents.z, 2));
		}
		/// <summary>
		/// 移动向量值
		/// </summary>
		/// <param name="position">Vector3.</param>
		public void WalkBy (Vector3 position)
		{
			if (_Transform == null) {
				return;
			}

			_Transform.Translate (position);
		}
		/// <summary>
		/// 移动到目的地
		/// </summary>
		/// <param name="position">Vector3.</param>
		public void WalkTo (Vector3 position)
		{
			if (_Transform == null) {
				return;
			}

			_Transform.position = position;
		}
		/// <summary>
		/// 旋转向量值
		/// </summary>
		/// <param name="rotation">Vector3.</param>
		public void RotateBy (Vector3 rotation)
		{
			if (_Transform == null) {
				return;
			}

			_Transform.Rotate (rotation);
		}
		/// <summary>
		/// 旋转到指定方向
		/// </summary>
		/// <param name="rotation">Vector3.</param>
		public void RotateTo (Vector3 rotation)
		{
			if (_Transform == null) {
				return;
			}

			_Transform.eulerAngles = rotation;
		}

		/// <summary>
		/// 碰撞检测
		/// </summary>
		/// <returns><c>true</c> if this instance is collision the specified target; otherwise, <c>false</c>.</returns>
		/// <param name="target">Target.</param>
		public bool IsCollision(MemberTransform target)
		{
			if (target == null) {
				return false;
			}

			if (Collider == null || target.Collider == null) {
				return false;
			}

			if (!Collider.enabled || !target.Collider.enabled) {
				return false;
			}

			return MathHelp.IsIntersect (Position, CollisionRadius, target.Position, target.CollisionRadius);
		}
	}
}

