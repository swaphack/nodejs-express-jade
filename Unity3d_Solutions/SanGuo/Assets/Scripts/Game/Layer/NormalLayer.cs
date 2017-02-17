using System;
using UnityEngine;
using Game;
using Foundation.Net;

namespace Game.Layer
{
	/// <summary>
	/// 通用UI层
	/// </summary>
	public class NormalLayer : UILayer
	{
		/// <summary>
		/// 是否初始控件了
		/// </summary>
		private bool _InitControl;
		/// <summary>
		/// 是否监听返回键
		/// </summary>
		protected bool EnabledBackKeyListener;

		public NormalLayer ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		protected override void OnEnter ()
		{
			base.OnEnter ();
			if (_InitControl == false) {
				InitControl ();
				InitText ();
				_InitControl = true;
			}
			InitPacketListeners ();
			InitEventListeners ();

			if (EnabledBackKeyListener) {
				if (GameInstance.GetInstance ().Device != null) {
					GameInstance.GetInstance ().Device.AddEscKeyHandler (EscapeKeyHandler);
				}
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		protected override void OnExit ()
		{
			base.OnExit ();

			DisposePacketListeners ();
			DisposeEventListeners ();

			if (EnabledBackKeyListener) {
				if (GameInstance.GetInstance ().Device != null) {
					GameInstance.GetInstance ().Device.RemoveEscKeyHandler (EscapeKeyHandler);
				}
			}
		}

		/// <summary>
		/// 返回键处理
		/// </summary>
		protected void EscapeKeyHandler()
		{
			OnEscapeHandler ();
		}

		/// <summary>
		/// 初始化UI控件
		/// </summary>
		protected virtual void InitControl ()
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
		protected virtual void InitPacketListeners ()
		{

		}

		/// <summary>
		/// 初始化事件监听
		/// </summary>
		protected virtual void InitEventListeners ()
		{

		}

		/// <summary>
		/// 移除报文监听
		/// </summary>
		protected virtual void DisposePacketListeners ()
		{

		}

		/// <summary>
		/// 移除事件监听
		/// </summary>
		protected virtual void DisposeEventListeners ()
		{

		}

		/// <summary>
		/// 返回键处理，需重写
		/// </summary>
		protected virtual void OnEscapeHandler ()
		{
			
		}
	}
}