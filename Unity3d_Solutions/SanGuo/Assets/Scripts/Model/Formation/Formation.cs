using UnityEngine;
using System.Collections.Generic;
using Model.Paper;
using Game;

namespace Model.Formation
{	
	/// <summary>
	/// 阵型
	/// </summary>
	public class Formation
	{
		/// <summary>
		/// 单位
		/// </summary>
		private List<Unit> _Units;
		/// <summary>
		/// 布阵信息
		/// </summary>
		private FormationPaper _FormationPaper;
		/// <summary>
		/// 数据读取索引
		/// </summary>
		private int _ReadPaperCursor;
		/// <summary>
		/// 单位个数
		/// </summary>
		/// <value>The count.</value>
		public int Count { 
			get {
				return _Units.Count;	
			} 
		}

		public Formation()
		{
			_Units = new List<Unit> ();
		}

		/// <summary>
		/// 从阵型说明上加载队伍
		/// </summary>
		/// <param name="paper">Paper.</param>
		public void setPaper (FormationPaper paper)
		{
			if (paper == null) {
				return;
			}

			_FormationPaper = paper;
			_ReadPaperCursor = 0;
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
			if (_FormationPaper == null || _FormationPaper.UnitPapers.Count == 0) {
				return;
			}
			if (_ReadPaperCursor >= _FormationPaper.UnitPapers.Count) {
				return;
			}

			UnitPaper paper = _FormationPaper.UnitPapers [_ReadPaperCursor];
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
			_ReadPaperCursor++;
		}

		/// <summary>
		/// 更新单位
		/// </summary>
		/// <returns><c>true</c>, if units was updated, <c>false</c> otherwise.</returns>
		/// <param name="dt">Dt.</param>
		protected void UpdateUnits(float dt) {
			foreach (Unit unit in _Units) {
				unit.Update(dt);
			}
		}

		/// <summary>
		/// 创建单位回调
		/// </summary>
		/// <param name="gameObj">Game object.</param>
		/// <param name="paper">UnitPaper.</param>
		private void OnCreateGameObject(GameObject gameObj, UnitPaper paper)
		{
			if (gameObj == null || paper == null) {
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
			unit.SetObject (gameObj);
			_Units.Add (unit);
		}
	}
}


