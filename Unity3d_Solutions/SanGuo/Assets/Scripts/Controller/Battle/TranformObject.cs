using System;
using UnityEngine;
using Game.Action;
using Model.Base;

namespace Controller.Battle
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
		/// <summary>
		/// The action.
		/// </summary>
		private GOAction _Action;
		/// <summary>
		/// 最后一次播放动作的名称
		/// </summary>
		private string _LastActionName;

		/// <summary>
		/// 开始播放动作
		/// </summary>
		public event OnActionBroadcast OnActionStart;
		/// <summary>
		/// 动作播放停止
		/// </summary>
		public event OnActionBroadcast OnActionEnd;

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
					Vector3 vector;
					vector.x = _Transform.localRotation.x;
					vector.y = _Transform.localRotation.y;
					vector.z = _Transform.localRotation.z;
					return vector;
				}
			}
		}

		public TranformObject()
		{
			_Action = new GOAction ();
		}

		/// <summary>
		/// 设置变换对象
		/// </summary>
		/// <param name="target">Transform.</param>
		public void SetTranform(Transform target)
		{
			_Transform = target.transform;
			_Action.Target = target;
		}
		/// <summary>
		/// 移动向量值
		/// </summary>
		/// <param name="position">Vector3.</param>
		public void WalkBy (Vector3 position)
		{
			if (Transform == null) {
				return;
			}

			Transform.Translate (position);
		}
		/// <summary>
		/// 移动到目的地
		/// </summary>
		/// <param name="position">Vector3.</param>
		public void WalkTo (Vector3 position)
		{
			if (Transform == null) {
				return;
			}

			Vector3 vector = position - Position;
			Transform.Translate (vector);
		}
		/// <summary>
		/// 旋转向量值
		/// </summary>
		/// <param name="rotation">Vector3.</param>
		public void RotateBy (Vector3 rotation)
		{
			if (Transform == null) {
				return;
			}

			Transform.Rotate (rotation);
		}
		/// <summary>
		/// 旋转到指定方向
		/// </summary>
		/// <param name="rotation">Vector3.</param>
		public void RotateTo (Vector3 rotation)
		{
			if (Transform == null) {
				return;
			}

			Vector3 vector = rotation - Rotation;
			Transform.Rotate (vector);
		}

		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="name">Name.</param>
		public void PlayAction(string name)
		{
			if (Transform == null) {
				return;
			}
			Animator animation = Transform.GetComponent<Animator> ();
			if (animation) {
				animation.SetTrigger (name);
				OnActionStart (name);
			}
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void RunAction(IAction action) 
		{
			if (action == null) {
				return;
			}

			_Action.RunAction (action);
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Pause()
		{
			_Action.Pause ();
			if (Transform != null) {
				Animator animation = Transform.GetComponent<Animator> ();
				if (animation) {
					animation.speed = 0;
				}
			}
		}

		/// <summary>
		/// 恢复
		/// </summary>
		public void Resume()
		{
			_Action.Resume ();
			if (Transform != null) {
				Animator animation = Transform.GetComponent<Animator> ();
				if (animation) {
					animation.speed = 1;
				}
			}

		}

		/// <summary>
		/// 是否正在播放动作
		/// </summary>
		/// <returns><c>true</c> if this instance is play the specified tag; otherwise, <c>false</c>.</returns>
		/// <param name="tag">Tag.</param>
		public bool IsPlay(string tag) {
			if (Transform == null) {
				return false;
			}
			Animator animation = Transform.GetComponent<Animator> ();
			if (animation == null) {
				return false;
			}

			AnimatorStateInfo stateInfo = animation.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.IsTag(tag)) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// 站立
		/// </summary>
		public void PlayStand()
		{
			PlayAction(UnitAction.TriggerName.Stand);
		}

		/// <summary>
		/// 行走
		/// </summary>
		public void PlayWalk()
		{
			PlayAction(UnitAction.TriggerName.Walk);
		}

		/// <summary>
		/// 跑动
		/// </summary>
		public void PlayRun()
		{
			PlayAction(UnitAction.TriggerName.Run);
		}

		/// <summary>
		/// 攻击
		/// </summary>
		public void PlayAttack()
		{
			PlayAction(UnitAction.TriggerName.Attack);
		}

		/// <summary>
		/// 死亡
		/// </summary>
		public void PlayDead()
		{
			PlayAction(UnitAction.TriggerName.Dead);
		}

		/// <summary>
		/// 受击
		/// </summary>
		public void PlayHurt()
		{
			PlayAction(UnitAction.TriggerName.Hurt);
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (IsPlay (UnitAction.AnimationTag.Dead)) {
				// 检查死亡动作是否播放结束
				Animator animation = Transform.GetComponent<Animator> ();
				AnimatorStateInfo stateInfo = animation.GetCurrentAnimatorStateInfo (0);
				if (stateInfo.normalizedTime >= 1) {
					OnActionEnd (UnitAction.AnimationTag.Dead);
				}
			}
		}
	}
}

