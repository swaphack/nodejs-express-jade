using System;
using Game;

namespace Control
{
	public class MainPlayer : Player
	{
		/// <summary>
		/// 是否初始化
		/// </summary>
		private bool _bInit;

		public MainPlayer ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			if (_bInit == false) {
				Resource.Food = UserDefault.GetInstance ().GetInteger ("Food");
				Resource.Wood = UserDefault.GetInstance ().GetInteger ("Wood");
				Resource.Iron = UserDefault.GetInstance ().GetInteger ("Iron");
				_bInit = true;
			}
		}
	}
}

