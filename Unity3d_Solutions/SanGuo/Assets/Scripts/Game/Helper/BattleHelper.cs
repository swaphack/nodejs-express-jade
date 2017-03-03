using System;
using Game.Module;
using Data;
using Game;
using Controller.Battle;

namespace Game.Helper
{
	public class BattleHelp
	{
		private BattleHelp ()
		{
		}

		/// <summary>
		/// 获取当前战场
		/// </summary>
		/// <value>The field.</value>
		public static Field Field {
			get { 
				if (_BattlePlugin == null) {
					return null;
				}
				return _BattlePlugin.Field;
			}
		}

		/// <summary>
		/// 战斗差将
		/// </summary>
		private static BattlePlugin _BattlePlugin;

		/// <summary>
		/// 开始战斗
		/// </summary>
		/// <param name="data">FieldData.</param>
		public static void StartBattle(FieldData data)
		{
			if (data == null) {
				return;
			}
			EndBattle ();

			_BattlePlugin = new BattlePlugin ();
			_BattlePlugin.SetFieldData (data);
			_BattlePlugin.Init ();
			GameInstance.GetInstance ().AddTempPlugin (_BattlePlugin);
		}

		/// <summary>
		/// 结束战斗
		/// </summary>
		public static void EndBattle()
		{
			if (_BattlePlugin == null) {
				return;
			}

			_BattlePlugin.Dispose ();
			GameInstance.GetInstance ().RemoveTempPlugin (_BattlePlugin);

			_BattlePlugin = null;
		}

		/// <summary>
		/// 开始模拟战斗
		/// </summary>
		public static void StartSimulatorBattle()
		{
			FieldData data = new FieldData ();
			if (!data.Load (XmlFilePath.DataBaseBattleTest)) {
				return;
			}

			StartBattle (data);
		}
	}
}

