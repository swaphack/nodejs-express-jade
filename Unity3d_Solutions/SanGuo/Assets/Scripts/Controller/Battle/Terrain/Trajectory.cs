using System;
using UnityEngine;

namespace Controller.Battle.Terrain
{
	/// <summary>
	/// 弹道轨迹
	/// </summary>
	public class Trajectory
	{
		/// <summary>
		/// 目标
		/// </summary>
		private Transform _Target;

		/// <summary>
		/// 起始位置
		/// </summary>
		private Vector3 _FromPosition;

		/// <summary>
		/// 初始速度
		/// </summary>
		private Vector3 _FromSpeed;

		/// <summary>
		/// 质量
		/// </summary>
		private float _Mass;
		/// <summary>
		/// 重力系数
		/// </summary>
		private float _GravityRate;

		/// <summary>
		/// 当前速度
		/// </summary>
		private Vector3 _CurrentSpeed;
		/// <summary>
		/// 当前位移
		/// </summary>
		private Vector3 _CurrentPosition;

		/// <summary>
		/// 重力
		/// </summary>
		private Vector3 _GravityForce;

		/// <summary>
		/// 目标
		/// </summary>
		public Transform Target {
			get { 
				return _Target;
			}
			set { 
				_Target = value;
			}
		}

		/// <summary>
		/// 起始位置
		/// </summary>
		public Vector3 FromPosition {
			get { 
				return _FromPosition;
			}
			set { 
				_FromPosition = value;
			}
		}

		/// <summary>
		/// 初始速度
		/// </summary>
		public Vector3 FromSpeed {
			get { 
				return _FromSpeed;
			}
			set { 
				_FromSpeed = value;
			}
		}

		/// <summary>
		/// 质量
		/// </summary>
		public float Mass {
			get { 
				return _Mass;
			}
			set { 
				_Mass = value;
			}
		}

		/// <summary>
		/// 重力系数
		/// </summary>
		public float GravityRate {
			get { 
				return _GravityRate;
			}
			set { 
				_GravityRate = value;
			}
		}
		
		public Trajectory ()
		{
			_Mass = 1.0f;
			_GravityRate = 1;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			if (Target == null) {
				return;
			}

			// 重力
			float gravityForce = _Mass * _GravityRate;			

			_GravityForce = new Vector3 (0, -gravityForce, 0);

			_CurrentSpeed = _FromSpeed;
			_CurrentPosition = _FromPosition;
		}

		/// <summary>
		/// 更新时间
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (Target == null) {
				return;
			}

			Vector3 lastSpeed = _CurrentSpeed;
			Vector3 newSpeed = lastSpeed + _GravityForce * dt;

			Vector3 lastPosition = _CurrentPosition;
			Vector3 newPosition = lastPosition + (lastSpeed + newSpeed) * dt * 0.5f;

			Target.position = newPosition;
			Target.Rotate (Quaternion.FromToRotation (lastSpeed, newSpeed).eulerAngles);

			_CurrentSpeed = newSpeed;
			_CurrentPosition = newPosition;
		}
	}
}

