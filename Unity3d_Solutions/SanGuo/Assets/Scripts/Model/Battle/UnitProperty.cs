using System;
using Model.Base;

namespace Model.Battle
{
	/// <summary>
	/// 单位属性
	/// </summary>
	public class UnitProperty
	{
		/// <summary>
		/// 基础属性
		/// </summary>
		private Property _BaseProperty;
		/// <summary>
		/// 增加固定值
		/// </summary>
		private Property _AddFixProperty;
		/// <summary>
		/// 增加百分比
		/// </summary>
		private Property _AddPercentProperty;

		/// <summary>
		/// 基础属性
		/// </summary>
		public Property BaseProperty {
			get { 
				return _BaseProperty;
			}
		}

		/// <summary>
		/// 增加固定值
		/// </summary>
		public Property AddFixProperty {
			get { 
				return _AddFixProperty;
			}
		}

		/// <summary>
		/// 增加百分比
		/// </summary>
		public Property AddPercentProperty {
			get { 
				return _AddPercentProperty;
			}
		}
		
		public UnitProperty ()
		{
			_BaseProperty = new Property ();
			_AddFixProperty = new Property ();
			_AddPercentProperty = new Property ();
		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="type">PropertyType.</param>
		public float GetValue(PropertyType type)
		{
			float baseValue = _BaseProperty.GetValue (type);
			float fixValue = _AddFixProperty.GetValue (type);
			float percentValue = _AddPercentProperty.GetValue (type);

			float value = (baseValue + fixValue) * (1 + percentValue);
			if (value < 0) {
				value = 0;
			}

			return value;
		}
	}
}

