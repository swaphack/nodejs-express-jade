using System;
using System.Collections.Generic;

namespace Control.Battle
{
	/// <summary>
	/// 战斗广播
	/// </summary>
	public delegate void OnBattleBroadCast(float dt);
	/// <summary>
	/// 战场
	/// </summary>
	public class Field
	{
		/// <summary>
		/// 队伍
		/// </summary>
		private Dictionary<int, Team> _Teams;
		/// <summary>
		/// 进行时间
		/// </summary>
		private int _Time;

		/// <summary>
		/// 开始战斗
		/// </summary>
		public event OnBattleBroadCast OnBeginBattle;
		/// <summary>
		/// 结束战斗
		/// </summary>
		public event OnBattleBroadCast OnEndBattle;
		/// <summary>
		/// 播放战斗
		/// </summary>
		public event OnBattleBroadCast OnRunBattle;

		public Field ()
		{
			_Teams = new Dictionary<int, Team> ();
		}

		/// <summary>
		/// 添加队伍
		/// </summary>
		/// <param name="team">Team.</param>
		public void AddTeam(Team team)
		{
			if (team == null) {
				return;
			}

			_Teams.Add (team.ID, team);
		}

		/// <summary>
		/// 开始战斗
		/// </summary>
		public void Start()
		{
			
		}

		/// <summary>
		/// 更新战斗
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			
		}
	}
}

