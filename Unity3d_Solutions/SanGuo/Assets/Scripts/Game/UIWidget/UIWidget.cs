using UnityEngine;
using System.Collections;

namespace Game
{
	/// <summary>
	/// UI控件
	/// </summary>
	public class UIWidget : MonoBehaviour
	{
		/// <summary>
		/// 是否可用
		/// </summary>
		public bool Enable;
		/// <summary>
		/// 是否可见
		/// </summary>
		public bool Visible;
		/// <summary>
		/// 是否可以点击
		/// </summary>
		public bool TouchEnable;
		/// <summary>
		/// 位置
		/// </summary>
		public Vector2 Position;
		/// <summary>
		/// 宽度
		/// </summary>
		public int Width;
		/// <summary>
		/// 高度
		/// </summary>
		public int Height;
		/// <summary>
		/// 垂直对齐方式
		/// </summary>
		public VerticalAlign Vertical;
		/// <summary>
		/// 水平对齐方式
		/// </summary>
		public HorizontalAlign Horizontal;
		/// <summary>
		/// 是否需要重新绘制
		/// </summary>
		private bool _IsDirty;
		/// <summary>
		/// 是否需要重新绘制
		/// </summary>
		protected bool IsDirty {
			set { 
				_IsDirty = value;
			}
			get { 
				return _IsDirty;
			}
		}

		// Use this for initialization
		public void Start ()
		{
				
		}
	
		// Update is called once per frame
		public void Update ()
		{
			if (IsDirty == true) {
				UpdateWidget ();
				IsDirty = true;
			}
		}

		/// <summary>
		/// 更新控件
		/// </summary>
		private void UpdateWidget()
		{
			Canvas canvas = new Canvas ();
			transform.GetComponent<Canvas>().
		}
	}
}