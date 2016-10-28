using System;
using System.Collections.Generic;

namespace UI
{
	/// <summary>
	/// UI属性
	/// </summary>
	public class UIProperty
	{
		/// <summary>
		/// 属性成员
		/// </summary>
		private Dictionary<string, string> _Members;

		public UIProperty ()
		{
			_Members = new Dictionary<string, string> ();
		}

		/// <summary>
		/// 设置成员值
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public void SetValue(string name, string value)
		{
			_Members [name] = value;
		}

		public string GetValue(string name)
		{
			if (_Members.ContainsKey (name) == false) {
				return null;
			}

			return _Members [name];
		}

		/// <summary>
		/// 清空数据
		/// </summary>
		public void Clear()
		{
			_Members.Clear ();
		}
	}
}

