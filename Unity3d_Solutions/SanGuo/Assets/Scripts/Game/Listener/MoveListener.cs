using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Listener
{
	/// <summary>
	/// 刚体接触状态
	/// </summary>
	public enum CollisionTouchStatus
	{
		/// <summary>
		/// 接触
		/// </summary>
		Enter,
		/// <summary>
		/// 停留
		/// </summary>
		Stay,
		/// <summary>
		/// 退出
		/// </summary>
		Exit,
	}
	/// <summary>
	/// 移动监听
	/// 如果是刚体，采用力，否则采用位移
	/// 挂在物体上
	/// </summary>
	public class MoveListener : MonoBehaviour
	{
		/// <summary>
		/// 转换对象
		/// </summary>
		private Rigidbody _Rigidbody;
		/// <summary>
		/// 速率
		/// </summary>
		public float SpeedRatio = 0.1f;
		/// <summary>
		/// 作用力
		/// </summary>
		public float ForceRatio = 1f;
		/// <summary>
		/// 作用力
		/// </summary>
		public float JumpForce = 200f;

		public MoveListener()
		{
			
		}

		void Start()
		{
			_Rigidbody = GetComponent<Rigidbody> ();

			GameInstance.GetInstance().Platform.AddKeyDownHandler(KeyCode.UpArrow, OnUpArrow);
			GameInstance.GetInstance().Platform.AddKeyDownHandler(KeyCode.DownArrow, OnDownArrow);
			GameInstance.GetInstance().Platform.AddKeyDownHandler(KeyCode.LeftArrow, OnLeftArrow);
			GameInstance.GetInstance().Platform.AddKeyDownHandler(KeyCode.RightArrow, OnRightArrow);

			GameInstance.GetInstance().Platform.AddKeyHandler(KeyCode.Space, KeyPhase.Began, OnJumpArrow);
		}

		void Update()
		{
		}

		/// <summary>
		/// 方向键上
		/// </summary>
		protected void OnUpArrow()
		{
			Vector3 vector = Vector3.forward;
			//vector.y = 0;

			UpdatePosition (vector);
		}

		/// <summary>
		/// 方向键下
		/// </summary>
		protected void OnDownArrow()
		{
			Vector3 vector = Vector3.forward * -1;
			//vector.y = 0;

			UpdatePosition (vector);
		}

		/// <summary>
		/// 方向键左
		/// </summary>
		protected void OnLeftArrow()
		{
			Vector3 vector = Vector3.right * -1;
			//vector.y = 0;

			UpdatePosition (vector);
		}

		/// <summary>
		/// 方向键右
		/// </summary>
		protected void OnRightArrow()
		{
			Vector3 vector = Vector3.right * 1;
			//vector.y = 0;

			UpdatePosition (vector);
		}

		/// <summary>
		/// 更新位置
		/// </summary>
		/// <param name="vector">Vector.</param>
		private void UpdatePosition(Vector3 vector)
		{
			if (_Rigidbody != null) {
				_Rigidbody.AddForce (vector * ForceRatio);
			} else {
				transform.Translate (vector * SpeedRatio);
			}
		}

		/// <summary>
		/// 跳跃
		/// </summary>
		protected void OnJumpArrow()
		{
			Vector3 vector = Camera.main.transform.up * JumpForce;
			//vector.x = 0;
			//vector.z = 0;
			if (_Rigidbody) {
				_Rigidbody.AddForce(vector);	
			}
		}
		  
		void OnTriggerEnter(Collider other)
		{
			Log.Warning ("[" + DateTime.Now.Ticks.ToString() + "]" + "OnTriggerEnter : " + other.name);
		}

		void OnTriggerStay(Collider other)
		{
			Log.Warning ("[" + DateTime.Now.Ticks.ToString() + "]" + "OnTriggerStay : " + other.name);
			//
			//ResetPosition ();
		}

		void OnTriggerExit(Collider other)
		{
			Log.Warning ("[" + DateTime.Now.Ticks.ToString() + "]" + "OnTriggerExit : " + other.name);
		}
	}
}

