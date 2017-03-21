using UnityEngine;
using System.Collections.Generic;
using Game.Helper;
using Model.Base;

namespace Controller.Battle.Terrain
{
	/// <summary>
	/// 交通管理，单位寻路行走
	/// </summary>
	public class Traffic
	{
		private Field _Field;

		public Traffic ()
		{
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (_Field == null) {
				_Field = BattleHelp.Field;
			}
			
			foreach (KeyValuePair<int, Team> item in _Field.AliveTeams) {
				item.Value.MoveGroup.CalGroupInfo ();
				if (item.Value.LastHitTeamID == 0) {
					item.Value.LastHitTeamID = FindNearTeamID (item.Key);
					item.Value.MoveGroup.Concentrate ();
				} else {
					if (_Field.GetDeadTeam (item.Value.LastHitTeamID) != null) {
						item.Value.LastHitTeamID = 0;
					}
				}

				item.Value.UpdateMoveGroup (dt);
			}

			PreCheckPath ();
		}

		/// <summary>
		/// 查找最近队伍编号
		/// </summary>
		/// <returns>The near team I.</returns>
		/// <param name="teamID">Team I.</param>
		private int FindNearTeamID(int teamID)
		{
			float distance = 0;
			float targetDistance = 0;
			int targetID = 0;
			foreach (KeyValuePair<int, Team> item in _Field.AliveTeams) {
				if (item.Key != teamID) 
				{
					distance = Vector3.Distance (_Field.GetTeam (teamID).MoveGroup.Centriod, item.Value.MoveGroup.Centriod);
					if (targetDistance == 0) {
						targetDistance = distance;
						targetID = item.Key;
					} else if (targetDistance > distance) {
						targetDistance = distance;
						targetID = item.Key;
					}
				}
			}

			return targetID;
		}

		/// <summary>
		/// 检查路线
		/// </summary>
		private void PreCheckPath()
		{
			// 计算路径
			foreach (KeyValuePair<int, Team> item in _Field.AliveTeams) {
				UnitSheet aliveUnits = item.Value.AliveUnits;
				foreach (KeyValuePair<int, Unit> item2 in aliveUnits.Units) {
					if (item2.Value.CanCollision) {
						if (item2.Value.MemeberMovement.Traveler.Empty) {
							continue;
						}

						CheckUnitPath (item2.Value);
					}
				}
			}
		}

		/// <summary>
		/// 检查单位路线
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void CheckUnitPath(Unit src)
		{
			if (src == null) {
				return;
			}
			// 计算路径
			foreach (KeyValuePair<int, Team> item in _Field.AliveTeams) {
				UnitSheet aliveUnits = item.Value.AliveUnits;
				foreach (KeyValuePair<int, Unit> item2 in aliveUnits.Units) {
					if (src.ID != item2.Key && item2.Value.CanCollision) {
						if (src.MemeberMovement.IsTargetOnPath (item2.Value.MemberTransform.Position, item2.Value.MemberTransform.CollisionRadius)
							|| src.MemeberMovement.IsCollideWith(item2.Value.MemeberMovement)
						) {
							//src.MemeberMovement.ResetWaitForFindWayTime ();
							src.MemeberMovement.RefindWay();
							break;
						}
					}
				}
			}
		}
	}
}



