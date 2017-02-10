using System;
using System.Collections.Generic;

namespace Controller.Battle
{
	/// <summary>
	/// 单位列表
	/// </summary>
	public class UnitSheet
	{
		/// <summary>
		/// 单位
		/// </summary>
		private Dictionary<int, Unit> _Units;

		/// <summary>
		/// 单位个数
		/// </summary>
		/// <value>The count.</value>
		public int Count { 
			get {
				return _Units.Count;	
			} 
		}

		public UnitSheet ()
		{
			_Units = new Dictionary<int, Unit> ();
		}

		/// <summary>
		/// 查找单位
		/// </summary>
		/// <returns>The unit.</returns>
		/// <param name="id">ID.</param>
		public Unit FindUnit(int id)
		{
			if (!_Units.ContainsKey (id)) {
				return null;
			}

			return _Units [id];
		}

		/// <summary>
		/// 添加单位
		/// </summary>
		/// <param name="unit">Unit.</param>
		public void AddUnit(Unit unit)
		{
			if (unit == null) {
				return;
			}
			_Units [unit.ID] = unit;
		}

		/// <summary>
		/// 移除单位
		/// </summary>
		/// <param name="id">ID.</param>
		public void RemoveUnit(int id)
		{
			_Units.Remove (id);
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt) {
			foreach (KeyValuePair<int, Unit> item in _Units) {
				item.Value.Update(dt);
			}
		}
	}
}

