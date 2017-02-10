using System;
using System.Collections.Generic;

namespace Controller.Battle
{
	/// <summary>
	/// 战斗广播
	/// </summary>
	public delegate void OnBattleBroadCast();
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
			OnBeginBattle ();
		}

		/// <summary>
		/// 暂停
		/// </summary>
		public void Pause()
		{
			
		}

		/// <summary>
		/// 恢复
		/// </summary>
		public void Resume()
		{
			
		}

		/// <summary>
		/// 更新战斗
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			List<int> removeTeams = new List<int> ();
			foreach (KeyValuePair<int,Team> item in _Teams) {
				if (item.Value.IsDestory) {
					removeTeams.Add (item.Key);
				}
			}

			foreach (int item in removeTeams) {
				_Teams[item].Dispose ();
				_Teams.Remove (item);
			}

			foreach (KeyValuePair<int,Team> item in _Teams) {
				item.Value.Update (dt);
			}
		}
	}
}

