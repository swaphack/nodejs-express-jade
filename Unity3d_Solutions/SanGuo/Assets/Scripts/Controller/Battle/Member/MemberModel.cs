using System;
using UnityEngine;
using Model.Base;
using Game.Helper;

namespace Controller.Battle.Member
{
	/// <summary>
	/// 模型对象
	/// </summary>
	public class MemberModel
	{
		/// <summary>
		/// 动作模型
		/// </summary>
		/// <value>The animator.</value>
		private Animator _Animator;
		/// <summary>
		/// The transform.
		/// </summary>
		private Transform _Transform;

		/// <summary>
		/// 开始播放动作
		/// </summary>
		public event OnActionBroadcast OnActionStart;
		/// <summary>
		/// 动作播放停止
		/// </summary>
		public event OnActionBroadcast OnActionEnd;
		/// <summary>
		/// 最近一次播放动作的名称
		/// </summary>
		private int _lastActionName = 0;
		/// <summary>
		/// 模型对象
		/// </summary>
		/// <value>The trans form.</value>
		internal Transform Transform {
			 get { 
				return _Transform;
			}
		}

		public MemberModel ()
		{
		}

		/// <summary>
		/// 设置变换对象
		/// </summary>
		/// <param name="target">Transform.</param>
		public void SetTranform(Transform target)
		{
			if (target == null) {
				_Transform = null;
				_Animator = null;
				return;
			}
			_Transform = target;
			_Animator = _Transform.GetComponent<Animator> ();
		}

		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="name">Name.</param>
		public void PlayAction(int name)
		{
			if (_Animator == null) {
				return;
			}

			AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.shortNameHash == name && stateInfo.normalizedTime < 1.0f) {
				return;
			}

			_lastActionName = name;

			_Animator.Play (name);
			//_Animator.CrossFade (name, 0.1f);
			if (OnActionStart != null) {
				OnActionStart (name);
			}
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Pause()
		{
			if (_Animator == null) {
				return;
			}
			_Animator.speed = 0;
		}

		/// <summary>
		/// 恢复
		/// </summary>
		public void Resume()
		{
			if (_Animator == null) {
				return;
			}
			_Animator.speed = 1;

		}

		/// <summary>
		/// 是否正在播放动画
		/// </summary>
		/// <returns><c>true</c> if this instance is playing; otherwise, <c>false</c>.</returns>
		public bool IsPlaying()
		{
			if (_Animator == null) {
				return false;
			}
			return _Animator.speed != 0;
		}

		/// <summary>
		/// 是否正在播放动作
		/// </summary>
		/// <returns><c>true</c> if this instance is play the specified tag; otherwise, <c>false</c>.</returns>
		/// <param name="tag">Tag.</param>
		public bool IsPlay(int tag) {
			if (_Animator == null) {
				return false;
			}

			AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.shortNameHash == tag) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (_Animator == null) {
				return;
			}
				
			AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo (0);

			if (stateInfo.shortNameHash != _lastActionName) {
				if (OnActionEnd != null) {
					OnActionEnd (_lastActionName);
				}
				// 播放完当前动作
				_lastActionName = stateInfo.shortNameHash;
			} else if (OnActionEnd != null && stateInfo.normalizedTime >= 1) {
				// 播放完一轮当前动作
				OnActionEnd (stateInfo.shortNameHash);
			}
		}

		/// <summary>
		/// 攻击
		/// </summary>
		public void PlayAttack01()
		{
			PlayAction(ActionConstants.t_attack_01);
		}

		/// <summary>
		/// 攻击
		/// </summary>
		public void PlayAttack02()
		{
			PlayAction(ActionConstants.t_attack_02);
		}

		/// <summary>
		/// 攻击
		/// </summary>
		public void PlayAttack03()
		{
			PlayAction(ActionConstants.t_attack_03);
		}

		/// <summary>
		/// 后退
		/// </summary>
		public void PlayWalkBackward()
		{
			PlayAction(ActionConstants.t_walkBattleBackward);
		}

		/// <summary>
		/// 前进
		/// </summary>
		public void PlayWalkForward()
		{
			PlayAction(ActionConstants.t_walkBattleForward);
		}

		/// <summary>
		/// 左移
		/// </summary>
		public void PlayWalkLeft()
		{
			PlayAction(ActionConstants.t_walkBattleLeft);
		}

		/// <summary>
		/// 右移
		/// </summary>
		public void PlayWalkRight()
		{
			PlayAction(ActionConstants.t_walkBattleRight);
		}

		/// <summary>
		/// 防卫
		/// </summary>
		public void PlayDefend()
		{
			PlayAction(ActionConstants.t_defend);
		}

		/// <summary>
		/// 死亡
		/// </summary>
		public void PlayDie()
		{
			PlayAction(ActionConstants.t_die);
		}

		/// <summary>
		/// 受击
		/// </summary>
		public void PlayGetHit()
		{
			PlayAction(ActionConstants.t_getHit);
		}

		/// <summary>
		/// 起跳
		/// </summary>
		public void PlayJump()
		{
			PlayAction(ActionConstants.t_jump);
		}

		/// <summary>
		/// 行走
		/// </summary>
		public void PlayWalk()
		{
			PlayAction(ActionConstants.t_walk);
		}

		/// <summary>
		/// 奔跑
		/// </summary>
		public void PlayRun()
		{
			PlayAction(ActionConstants.t_run);
		}

		/// <summary>
		/// 嘲讽
		/// </summary>
		public void PlayTaunt()
		{
			PlayAction(ActionConstants.t_taunt);
		}

		/// <summary>
		/// 等待
		/// </summary>
		public void PlayIdle()
		{
			PlayAction (ActionConstants.t_idle_01);
		}
	}
}

