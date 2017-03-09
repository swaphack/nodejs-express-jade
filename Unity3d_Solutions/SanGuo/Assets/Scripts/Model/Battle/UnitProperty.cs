using System;
using Model.Base;

namespace Model.Battle
{
	/// <summary>
	/// 属性广播
	/// </summary>
	public delegate void OnPropertyBroadcast(PropertyType type, float value);

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
		/// 当前值
		/// </summary>
		private Property _CurrentProperty;

		/// <summary>
		/// 当前属性改变
		/// </summary>
		public event OnPropertyBroadcast OnCurrentPropertyChanged;

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

		/// <summary>
		/// 当前值
		/// </summary>
		/// <value>The current property.</value>
		public Property CurrentProperty {
			get { 
				return _CurrentProperty;
			}
		}
		
		public UnitProperty ()
		{
			_BaseProperty = new Property ();
			_AddFixProperty = new Property ();
			_AddPercentProperty = new Property ();
			_CurrentProperty = new Property ();
		}

		/// <summary>
		/// 获取最大属性值
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="type">PropertyType.</param>
		public float GetMaxValue(PropertyType type)
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

		/// <summary>
		/// 获取当前属性值
		/// </summary>
		/// <returns>The value.</returns>
		public float GetValue(PropertyType type)
		{
			return _CurrentProperty.GetValue (type);
		}

		/// <summary>
		/// 增加生命
		/// </summary>
		/// <param name="value">Value.</param>
		public void AddHP(float value)
		{
			if (value == 0) {
				return;
			}
			PropertyType type = PropertyType.HitPoints;
			float curVal = GetValue (type);
			if (curVal <= 0) {
				return;
			}

			float newVal = curVal + value;
			_CurrentProperty.SetValue (type, newVal);
			OnCurrentPropertyChanged (type, newVal);
		}

		/// <summary>
		/// 是否已死完
		/// </summary>
		/// <value><c>true</c> if dead; otherwise, <c>false</c>.</value>
		public bool Dead {
			get { 
				return GetValue (PropertyType.HitPoints) <= 0;
			}
		}
	}
}

