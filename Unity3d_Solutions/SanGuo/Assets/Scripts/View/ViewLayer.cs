using System;
using Game.Layer;
using View.Layer;
using Game.Helper;
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
		}
	}
}

