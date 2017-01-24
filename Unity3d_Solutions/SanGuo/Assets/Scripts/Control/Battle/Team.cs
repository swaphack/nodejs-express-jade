using System;
using UnityEngine;
using Model.Battle;
using Game;

namespace Control.Battle
{
	/// <summary>
	/// 队伍
	/// </summary>
	public class Team : Identifier
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
		/// 是否被摧毁
		/// </summary>
		/// <value><c>true</c> if this instance is destory; otherwise, <c>false</c>.</value>
		public bool IsDestory {
			get {
				return _AliveUnits.Count == 0;
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
		private UnitSheet DeadUnits {
			get { 
				return _DeadUnits;
			}
		}

		public Team()
		{
			_AliveUnits = new UnitSheet ();
			_DeadUnits = new UnitSheet ();
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
			// 加载单位
			LoadUnits ();
			// 更新单位
			UpdateUnits (dt);
		}

		/// <summary>
		/// 加载单位
		/// </summary>
		protected void LoadUnits() {
			if (_Formation == null || _Formation.UnitModels.Count == 0) {
				return;
			}
			if (_ReadUnitCursor >= _Formation.UnitModels.Count) {
				return;
			}

			UnitModel paper = _Formation.UnitModels [_ReadUnitCursor];
			if (string.IsNullOrEmpty (paper.AssetBundlePath)) {
				FileDataHelp.CreatePrefabFromAsset (paper.FileName, (GameObject gameObj)=>{
					if (gameObj != null) {
						OnCreateGameObject(gameObj, paper);
					}
				});					
			} else {
				FileDataHelp.CreatePrefabFromAssetBundle (paper.AssetBundlePath, paper.FileName, (GameObject gameObj)=>{
					if (gameObj != null) {
						OnCreateGameObject(gameObj, paper);
					}
				});
			}
			_ReadUnitCursor++;
		}

		/// <summary>
		/// 更新单位
		/// </summary>
		/// <returns><c>true</c>, if units was updated, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		protected void UpdateUnits(float dt) {
			_AliveUnits.Update (dt);
		}

		/// <summary>
		/// 创建单位回调
		/// </summary>
		/// <param name="gameObj">Game object.</param>
		/// <param name="paper">UnitModel.</param>
		private void OnCreateGameObject(GameObject gameObj, UnitModel paper)
		{
			if (gameObj == null || paper == null) {
				return;
			}

			GameObject newGameObj = GameObject.Instantiate<GameObject> (gameObj);
			if (newGameObj == null) {
				return;
			}

			gameObj.transform.localPosition = paper.Position;
			gameObj.transform.localScale = paper.Scale;
			gameObj.transform.Rotate (paper.Rotation);
			Collider collider = gameObj.GetComponent<Collider> ();
			if (collider) {
				Bounds bounds = collider.bounds;
				bounds.size = paper.Volume;
			}

			Unit unit = new Unit ();
			unit.SetObject (newGameObj);
			unit.SetModel (paper);
			_AliveUnits.AddUnit (unit);
		}
	}
}

