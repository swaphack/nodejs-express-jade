using System;
using Model.Base;

namespace Controller.Battle
{
	/// <summary>
	/// 战斗广播
	/// </summary>
	public delegate void OnBattleBroadCast();

	/// <summary>
	/// 队伍广播
	/// </summary>
	public delegate void OnTeamBroadCast(Team Team);

	/// <summary>
	/// 单位广播
	/// </summary>
	public delegate void OnUnitBroadCast(Unit unit);

	/// <summary>
	/// 动作广播
	/// </summary>
	public delegate void OnActionBroadCast(string tag);
	/// <summary>
	/// 单位动作广播
	/// </summary>
	public delegate void OnUnitActionBroadCast(Unit unit, string tag);
}

