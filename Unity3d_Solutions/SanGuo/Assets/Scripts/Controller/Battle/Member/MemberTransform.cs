using System;
using UnityEngine;
using Game.Action;
using Model.Base;

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
		/// 最后一次播放动作的名称
		/// </summary>
		private string _LastActionName;

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
			_Transform = target;
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
	}
}

