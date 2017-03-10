using System;
using Game.Layer;
using View.Layer;
using Game.Helper;
using Game;
using UnityEngine;

namespace View
{
	/// <summary>
	/// 视图层
	/// </summary>
	public class ViewLayer
	{
		public ViewLayer ()
		{
			//UI.GetInstance().Show<UIHomeLayer>();
			BattleHelp.StartSimulatorBattle();

			GameInstance.GetInstance ().Device.AddEscKeyHandler (() => {
				Application.Quit();
			});

		}
	}
}

