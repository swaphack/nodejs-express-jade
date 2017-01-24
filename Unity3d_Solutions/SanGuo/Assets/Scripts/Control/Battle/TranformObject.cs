using System;
using UnityEngine;

namespace Control.Battle
{
	/// <summary>
	/// 空间变换对象
	/// </summary>
	public class TranformObject
	{
		/// <summary>
		/// The transform.
		/// </summary>
		private Transform _Transform;

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
					return _Transform.localPosition;
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
					Vector3 vector;
					vector.x = _Transform.localRotation.x;
					vector.y = _Transform.localRotation.y;
					vector.z = _Transform.localRotation.z;
					return vector;
				}
			}
		}

		/// <summary>
		/// 设置变换对象
		/// </summary>
		/// <param name="transform">Transform.</param>
		public void SetTranform(Transform transform)
		{
			_Transform = transform;
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
			//_Transform.localPosition = position;

			Vector3 vector = position - Position;
			_Transform.Translate (vector);
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

			Vector3 vector = rotation - Rotation;
			_Transform.Rotate (vector);
		}
	}
}

