using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Battle;
using Game;
using Game.Helper;
using Model.Base;
using Controller.AI.Movement;

namespace Controller.Battle
{
	/// <summary>
	/// 队伍
	/// </summary>
	public class Team : Identifier, IDisposable
	{
		/// <summary>
		/// 布阵信息
		/// </summary>
		private Formation _Formation;
		/// <summary>
		/// 数据读取索引
		/// </summary>
		private int _ReadUnitCursor;
		/// <summary>
		/// 活着的单位
		/// </summary>
		private UnitSheet _AliveUnits;
		/// <summary>
		/// 死亡的单位
		/// </summary>
		private UnitSheet _DeadUnits;
		/// <summary>
		/// 待移除的单位
		/// </summary>
		private List<Unit> _WaitForRemoveUnits;
		/// <summary>
		/// 运动组
		/// </summary>
		private MoveGroup _MoveGroup;
		/// <summary>
		/// 队伍被摧毁
		/// </summary>
		public event OnTeamBroadcast OnDestory;
		/// <summary>
		/// 创建新单位资源
		/// </summary>
		public event OnUnitBroadcast OnUnitCreate;
		/// <summary>
		/// 销毁单位资源
		/// </summary>
		public event OnUnitBroadcast OnUnitDestory;

		/// <summary>
		/// 最近一次攻击的队伍编号
		/// </summary>
		private int _LastHitTeamID;

		/// <summary>
		/// 是否被摧毁
		/// </summary>
		/// <value><c>true</c> if this instance is destory; otherwise, <c>false</c>.</value>
		public bool IsDestory {
			get {
				// 所有单位被摧毁
				return _DeadUnits.Count == _Formation.UnitModels.Count;
			}
		}

		/// <summary>
		/// 是否集结
		/// </summary>
		public bool IsBuildUp = false;

		/// <summary>
		/// 是否加载完毕所有资源
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		public bool IsLoaded {
			get { 
				return _Formation.UnitModels.Count == _AliveUnits.Count + _DeadUnits.Count;
			}
		}

		/// <summary>
		/// 活着的单位
		/// </summary>
		public UnitSheet AliveUnits {
			get { 
				return _AliveUnits;
			}
		}

		/// <summary>
		/// 死亡的单位
		/// </summary>
		public UnitSheet DeadUnits {
			get { 
				return _DeadUnits;
			}
		}

		/// <summary>
		/// 移动组
		/// </summary>
		/// <value>The place.</value>
		public MoveGroup MoveGroup {
			get { 
				return _MoveGroup;
			}
		}

		/// <summary>
		/// 最近一次攻击的队伍编号
		/// </summary>
		public int LastHitTeamID {
			get { 
				return _LastHitTeamID;
			}
			set { 
				_LastHitTeamID = value;
			}
		}

		public Team()
		{
			_AliveUnits = new UnitSheet ();
			_DeadUnits = new UnitSheet ();

			_WaitForRemoveUnits = new List<Unit> ();
			_MoveGroup = new MoveGroup ();
		}

