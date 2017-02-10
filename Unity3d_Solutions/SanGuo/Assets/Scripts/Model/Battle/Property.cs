using System;
using System.Collections.Generic;
using Model.Base;

namespace Model.Battle
{
	/// <summary>
	/// 属性
	/// </summary>
	public class Property
	{
		/// <summary>
		/// 属性集合{属性类型，属性值}
		/// </summary>
		private Dictionary<PropertyType, float> _Properties;

		public Property ()
		{
			_Properties = new Dictionary<PropertyType, float> ();
		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="type">PropertyType.</param>
		public float GetValue(PropertyType type)
		{
			if (_Properties.ContainsKey (type)) {
				return _Properties [type];
			}

			return 0;
		}

		/// <summary>
		/// 设置属性值
		/// </summary>
		/// <param name="type">PropertyType.</param>
		/// <param name="value">Value.</param>
		public void SetValue(PropertyType type, int value)
		{
			if (_Properties.ContainsKey (type)) {
				_Properties [type] = value;
			} else {
				_Properties.Add (type, value);
			}
		}  
	}
}

