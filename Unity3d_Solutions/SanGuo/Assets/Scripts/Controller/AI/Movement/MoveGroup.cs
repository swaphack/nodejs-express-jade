using System.Collections.Generic;
using UnityEngine;
using Game.Helper;
using Foundation.Algorithm;

namespace Controller.AI.Movement
{
	/// <summary>
	/// 布阵状态
	/// </summary>
	public enum FormationState
	{
		/// <summary>
		/// 未在状态中
		/// </summary>
		Broken,
		/// <summary>
		/// 调整队伍中
		/// </summary>
		Forming,
		/// <summary>
		/// 调整完毕
		/// </summary>
		Formed,
	}

	/// <summary>
	/// 碰撞组，用于处理同组内的碰撞检测
	/// </summary>
	public class MoveGroup
	{
		/// <summary>
		/// 单位
		/// </summary>
		private Dictionary<int, IMoveUnit> _Units;
		/// <summary>
		/// 目标 {idx, id}
		/// </summary>
		private int[] _Destinations;
		/// <summary>
		/// 指挥官编号
		/// </summary>
		private int _CommanderID;
		/// <summary>
		/// 中心点坐标
		/// </summary>
		private Vector3 _Centriod;
		/// <summary>
		/// 阵型
		/// </summary>
		private Place _Place;
		/// <summary>
		/// 小组移动速度
		/// </summary>
		/// <value>The group speed.</value>
		private float _GroupSpeed;
		/// <summary>
		/// 布阵状态
		/// </summary>
		private FormationState _FormationState;

		/// <summary>
		/// 布阵状态
		/// </summary>
		public FormationState State {
			get { 
				return _FormationState;
			}
		}

		/// <summary>
		/// 单位数
		/// </summary>
		/// <value>The unit count.</value>
		public int UnitCount {
			get { 
				return _Units.Count;
			}
		}

		/// <summary>
		/// 指挥官编号
		/// </summary>
		/// <value>The commander I.</value>
		public int CommanderID {
			get { 
				return _CommanderID;
			}
			set { 
				_CommanderID = value;

			}
		}

		/// <summary>
		/// 中心点
		/// </summary>
		/// <value>The centriod.</value>
		public Vector3 Centriod {
			get { 
				return _Centriod;
			}
		}

		public MoveGroup ()
		{
			_Units = new Dictionary<int, IMoveUnit> ();
			_Place = new Place ();
		}

		/// <summary>
		/// 添加碰撞体
		/// </summary>
		/// <param name="unit">unit.</param>
		public void AddUnit(IMoveUnit unit)
		{
			if (unit == null) {
				return;
			}

			if (_Units.ContainsKey (unit.ID)) {
				return;
			}

			_Units [unit.ID] = unit;
		}

		/// <summary>
		///  移除碰撞体
		/// </summary>
		/// <param name="id">Identifier.</param>
		public void RemoveUnit(int id) 
		{
			_Units.Remove (id);
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void ClearUnits()
		{
			_Units.Clear ();
		}

		/// <summary>
		/// 设置阵型单位信息
		/// </summary>
		/// <param name="positions">Positions.</param>
		public void SetPlace(Vector3[] positions)
		{
			if (_Place == null || positions == null || positions.Length == 0) {
				return;
			}

			_Place.SetInfos(positions);

			_Destinations = new int[positions.Length];
		}

		/// <summary>
		/// 移动到目标
		/// </summary>
		/// <param name="destination">Destination.</param>
		public void MoveTo(Vector3 destination)
		{
			// 集结，设置移动位置
			int count = _Units.Count;
			foreach(KeyValuePair<int, IMoveUnit> item in _Units) {
				item.Value.GroupSpeed = _GroupSpeed;
				item.Value.IsIndividual = false;
				item.Value.MoveTo (item.Value.Position + destination - Centriod, false);
			}

			_FormationState = FormationState.Formed;
		}

		/// <summary>
		/// 计算彭碰撞组信息
		/// </summary>
		public void CalGroupInfo()
		{
			_GroupSpeed = 0;
			_Centriod = Vector3.zero;
			int count = _Units.Count;
			if (count == 0) {
				return;
			}

			foreach (KeyValuePair<int, IMoveUnit> item in _Units) {
				_GroupSpeed += item.Value.MoveSpeed;
				_Centriod += item.Value.Position;
			}

			_GroupSpeed /= count;
			_Centriod /= count;
		}

		/// <summary>
		/// 集结
		/// </summary>
		public void Concentrate()
		{
			Concentrate (_Centriod);
		}

		/// <summary>
		/// 集结到指定目标点
		/// </summary>
		/// <param name="destination">Destination.</param>
		public void Concentrate(Vector3 destination)
		{
			_FormationState = FormationState.Broken;
			for (int i = 0; i < _Destinations.Length; i++) 
			{ 
				_Destinations[i] = 0; 
			}

			CalGroupInfo ();
			_Place.CalConcentrate (destination, destination - _Centriod);

			int count = _Units.Count;
			if (count == 0) {
				return;
			}

			List<int> ids = new List<int> ();
			foreach (KeyValuePair<int, IMoveUnit> item in _Units) {
				ids.Add (item.Key);
			}

			float[,] distances = new float[count, count];

			// 计算最近坐标位置
			for (int i = 0; i < count; i++) { // 格子
				for (int j = 0; j < count; j++) { // 单位
					distances [i, j] = Vector3.Distance (_Place.GetPosition (j), _Units [ids [i]].Position);
				}
			}

			// 路线
			Dictionary<int, int> filters = Matrix.FilterMinPairs (distances);
			if (filters == null || filters.Count != count) {
				return;
			}

			for (int i = 0; i < count; i++) {
				_Destinations [i] = ids[filters [i]];
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			foreach (KeyValuePair<int, IMoveUnit> item in _Units) {
				item.Value.Update (dt);
			}

			if (_FormationState == FormationState.Formed) { // 集结完毕
				return;
			}

			if (_FormationState == FormationState.Broken) { // 队伍处于游离状态
				int count = _Units.Count;
				for (int i = 0; i < count; i++) {
					int unitID = _Destinations [i];
					if (_Units.ContainsKey (unitID)) {
						_Units [unitID].GroupSpeed = _GroupSpeed;
						_Units [unitID].MoveTo (_Place.GetPosition (i), false);
					}
				}
				_FormationState = FormationState.Forming;
			} else if (_FormationState == FormationState.Forming) { // 正在集结队伍
				bool bFinishForm = true;
				foreach (KeyValuePair<int, IMoveUnit> item in _Units) {
					if (item.Value.State != UnitState.Formed) {
						bFinishForm = false;
					}
				}

				if (bFinishForm) {
					_FormationState = FormationState.Formed;
					_CommanderID = _Destinations [0];
				}
			}
		}
	}
}