		/// <summary>
		/// 从阵型说明上加载队伍
		/// </summary>
		/// <param name="formation">Formation.</param>
		public void SetFormation (Formation formation)
		{
			if (formation == null) {
				return;
			}

			_Formation = formation;
			_ReadUnitCursor = 0;
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Update(float dt)
		{
			// 更新单位
			UpdateUnits (dt);
		}

		/// <summary>
		/// 更新移动组
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void UpdateMoveGroup(float dt)
		{
			_MoveGroup.Update (dt);
		}

		/// <summary>
		/// 加载单位
		/// </summary>
		public bool LoadUnits()
		{
			if (_Formation == null || _Formation.UnitModels.Count == 0) {
				return false;
			}

			// 全部加载完毕
			if (_Formation.UnitModels.Count == _AliveUnits.Count + _DeadUnits.Count) {
				return false;
			}

			// 还在加载
			if (_ReadUnitCursor >= _Formation.UnitModels.Count) {
				return true;
			}

			UnitModel paper = _Formation.UnitModels [_ReadUnitCursor];
			if (string.IsNullOrEmpty (paper.AssetBundlePath)) {
				FileDataHelp.CreatePrefabFromAsset (paper.FileName, (GameObject gameObj)=>{
					if (gameObj != null) {
						OnCreateUnit(gameObj, paper);
					}
				});					
			} else {
				FileDataHelp.CreatePrefabFromAssetBundle (paper.AssetBundlePath, paper.FileName, (GameObject gameObj)=>{
					if (gameObj != null) {
						OnCreateUnit(gameObj, paper);
					}
				});
			}
			_ReadUnitCursor++;

			return true;
		}

		/// <summary>
		/// 更新单位
		/// </summary>
		/// <returns><c>true</c>, if units was updated, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		protected void UpdateUnits(float dt)
		{
			_AliveUnits.Update (dt);
			_DeadUnits.Update (dt);
		}

		/// <summary>
		/// 更新活着的单位
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void UpdateAliveUnits(float dt)
		{
			_AliveUnits.Update (dt);
		}

		/// <summary>
		/// 更新死亡单位
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void UpdateDeadUnits(float dt)
		{
			_DeadUnits.Update (dt);
		}

		/// <summary>
		/// 创建单位回调
		/// </summary>
		/// <param name="gameObj">Game object.</param>
		/// <param name="paper">UnitModel.</param>
		private void OnCreateUnit(GameObject gameObj, UnitModel paper)
		{
			if (gameObj == null || paper == null) {
				return;
			}

			GameObject newGameObj = Utility.Clone (gameObj);
			if (newGameObj == null) {
				return;
			}

			// 设置控件属性
			newGameObj.name = paper.Name;
			newGameObj.transform.position = paper.Position;
			newGameObj.transform.localScale = paper.Scale;
			newGameObj.transform.Rotate (paper.Rotation);
			Collider collider = newGameObj.GetComponent<BoxCollider> ();
			if (collider == null) {
				newGameObj.AddComponent<BoxCollider> ();
			} 
			newGameObj.GetComponent<BoxCollider> ().size = paper.Volume;
			newGameObj.GetComponent<BoxCollider> ().center = paper.Center;

			// 创建单位
			Unit unit = new Unit ();
			unit.ID = paper.ID;
			unit.Name = paper.Name;
			unit.TeamID = ID;
			unit.OnDead += OnDisposeUnit;
			unit.OnUnitActionEnd += OnEndUnitAction;
			unit.SetObject (newGameObj);
			unit.SetModel (paper);

			_AliveUnits.AddUnit (unit);
			_MoveGroup.AddUnit (unit.MemeberMovement);

			if (OnUnitCreate != null) {
				OnUnitCreate (unit);
			}
		}

		/// <summary>
		/// 移除单位
		/// </summary>
		/// <param name="unit">Unit.</param>
		private void OnDisposeUnit(Unit unit)
		{
			if (unit == null) {
				return;
			}

			_WaitForRemoveUnits.Add (unit);
		}

		/// <summary>
		/// 处理待移除单位事件
		/// </summary>
		public void HandWaitForRemoveUnits()
		{
			if (_WaitForRemoveUnits.Count == 0) {
				return;
			}
			int count = _WaitForRemoveUnits.Count;
			for (int i = 0; i < count; i++) {
				_AliveUnits.RemoveUnit (_WaitForRemoveUnits [i].ID);
				_DeadUnits.AddUnit (_WaitForRemoveUnits [i]);
				_MoveGroup.RemoveUnit (_WaitForRemoveUnits [i].ID);
			}

			_WaitForRemoveUnits.Clear ();
			if (_AliveUnits.Count == 0) {
				this.Dispose ();
			}
		}

		/// <summary>
		/// 单位动作播放结束
		/// </summary>
		/// <param name="unit">Unit.</param>
		/// <param name="tag">Tag.</param>
		private void OnEndUnitAction(Unit unit, int tag)
		{
			if (unit == null) {
				return;
			}

			if (tag == ActionConstants.t_die) {
				if (OnUnitDestory != null) {
					OnUnitDestory (unit);
				}
			}
		}

		/// <summary>
		/// 销毁队伍
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Controller.Battle.Team"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Controller.Battle.Team"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="Controller.Battle.Team"/> so the garbage
		/// collector can reclaim the memory that the <see cref="Controller.Battle.Team"/> was occupying.</remarks>
		public void Dispose()
		{
			OnDestory (this);
		}
	}
}

