using UnityEngine;
using System.Collections;

namespace Game.Listener
{
	/// <summary>
	/// 单位动作控制
	/// </summary>
	public class UnitActionListener : MonoBehaviour
	{
		public const string ActionMarkStand = "stand";
		public const string ActionMarkWalk = "walk";
		public const string ActionMarkRun = "run";
		public const string ActionMarkAttack = "attack";
		public const string ActionMarkDead = "die";
		public const string ActionMarkHurt = "hurt";

		// Use this for initialization
		void Start ()
		{
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad1, Game.Platform.KeyPhase.Began, RunStand);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad2, Game.Platform.KeyPhase.Began, RunWalk);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad3, Game.Platform.KeyPhase.Began, RunRun);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad4, Game.Platform.KeyPhase.Began, RunAttack);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad5, Game.Platform.KeyPhase.Began, RunHurt);
			GameInstance.GetInstance().Device.AddKeyHandler(KeyCode.Keypad6, Game.Platform.KeyPhase.Began, RunDead);
		}

		// Update is called once per frame
		void Update ()
		{

		}

		void OnDestory()
		{
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad1, Game.Platform.KeyPhase.Began, RunStand);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad2, Game.Platform.KeyPhase.Began, RunWalk);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad3, Game.Platform.KeyPhase.Began, RunRun);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad4, Game.Platform.KeyPhase.Began, RunAttack);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad5, Game.Platform.KeyPhase.Began, RunHurt);
			GameInstance.GetInstance().Device.RemoveKeyHandler(KeyCode.Keypad6, Game.Platform.KeyPhase.Began, RunDead);
		}

		private void RunStand()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (ActionMarkStand);
		}

		private void RunWalk()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (ActionMarkWalk);
		}

		private void RunRun()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (ActionMarkRun);
		}

		private void RunAttack()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (ActionMarkAttack);
		}

		private void RunDead()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (ActionMarkDead);
		}

		private void RunHurt()
		{
			Animator animator = this.GetComponent<Animator> ();
			animator.SetTrigger (ActionMarkHurt);
		}
	}
}
