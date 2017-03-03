using UnityEngine;
using System.Collections;
using Model.Base;
using Game.Helper;

namespace Game.Controller
{
	/// <summary>
	/// 单位动作控制
	/// </summary>
	public class UnitActionController : MonoBehaviour
	{
		// Use this for initialization
		void Start ()
		{
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad1, Game.Platform.KeyPhase.Began, PlayStand);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad2, Game.Platform.KeyPhase.Began, PlayWalk);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad3, Game.Platform.KeyPhase.Began, PlayRun);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad4, Game.Platform.KeyPhase.Began, PlayAttack);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad5, Game.Platform.KeyPhase.Began, PlayHurt);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad6, Game.Platform.KeyPhase.Began, PlayDead);

			Animator animation = this.GetComponent<Animator> ();
			int layerCount = animation.layerCount;
			AnimatorClipInfo[] clipInfos = animation.GetCurrentAnimatorClipInfo (0);
			for (int i = 0; i < clipInfos.Length; i++) {
				AnimatorClipInfo clipInfo = clipInfos [i];
				AnimationClip clip = clipInfo.clip;

				AnimationEvent e = new AnimationEvent();
				e.time = clip.length;
				e.functionName = "Test";
				e.stringParameter = clip.name;
				clip.AddEvent (e);
			}
		}

		// Update is called once per frame
		void Update ()
		{
			Animator animation = this.GetComponent<Animator> ();
			AnimatorStateInfo stateInfo = animation.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.IsTag(UnitAction.AnimationTag.Dead)) {
				Log.Info ("normalizedTime : " + stateInfo.normalizedTime);
			}
		}

		void OnDestory()
		{
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad1, Game.Platform.KeyPhase.Began, PlayStand);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad2, Game.Platform.KeyPhase.Began, PlayWalk);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad3, Game.Platform.KeyPhase.Began, PlayRun);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad4, Game.Platform.KeyPhase.Began, PlayAttack);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad5, Game.Platform.KeyPhase.Began, PlayHurt);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad6, Game.Platform.KeyPhase.Began, PlayDead);
		}

		private void Test(string tag)
		{
			Log.Info ("HaHa" + tag);
		}

		/// <summary>
		/// 站立
		/// </summary>
		private void PlayStand()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (UnitAction.TriggerName.Stand);
		}

		/// <summary>
		/// 行走
		/// </summary>
		private void PlayWalk()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (UnitAction.TriggerName.Walk);
		}

		/// <summary>
		/// 跑动
		/// </summary>
		private void PlayRun()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (UnitAction.TriggerName.Run);
		}

		/// <summary>
		/// 攻击
		/// </summary>
		private void PlayAttack()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (UnitAction.TriggerName.Attack);
		}

		/// <summary>
		/// 死亡
		/// </summary>
		private void PlayDead()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (UnitAction.TriggerName.Dead);
		}

		/// <summary>
		/// 受击
		/// </summary>
		private void PlayHurt()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (UnitAction.TriggerName.Hurt);
		}
	}
}
