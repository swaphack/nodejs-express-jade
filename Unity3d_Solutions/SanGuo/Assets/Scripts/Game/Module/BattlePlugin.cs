using System;
using Foundation.Plugin;
using Controller.Battle;
using Data;
using System.Collections.Generic;
using Model.Battle;
using Model.Base;
using UnityEngine;
using Game.Helper;
using Model.Skill;
using Data.Battle;

namespace Game.Module
{
	/// <summary>
	/// 战斗插件
	/// </summary>
	public class BattlePlugin : IPlugin
	{
		/// <summary>
		/// 战场
		/// </summary>
		private Field _Field;
		/// <summary>
		/// 战场数据
		/// </summary>
		private FieldData _FieldData;

		/// <summary>
		/// 战场
		/// </summary>
		/// <value>The field.</value>
		public Field Field {
			get { 
				return _Field;
			}
		}

		public BattlePlugin ()
		{
			_Field = new Field ();
		}

		/// <summary>
		/// 设置战场数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void SetFieldData(FieldData data)
		{
			_FieldData = data;
		}

		/// <summary>
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.Battle;
			} 
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			if (_FieldData == null) {
				return;
			}

			// 设置地图
			GameObject gameObject = GameObject.Find ("Battle");
			if (gameObject != null) {
				_Field.Map.Root = gameObject.transform;
			} else {
				Log.Warning ("Not Exist Battle Node, Name : 'Battle'");
				return;
			}

			HashSet<String> assetBundles = new HashSet<string> ();

			// 设置队伍
			foreach (KeyValuePair<int, FieldData.TeamItem> item in _FieldData.TeamItems) {
				Formation formation = new Formation ();
				int unitCount = item.Value.UnitIDs.Count;
				for (int i = 0; i < unitCount; i++) {
					int unitID = item.Value.UnitIDs [i];
					FieldData.UnitItem unitItem = _FieldData.GetUnitItem (unitID);
					if (unitItem == null) {
						continue;
					}
					FieldData.ResourceItem resItem = _FieldData.GetResourceItem (unitItem.ResourceID);
					FieldData.PropertyItem propItem = _FieldData.GetPropertyItem (unitItem.PropertyID);
					FieldData.TransformItem tranItem = _FieldData.GetTransformItem (unitItem.TransformID);
					FieldData.SkillItem skillItem = _FieldData.GetSkillItem (unitItem.SkillID);

					UnitModel unitModel = new UnitModel ();
					unitModel.ID = unitID; 
					unitModel.Name = unitItem.Name;

					if (resItem != null) {
						unitModel.AssetBundlePath = resItem.AssetBundlePath;
						unitModel.FileName = resItem.Name;

						assetBundles.Add (resItem.AssetBundlePath);
					}
					if (tranItem != null) {
						unitModel.Position = tranItem.Position;
						unitModel.Rotation = tranItem.Rotation;
						unitModel.Scale = tranItem.Scale;
						unitModel.Volume = tranItem.Volume;
						unitModel.Center = tranItem.Center;
					}

					if (propItem != null) {
						foreach(KeyValuePair<PropertyType, float>  value in propItem.Values){
							unitModel.Attributes.Add (value.Key, value.Value);
						}
					}

					if (skillItem != null) {
						SkillModel skillModel = new SkillModel ();
						skillModel.ID = unitItem.SkillID;
						skillModel.CoolDown = skillItem.CoolDown;
						skillModel.CostMana = skillItem.CostMana;
						skillModel.Radius = skillItem.Radius;
						skillModel.TargetType = skillItem.TargetType;

						unitModel.Skills.Add (skillModel);
					}

					formation.AddUnit (unitModel);
				}
				Team team = new Team ();
				team.ID = item.Key;
				team.SetFormation (formation);
				_Field.AddTeam (team);
			}

			_Field.Map.SetSize (_FieldData.MapInfo.Width, _FieldData.MapInfo.Height);

			foreach (string item in assetBundles) {
				_Field.MapLoader.AddAssetBundle (item);
			}

			// 开始战斗
			_Field.Start ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			_Field.Update (dt);
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_Field.Dispose ();
		}
	}
}

