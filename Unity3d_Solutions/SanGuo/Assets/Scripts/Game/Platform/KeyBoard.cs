using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 键盘
	/// </summary>
	public class KeyBoard : Notifition<KeyCode>
	{
		public KeyBoard ()
		{
		}

		public void Update()
		{
			foreach (KeyValuePair<KeyCode, NotifyListener> item in _NotifyListeners) {
				if (Input.GetKeyDown (item.Key) == true) {
					Notify(item.Key);
				}
			}
		}
	}
}

