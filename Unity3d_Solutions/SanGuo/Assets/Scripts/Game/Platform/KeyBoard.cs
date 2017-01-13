using System;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Notify;

namespace Game
{
	/// <summary>
	/// 按键状态
	/// </summary>
	public enum KeyPhase
	{
		/// <summary>
		/// 开始点击
		/// </summary>
		Began,
		/// <summary>
		/// 按住不放
		/// </summary>
		Keeped,
		/// <summary>
		/// 放开
		/// </summary>
		Ended,
	}
	/// <summary>
	/// 按键
	/// </summary>
	public struct KeyPress
	{
		/// <summary>
		/// 按键
		/// </summary>
		public KeyCode Key;
		/// <summary>
		/// 状态
		/// </summary>
		public KeyPhase Status;

		public static bool operator==(KeyPress item1, KeyPress item2)
		{
			return item1.Key == item2.Key && item1.Status == item2.Status;
		}

		public static bool operator!=(KeyPress item1, KeyPress item2)
		{
			return item1.Key != item2.Key || item1.Status != item2.Status;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	/// <summary>
	/// 键盘
	/// </summary>
	public class KeyBoard : Notifition<KeyPress>
	{
		public KeyBoard ()
		{
		}

		/// <summary>
		/// 点击按钮或持续点击
		/// </summary>
		public void Update()
		{
			KeyPress keyPress;
			bool bTrigger = false;
			foreach (KeyValuePair<KeyPress, NotifyEvent> item in _NotifyListeners) {
				
				keyPress = item.Key;
				bTrigger = false;

				if (keyPress.Status == KeyPhase.Began && Input.GetKeyDown (keyPress.Key) == true)
					bTrigger = true;
				if (!bTrigger && keyPress.Status == KeyPhase.Keeped && Input.GetKey (keyPress.Key) == true)
					bTrigger = true;
				if (bTrigger)
					Notify(item.Key);
			}
		}
	}
}

