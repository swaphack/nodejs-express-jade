using UnityEngine;
using System.Collections.Generic;
using Game.Helper;

namespace Controller.Battle
{
	/// <summary>
	/// 交通管理，单位寻路行走
	/// </summary>
	public class Traffic
	{
		/// <summary>
		/// 下一步路径
		/// </summary>
		private Dictionary<int, Vector3> _NextPositions;
		/// <summary>
		/// 单位
		/// </summary>
		private Dictionary<int, Unit> _Units;

		public Traffic ()
		{
			_NextPositions = new Dictionary<int, Vector3> ();
			_Units = new Dictionary<int, Unit> ();
		}

		private Vector3 srcPos;
		private float srcRadius;
		private Vector3 srcNextPos;
		private bool bSrcExistsNext;

		private Vector3 destPos;
		private float destRadius;
		private Vector3 destNextPos;
		private bool bDestExistsNext;

		/// <summary>
		/// 是否会碰撞
		/// </summary>
		/// <returns><c>true</c>, if collision was checked, <c>false</c> otherwise.</returns>
		/// <param name="srcID">Source I.</param>
		/// <param name="destID">Destination I.</param>
		private bool CheckCollision(int srcID, int destID) {
			if (!_Units.ContainsKey(srcID)
				|| !_Units.ContainsKey(destID)) {
				return false;
			}

			srcPos = _Units [srcID].MemberTransform.Position;
			srcRadius = _Units [srcID].MemberTransform.CollisionRadius;


			destPos = _Units [destID].MemberTransform.Position;
			destRadius = _Units [destID].MemberTransform.CollisionRadius;

			if (MathHelp.IsIntersect (srcPos, srcRadius, destPos, destRadius)) {
				return true;
			}

			bSrcExistsNext = false;
			if (_NextPositions.ContainsKey (srcID)) {
				srcNextPos = _NextPositions [srcID];
				bSrcExistsNext = true;
			}

			bDestExistsNext = false;
			if (_NextPositions.ContainsKey (destID)) {
				destNextPos = _NextPositions [destID];
				bDestExistsNext = true;
			}

			if (bSrcExistsNext && !bDestExistsNext) {
				if (MathHelp.IsIntersect (srcNextPos, srcRadius, destPos, destRadius)) {
					return true;
				}
			} else if (bDestExistsNext && !bSrcExistsNext) {
				if (MathHelp.IsIntersect (srcPos, srcRadius, destNextPos, destRadius)) {
					return true;
				}
			} else if (bSrcExistsNext && bDestExistsNext) {
				if (MathHelp.IsIntersect (srcNextPos, srcRadius, destNextPos, destRadius)) {
					return true;
				}
			}

			return false;
		}


		private Vector3 position;
		private Vector3 position0;
		private int unitID;

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			_NextPositions.Clear ();
			_Units.Clear ();

			Field field = BattleHelp.Field;
			// 计算路径
			foreach (KeyValuePair<int, Team> item in field.AliveTeams) {
				UnitSheet aliveUnits = item.Value.AliveUnits;
				foreach (KeyValuePair<int, Unit> item2 in aliveUnits.Units) {
					if (!item2.Value.Property.Dead
						&& item2.Value.IsCollider
					    && item2.Value.MemberTransform.Collider != null) {
						if (item2.Value.Walker.Empty) {
							continue;
						}
						_Units [item2.Key] = item2.Value;
						if (item2.Value.Walker.TryGetNextStation (dt, out position)) {
							_NextPositions [item2.Key] = position;
						}
					}
				}
			}

			while (_Units.Count != 0) {
				unitID = 0;
				foreach (KeyValuePair<int, Unit> item in _Units) {
					if (unitID == 0) {
						unitID = item.Key;
					} else {
						if (CheckCollision (unitID, item.Key)) {
							_Units[unitID].StopWalk ();
							//_Units [unitID].UnitBehaviour.Reset ();
							break;
						}
					}
				}

				if (unitID != 0) {
					_Units.Remove (unitID);
				}
			}

			_NextPositions.Clear ();
			_Units.Clear ();
		}
	}
}



