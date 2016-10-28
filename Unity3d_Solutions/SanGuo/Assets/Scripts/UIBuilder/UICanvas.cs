using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
	public class UICanvas : UIControl
	{
		/// <summary>
		/// 名称
		/// </summary>
		public override string Name { get { return "Canvas"; } }

		/// <summary>
		/// 初始化
		/// </summary>
		public override void Init()
		{
			if (Control == null) {
				Control = new UnityEngine.Canvas ();
			}
		}

		/// <summary>
		/// 解析属性
		/// </summary>
		protected override void ParseAttributes()
		{
			string value;

			Vector3 position;

			if (GetAttribute ("x", out value)) {
				position.x = Convert.ToSingle (value);
			}
		}

		/// <summary>
		/// 生成属性
		/// </summary>
		protected override void MakeAttributes()
		{
		}
	}
}

