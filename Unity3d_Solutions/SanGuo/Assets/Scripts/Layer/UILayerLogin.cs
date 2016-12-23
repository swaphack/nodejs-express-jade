using UnityEngine;
using UnityEngine.UI;
using System;
using Game;

public class UILayerLogin : UILayer 
{
	/// <summary>
	/// 登录按钮
	/// </summary>
	private Button _BtnLogin = null;
	/// <summary>
	/// 用户名称文本输入栏
	/// </summary>
	private InputField _InputUserName = null;

	/// <summary>
	/// 初始化UI
	/// </summary>
	protected override void InitUI()
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
	protected override void InitPacket()
	{
		Net.RegisterPacket (PacketID.Login, OnReceivePacket_Login);
	}

	private void OnClick(Button sender)	
	{
		string name = _InputUserName.text;
		
		ReqPacketLogin packet = Net.GetRequestPacket<ReqPacketLogin>(PacketID.Login);
		packet.Name = Net.GetByteText(name, 15);
		packet.Password = Net.GetByteText("123", 20);

		this.Net.Send (packet);
	}

	private void OnReceivePacket_Login(byte[] bytes)
	{
		ReqPacketLogin packet = Net.GetResponsePacket<ReqPacketLogin> (bytes);

		Log.Write ("Player ID " + packet.Name.ToString());
	}
}
