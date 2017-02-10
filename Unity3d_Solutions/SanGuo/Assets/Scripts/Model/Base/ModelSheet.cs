using System;
using System.Collections.Generic;

namespace Model.Base
{
	/// <summary>
	/// 模型实例
	/// </summary>
	public class ModelSheet<T> where T : IModelItem
	{
		/// <summary>
		/// 编号
		/// </summary>
		private ModelType _ID;
		/// <summary>
		/// 数据
		/// </summary>
		private Dictionary<int, T> _Items;

		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		public ModelType ID {
			get { 
				return _ID;
			}
		}

		public ModelSheet (ModelType id)
		{
			_ID = id;
			_Items = new Dictionary<int, T> ();
		}

		/// <summary>
		/// 添加数据
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add(T item)
		{
			if (item == null) {
				return;
			}

			_Items.Add (item.ID, item);
		}

		/// <summary>
		/// 移除数据
		/// </summary>
		/// <param name="item">Item.</param>
		public void Remove(int itemID)
		{
			_Items.Remove (itemID);
		}

		/// <summary>
		/// 查找数据
		/// </summary>
		/// <param name="itemID">Item I.</param>
		public T Find(int itemID)
		{
			if (!_Items.ContainsKey (itemID)) {
				return default(T);
			}

			return _Items [itemID];
		}

		/// <summary>
		/// 清空数据
		/// </summary>
		public void Clear()
		{
			_Items.Clear ();
		}
	}
}

