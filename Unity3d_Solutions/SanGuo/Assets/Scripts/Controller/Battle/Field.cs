using System;
using System.Collections.Generic;
using Model.Battle;

namespace Controller.Battle
{
	/// <summary>
	/// 战场
	/// </summary>
	public class Field : IDisposable
	{
		/// <summary>
		/// 存活的队伍
		/// </summary>
		private Dictionary<int, Team> _AliveTeams;
		/// <summary>
		/// 死亡的队伍
		/// </summary>
		private Dictionary<int, Team> _DeadTeams;
		/// <summary>
		/// 进行时间
		/// </summary>
		private int _Time;
		/// <summary>
		/// 是否正在运行中
		/// </summary>
		private bool _Running;
		/// <summary>
		/// 地图加载管理
		/// </summary>
		private MapLoader _MapLoader;
		/// <summary>
		/// 地图
		/// </summary>
		private Map _Map;

		/// <summary>
		/// 存活的队伍
		/// </summary>
		public Dictionary<int, Team> AliveTeams {
			get { 
				return _AliveTeams;
			}
		}

		/// <summary>
		/// 死亡的队伍
		/// </summary>
		public Dictionary<int, Team> DeadTeams {
			get { 
				return _DeadTeams;
			}
		}

		/// <summary>
		/// 地图加载
		/// </summary>
		/// <value>The map.</value>
		public MapLoader MapLoader {
			get { 
				return _MapLoader;
			}
		}

		/// <summary>
		/// 地图
		/// </summary>
		/// <value>The map.</value>
		public Map Map {
			get { 
				return _Map;
			}
		}

		/// <summary>
		/// 开始战斗
		/// </summary>
		//public event OnBattleBroadcast OnBeginBattle;
		/// <summary>
		/// 结束战斗
		/// </summary>
		//public event OnBattleBroadcast OnEndBattle;

		public Field ()
		{
			_AliveTeams = new Dictionary<int, Team> ();
			_DeadTeams = new Dictionary<int, Team> ();
			_MapLoader = new MapLoader ();
			_Map = new Map ();
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

			_AliveTeams.Add (team.ID, team);

			team.OnDestory += OnTeamDestory;
			team.OnUnitCreate += OnCreateUnit;
			team.OnUnitDestory += OnDestoryUnit;
		}

		/// <summary>
		/// 查找队伍
		/// </summary>
		/// <returns>The team.</returns>
		/// <param name="teamID">Team I.</param>
		public Team GetTeam(int teamID)
		{
			if (_AliveTeams.ContainsKey (teamID)) {
				return _AliveTeams [teamID];
			}

			if (_DeadTeams.ContainsKey (teamID)) {
				return _DeadTeams [teamID];
			}

			return null;
		}

		/// <summary>
		/// 获取其他非存活的队伍
		/// </summary>
		/// <returns>The other alive team.</returns>
		/// <param name="teamID">Team I.</param>
		public List<Team> GetOtherAliveTeam(int teamID)
		{
			List<Team> teams = new List<Team> ();
			foreach (KeyValuePair<int,Team> item in _AliveTeams) {
				if (item.Key != teamID) {
					teams.Add (item.Value);
				}
			}

			return teams;
		}

		/// <summary>
		/// 销毁队伍
		/// </summary>
		/// <param name="team">Team.</param>
		private void OnTeamDestory(Team team)
		{
			if (team == null) {
				return;
			}

			_AliveTeams.Remove (team.ID);
			_DeadTeams.Add (team.ID, team);

			if (_AliveTeams.Count == 1) {
				//OnEndBattle ();
			}
		}

		/// <summary>
		/// 创建单位
		/// </summary>
		/// <param name="team">Unit.</param>
		private void OnCreateUnit(Unit unit)
		{
			if (unit == null || _Map == null) {
				return;
			}

			_Map.AddTransform (unit.TranformObject.Transform);
		}

		/// <summary>
		/// 销毁单位
		/// </summary>
		/// <param name="unit">Unit.</param>
		private void OnDestoryUnit(Unit unit)
		{
			if (unit == null) {
				return;
			}

			_Map.RemoveTransform (unit.TranformObject.Transform);
			unit.TranformObject.SetTranform (null);
		}

		/// <summary>
		/// 开始战斗
		/// </summary>
		public void Start()
		{
			_Running = true;

			//OnBeginBattle ();
		}

		/// <summary>
		/// 暂停
		/// </summary>
		public void Pause()
		{
			_Running = false;
		}

		/// <summary>
		/// 恢复
		/// </summary>
		public void Resume()
		{
			_Running = true;
		}

		/// <summary>
		/// 更新战斗
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (!_Running) {
				return;
			}

			if (_Map == null) {
				return;
			}

			// 正在加载资源
			if (_MapLoader.LoadAssetBundle ()) {
				return;
			}

			// 正在加载单位
			bool isLoading = false;
			foreach (KeyValuePair<int,Team> item in _AliveTeams) {
				if (!isLoading) {
					isLoading = item.Value.LoadUnits ();
				}
			}

			if (isLoading) {
				return;
			}

			foreach (KeyValuePair<int,Team> item in _DeadTeams) {
				item.Value.Update (dt);
			}

			foreach (KeyValuePair<int,Team> item in _AliveTeams) {
				item.Value.Update (dt);
			}
			//*/
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Controller.Battle.Field"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Controller.Battle.Field"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Controller.Battle.Field"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="Controller.Battle.Field"/> so the garbage
		/// collector can reclaim the memory that the <see cref="Controller.Battle.Field"/> was occupying.</remarks>
		public void Dispose()
		{
			_AliveTeams.Clear ();
			_DeadTeams.Clear ();
			_MapLoader.Dispose ();
			_Map.Dispose ();
		}
	}
}

