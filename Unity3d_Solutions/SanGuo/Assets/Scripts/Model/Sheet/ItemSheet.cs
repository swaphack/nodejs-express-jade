using System;
using Model.Base;
using Model.Item;

namespace Model.Sheet
{
	/// <summary>
	/// 物品
	/// </summary>
	public class ItemSheet : ModelSheet<ItemModel>
	{
		public ItemSheet ()
			:base(ModelType.Item)
		{
		}
	}
}

