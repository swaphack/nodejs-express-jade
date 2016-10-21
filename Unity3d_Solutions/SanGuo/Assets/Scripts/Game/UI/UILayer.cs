using System;
using UnityEngine;
using Game;
using Foundation.Net;

namespace Game
{
	/// <summary>
	/// UI层
	/// </summary>
	public class UILayer : MonoBehaviour
	{
		/// <summary>
		/// 网络
		/// </summary>
		private static UINet _Net;

		/// <summary>
		/// 网络
		/// </summary>
		public UINet Net {
			get { 
				if (_Net == null) {
					_Net = new UINet ();
				}
				return _Net; 
			}
		}

		public UILayer ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		void Start ()
		{
			InitUI ();
			InitText ();
			InitPacket ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		void Update ()
		{
		}

		/// <summary>
		/// 重新加载
		/// </summary>
		public virtual void Reload ()
		{
		
		}

		/// <summary>
		/// 初始化UI
		/// </summary>
		protected virtual void InitUI ()
		{
		}

		/// <summary>
		/// 初始化文本
		/// </summary>
		protected virtual void InitText ()
		{
		
		}

		/// <summary>
		/// 初始化报文监听
		/// </summary>
		protected virtual void InitPacket ()
		{

		}

		/// <summary>
		/// 关闭
		/// </summary>
		protected virtual void Close ()
		{
		}

		/// <summary>
		/// 获取本地文本
		/// </summary>
		/// <returns>文本</returns>
		/// <param name="textId">文本编号</param>
		public string GetLocalText(int textId)
		{
			return GameInstance.GetInstance ().Text.GetMessage(textId);
		}
	}
}