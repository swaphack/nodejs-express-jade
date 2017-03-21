using UnityEngine;
using UnityEngine.UI;
using System;
using Game;
using Game.Layer;
using Game.Helper;

namespace View.Layer
{
	public class UILoginLayer : NormalLayer 
	{
		/// <summary>
		/// 登录按钮
		/// </summary>
		private Button _BtnLogin = null;
		/// <summary>
		/// 用户名称文本输入栏
		/// </summary>
		private InputField _InputUserName = null;

		public UILoginLayer ()
		{
			FileName = "LoginLayer";
		}

		/// <summary>
		/// 初始化UI
		/// </summary>
		protected override void InitControl()
		{
			_BtnLogin = GameObject.Find ("Button").GetComponent<Button> ();
			_BtnLogin.onClick.AddListener (delegate() {	this.OnClick (_BtnLogin); });

			_InputUserName = GameObject.Find ("InputField").GetComponent<InputField> ();
		}

		/// <summary>
		/// 初始化文本
		/// </summary>
		protected override void InitText()
		{
			Text text = _BtnLogin.GetComponentInChildren<Text> ();
			text.text = GetLocalText (1);
		}

		/// <summary>
		/// 初始化报文监听
		/// </summary>
		protected override void InitPacketListeners()
		{
			PacketHelp.RegisterPacketHandler (PacketID.Login, OnReceivePacket_Login);
		}

		/// <summary>
		/// 初始化报文监听
		/// </summary>
		protected override void DisposePacketListeners()
		{
			PacketHelp.UnregisterPacketHandler (PacketID.Login, OnReceivePacket_Login);
		}

		private void OnClick(Button sender)	
		{
			string name = _InputUserName.text;

			ReqPacketLogin packet = PacketHelp.GetRequestPacket<ReqPacketLogin>();
			packet.Name = PacketHelp.GetByteText(name, 15);
			packet.Password = PacketHelp.GetByteText("123", 20);

			PacketHelp.Send (packet);
		}

		private void OnReceivePacket_Login(byte[] bytes)
		{
			ReqPacketLogin packet = PacketHelp.GetResponsePacket<ReqPacketLogin> (bytes);

			Log.Info ("Player ID " + packet.Name.ToString());
		}
	}
}
