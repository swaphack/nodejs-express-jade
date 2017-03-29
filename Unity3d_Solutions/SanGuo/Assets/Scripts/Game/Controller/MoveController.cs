using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;
using Game.Platform;
using Model.Base;

namespace Game.Controller
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
	public class MoveController : MonoBehaviour
	{
		/// <summary>
		/// 速率
		/// </summary>
		public float MoveSpeed;
		/// <summary>
		/// 作用力
		/// </summary>
		public float ForcePower;
		/// <summary>
		/// 等待响应时间
		/// </summary>
		public float WaitResponseLatence;
		/// <summary>
		/// 等待时间
		/// </summary>
		private float _WaitTime = 0;
		/// <summary>
		/// 动作
		/// </summary>
		private Animator _Animator;

		public MoveController()
		{
			MoveSpeed = 2.0f;
			ForcePower = 300.0f;
			WaitResponseLatence = 1.0f;
		}

		void Start()
		{
			GameInstance.GetInstance().Device.AddKeyDownHandler(KeyCode.W, OnUpArrow);
			GameInstance.GetInstance().Device.AddKeyDownHandler(KeyCode.S, OnDownArrow);
			GameInstance.GetInstance().Device.AddKeyDownHandler(KeyCode.A, OnLeftArrow);
			GameInstance.GetInstance().Device.AddKeyDownHandler(KeyCode.D, OnRightArrow);

			GameInstance.GetInstance ().Device.AddKeyHandler (KeyCode.J, KeyPhase.Began, OnAttack01);
			GameInstance.GetInstance ().Device.AddKeyHandler (KeyCode.I, KeyPhase.Began, OnAttack02);
			GameInstance.GetInstance ().Device.AddKeyHandler (KeyCode.K, KeyPhase.Began, OnAttack03);
			GameInstance.GetInstance ().Device.AddKeyHandler (KeyCode.L, KeyPhase.Began, OnDefend);

			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Space, KeyPhase.Began, OnJumpArrow);


			_Animator = GetComponentInChildren<Animator> ();
		}

		void Update()
		{
			if (_WaitTime > 0) {
				_WaitTime -= Time.deltaTime;
				if (_WaitTime <= 0) {
					PlayAction (ActionConstants.t_idle_01);
				}
			}
		}

		void OnDestory()
		{
			GameInstance.GetInstance().Device.RemoveKeyDownHandler(KeyCode.W, OnUpArrow);
			GameInstance.GetInstance().Device.RemoveKeyDownHandler(KeyCode.S, OnDownArrow);
			GameInstance.GetInstance().Device.RemoveKeyDownHandler(KeyCode.A, OnLeftArrow);
			GameInstance.GetInstance().Device.RemoveKeyDownHandler(KeyCode.D, OnRightArrow);

			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Space, KeyPhase.Began, OnJumpArrow);
		}

		/// <summary>
		/// 方向键上
		/// </summary>
		protected void OnUpArrow()
		{			
			Vector3 vector = transform.forward;
			UpdateDirection (vector);
		}

		/// <summary>
		/// 方向键下
		/// </summary>
		protected void OnDownArrow()
		{			
			Vector3 vector = transform.forward * -1;
			UpdateDirection (vector);
		}

		/// <summary>
		/// 方向键左
		/// </summary>
		protected void OnLeftArrow()
		{		
			Vector3 vector = transform.right * -1;
			UpdateDirection (vector);
		}

		/// <summary>
		/// 方向键右
		/// </summary>
		protected void OnRightArrow()
		{			
			Vector3 vector = transform.right * 1;
			UpdateDirection (vector);
		}

		/// <summary>
		/// 更新方向
		/// </summary>
		/// <param name="direction">Direction.</param>
		private void UpdateDirection(Vector3 direction)
		{
			if (transform.forward == direction) { // 前进
				OnWalkForward(direction);
			} else if (-1.0f * transform.forward == direction) { // 后退
				OnWalkForward(direction);
			} else { // 左右旋转
				OnTurnDirection(direction);
			}

			_WaitTime = WaitResponseLatence;
		}


		/// <summary>
		/// 是否播放动作
		/// </summary>
		/// <returns><c>true</c> if this instance is play the specified name; otherwise, <c>false</c>.</returns>
		/// <param name="name">Name.</param>
		private bool IsPlay(int name)
		{
			if (_Animator == null) {
				return false;
			}

			AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo (0);
			return stateInfo.shortNameHash == name;
		}


		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="name">Name.</param>
		private void PlayAction(int name)
		{
			if (_Animator == null) {
				return;
			}

			if (IsPlay (name)) {
				return;
			}

			_Animator.Play (name);
		}

		/// <summary>
		/// 前进
		/// </summary>
		/// <param name="vector">Vector.</param>
		private void OnWalkForward(Vector3 vector)
		{
			if (IsPlay (ActionConstants.t_attack_01)
				|| IsPlay (ActionConstants.t_attack_02)
				|| IsPlay (ActionConstants.t_attack_03)
				|| IsPlay (ActionConstants.t_defend)) {
				return;
			}

			transform.position += vector * MoveSpeed * Time.deltaTime; 
			PlayAction (ActionConstants.t_walkBattleForward);
		}

		/// <summary>
		/// 后退
		/// </summary>
		/// <param name="vector">Vector.</param>
		private void OnWalkBack(Vector3 vector)
		{
			if (IsPlay (ActionConstants.t_attack_01)
				|| IsPlay (ActionConstants.t_attack_02)
				|| IsPlay (ActionConstants.t_attack_03)
				|| IsPlay (ActionConstants.t_defend)) {
				return;
			}

			transform.position += vector * MoveSpeed * Time.deltaTime; 
			PlayAction (ActionConstants.t_walkBattleBackward);
		}

		/// <summary>
		/// 转向
		/// </summary>
		/// <param name="vector">Vector.</param>
		private void OnTurnDirection(Vector3 vector)
		{
			if (IsPlay (ActionConstants.t_attack_01)
				|| IsPlay (ActionConstants.t_attack_02)
				|| IsPlay (ActionConstants.t_attack_03)
				|| IsPlay (ActionConstants.t_defend)) {
				return;
			}
			Quaternion q0 = Quaternion.LookRotation (transform.forward);
			Quaternion q1 = Quaternion.LookRotation (vector);
			Quaternion q = Quaternion.Slerp (q0, q1, Time.deltaTime);
			transform.eulerAngles = q.eulerAngles;

			if (IsPlay (ActionConstants.t_walkBattleForward) 
				|| IsPlay (ActionConstants.t_walkBattleBackward)) {
				return;
			}

			PlayAction (ActionConstants.t_walk);
		}

		/// <summary>
		/// 跳跃
		/// </summary>
		private void OnJumpArrow()
		{
			Rigidbody body = GetComponent<Rigidbody> ();
			if (body == null) {
				return;
			}

//			if (!Mathf.Approximately(transform.position.y, 2.0f)) {
//				return;
//			}
			body.AddForce (Vector3.up * ForcePower);
		}

		/// <summary>
		/// 攻击1
		/// </summary>
		private void OnAttack01()
		{
			if (IsPlay (ActionConstants.t_attack_01)
				|| IsPlay (ActionConstants.t_attack_02)
				|| IsPlay (ActionConstants.t_attack_03)
				|| IsPlay (ActionConstants.t_defend)) {
				return;
			}

			PlayAction (ActionConstants.t_attack_01);

			_WaitTime = WaitResponseLatence;
		}

		/// <summary>
		/// 攻击2
		/// </summary>
		private void OnAttack02()
		{
			if (IsPlay (ActionConstants.t_attack_01)
				|| IsPlay (ActionConstants.t_attack_02)
				|| IsPlay (ActionConstants.t_attack_03)
				|| IsPlay (ActionConstants.t_defend)) {
				return;
			}

			PlayAction (ActionConstants.t_attack_02);

			_WaitTime = WaitResponseLatence;
		}

		/// <summary>
		/// 攻击3
		/// </summary>
		private void OnAttack03()
		{
			if (IsPlay (ActionConstants.t_attack_01)
				|| IsPlay (ActionConstants.t_attack_02)
				|| IsPlay (ActionConstants.t_attack_03)
				|| IsPlay (ActionConstants.t_defend)) {
				return;
			}

			PlayAction (ActionConstants.t_attack_03);

			_WaitTime = WaitResponseLatence;
		}

		/// <summary>
		/// 防御
		/// </summary>
		private void OnDefend()
		{
			PlayAction (ActionConstants.t_defend);

			_WaitTime = WaitResponseLatence;
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

