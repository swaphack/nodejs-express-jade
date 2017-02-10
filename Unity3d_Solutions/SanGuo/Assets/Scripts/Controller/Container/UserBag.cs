using System;
using System.Collections.Generic;
using Model.Item;

namespace Controller.Container
{
	/// <summary>
	/// 背包
	/// </summary>
	public class UserBag
	{
		/// <summary>
		/// 物品信息
		/// </summary>
		private Dictionary<int, int> _Items;
		
		public UserBag ()
		{
			_Items = new Dictionary<int, int> ();
		}

		/// <summary>
		/// 添加物品
		/// </summary>
		/// <param name="itemID">Item I.</param>
		/// <param name="count">Count.</param>
		public void AddItem(int itemID, int count)
		{
			if (_Items.ContainsKey (itemID)) {
				_Items [itemID] += count;
			} else {
				_Items [itemID] = count;
			}
		}

		/// <summary>
		/// 移除物品
		/// </summary>
		/// <param name="itemID">Item I.</param>
		/// <param name="count">Count.</param>
		public void RemoveItem(int itemID, int count)
		{
			if (!_Items.ContainsKey (itemID)) {
				return;
			}

			_Items [itemID] -= count;
			if (_Items [itemID] <= 0) {
				_Items.Remove (itemID);
			}
		}

		/// <summary>
		/// 获取物品数量
		/// </summary>
		/// <returns>The item count.</returns>
		/// <param name="itemID">Item I.</param>
		public int GetItemCount(int itemID)
		{
			if (!_Items.ContainsKey (itemID)) {
				return 0;
			}

			return _Items [itemID];
		}
	}
}

