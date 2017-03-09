using System;
using UnityEngine;
using Game.Helper;

namespace Model.Base
{
	/// <summary>
	/// 单位动作
	/// </summary>
	public class UnitAction
	{
		public const string attack_01 = "attack_01";
		public const string attack_02 = "attack_02";
		public const string attack_03 = "attack_03";
		public const string walkBattleBackward = "walkBattleBackward";
		public const string walkBattleForward = "walkBattleForward";
		public const string walkBattleLeft = "walkBattleLeft";	
		public const string walkBattleRight = "walkBattleRight";
		public const string defend = "defend";
		public const string die = "die";
		public const string getHit = "getHit";
		public const string idle_01 = "idle_01";
		public const string jump = "jump";
		public const string walk = "walk";
		public const string taunt = "taunt";
		public const string run = "run";

		public static int t_attack_01 = Animator.StringToHash("attack_01");
		public static int t_attack_02 = Animator.StringToHash("attack_02");
		public static int t_attack_03 = Animator.StringToHash("attack_03");
		public static int t_walkBattleBackward = Animator.StringToHash("walkBattleBackward");
		public static int t_walkBattleForward = Animator.StringToHash("walkBattleForward");
		public static int t_walkBattleLeft = Animator.StringToHash("walkBattleLeft");	
		public static int t_walkBattleRight = Animator.StringToHash("walkBattleRight");
		public static int t_defend = Animator.StringToHash("defend");
		public static int t_die = Animator.StringToHash("die");
		public static int t_getHit = Animator.StringToHash("getHit");
		public static int t_idle_01 = Animator.StringToHash("idle_01");
		public static int t_jump = Animator.StringToHash("jump");
		public static int t_walk = Animator.StringToHash("walk");
		public static int t_taunt = Animator.StringToHash("taunt");
		public static int t_run = Animator.StringToHash("run");

		static UnitAction()
		{
			Log.Info ("=====================================");
			Log.Info ("t_attack_01 : " + t_attack_01);
			Log.Info ("t_attack_02 : " + t_attack_02);
			Log.Info ("t_attack_03 : " + t_attack_03);
			Log.Info ("t_walkBattleBackward : " + t_walkBattleBackward);
			Log.Info ("t_walkBattleForward : " + t_walkBattleForward);
			Log.Info ("t_walkBattleLeft : " + t_walkBattleLeft);
			Log.Info ("t_walkBattleRight : " + t_walkBattleRight);
			Log.Info ("t_defend : " + t_defend);
			Log.Info ("t_die : " + t_die);
			Log.Info ("t_getHit : " + t_getHit);
			Log.Info ("t_jump : " + t_jump);
			Log.Info ("t_walk : " + t_walk);
			Log.Info ("t_taunt : " + t_taunt);
			Log.Info ("t_run : " + t_run);
			Log.Info ("idle_01 : " + t_idle_01);
			Log.Info ("=====================================");
		}
	}
}

