using System;
using System.Collections.Generic;
using Foundation.Notify;

namespace Control
{
	/// <summary>
	/// 资源类型
	/// </summary>
	public enum ResType
	{
		/// <summary>
		/// 食物
		/// </summary>
		Food,
		/// <summary>
		/// 木头
		/// </summary>
		Wood,
		/// <summary>
		/// 矿石
		/// </summary>
		Iron,
	}

	/// <summary>
	/// 资源项
	/// </summary>
	public class ResItem 
	{
		/// <summary>
		/// 资源类型
		/// </summary>
		public ResType Type;
		/// <summary>
		/// 资源值
		/// </summary>
		public int Value;
	}

	/// <summary>
	/// 资源
	/// </summary>
	public class Resource
	{
		/// <summary>
		/// 资源
		/// </summary>
		Dictionary<ResType, ResItem> _Items;
		/// <summary>
		/// 资源改变时的处理
		/// </summary>
		private Notifition<ResType> _ChangedNotifition;

		public Resource ()
		{
			_Items = new Dictionary<ResType, ResItem> ();
			_ChangedNotifition = new Notifition<ResType> ();
		}

		/// <summary>
		/// 添加资源改变时的通知
		/// </summary>
		/// <param name="resType">Res type.</param>
		/// <param name="handler">Handler.</param>
		public void AddChangedNotify(ResType resType, NotifyHandler handler)
		{
			if (handler == null) {
				return;
			}

			_ChangedNotifition.AddListener (resType, handler);
		}

		/// <summary>
		/// 获取资源值
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="resType">Res type.</param>
		public int GetValue(ResType resType)
		{
			if (_Items.ContainsKey (resType)) {
				return _Items [resType].Value;
			}

			return 0;
		}

		/// <summary>
		/// 设置资源值
		/// </summary>
		/// <param name="resType">Res type.</param>
		/// <param name="value">Value.</param>
		public void SetValue(ResType resType, int value)
		{
			if (_Items.ContainsKey (resType)) {
				_Items [resType].Value = value;
			} else {
				ResItem item = new ResItem ();
				item.Type = resType;
				item.Value = value;
				_Items.Add (resType, item);
			}

			_ChangedNotifition.Notify (resType);
		}

		/// <summary>
		/// 食物
		/// </summary>
		public int Food {
			get { 
				return GetValue(ResType.Food);
			}
			set { 
				SetValue (ResType.Food, value);
			}
		}
		/// <summary>
		/// 木材
		/// </summary>
		public int Wood {
			get { 
				return GetValue(ResType.Wood);
			}
			set { 
				SetValue (ResType.Wood, value);
			}
		}
		/// <summary>
		/// 矿石
		/// </summary>
		public int Iron {
			get { 
				return GetValue(ResType.Iron);
			}
			set { 
				SetValue (ResType.Iron, value);
			}
		}
	}
}