using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Battle
{	
	/// <summary>
	/// 阵型说明
	/// </summary>
	public class Formation
	{
		/// <summary>
		/// 单位说明
		/// </summary>
		private List<UnitModel> _UnitModels;

		/// <summary>
		/// 拥有单位说明
		/// </summary>
		/// <value>The unit papers.</value>
		public List<UnitModel> UnitModels {
			get { 
				return _UnitModels;
			}
		}

		public Formation ()
		{
			_UnitModels = new List<UnitModel>();
		}

		/// <summary>
		/// 添加单位说明
		/// </summary>
		/// <param name="unit">Unit.</param>
		public void AddUnit(UnitModel unit)
		{
			if (unit == null) {
				return;
			}

			_UnitModels.Add (unit);
		}

		/// <summary>
		/// 移除单位
		/// </summary>
		/// <param name="unit">Unit.</param>
		public void RemoveUnit(UnitModel unit)
		{
			if (unit == null) {
				return;
			}

			_UnitModels.Remove (unit);
		}
	}
}

