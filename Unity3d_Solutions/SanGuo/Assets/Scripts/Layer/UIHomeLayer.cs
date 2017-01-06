using UnityEngine;
using UnityEngine.UI;
using System;
using Game;
using Logic;

public class UIHomeLayer : Layer
{
	/// <summary>
	/// 食物标签
	/// </summary>
	private Text _LabelFood;
	/// <summary>
	/// 木料标签
	/// </summary>
	private Text _LabelWood;
	/// <summary>
	/// 矿石标签
	/// </summary>
	private Text _LabelIron;

	/// <summary>
	/// 食物按钮
	/// </summary>
	private Button _BtnFood;
	/// <summary>
	/// 木材按钮
	/// </summary>
	private Button _BtnWood;
	/// <summary>
	/// 矿石按钮
	/// </summary>
	private Button _BtnIron;

	private Player _MainPlayer;

	public UIHomeLayer ()
	{
		_MainPlayer = LogicInstance.GetInstance ().GetMainPlayer ();
	}

	/// <summary>
	/// 初始化UI
	/// </summary>
	protected override void InitUI()
	{
		/*
		_LabelFood = FindCanvas<Text> ("Canvas.CanvasResource.CanvasFood.Text");
		_LabelWood = FindCanvas<Text> ("Canvas.CanvasResource.CanvasWood.Text");
		_LabelIron = FindCanvas<Text> ("Canvas.CanvasResource.CanvasIron.Text");
		*/

		_BtnFood = FindCanvas<Button> ("Canvas.CanvasButton.CanvasFood.Button");
		_BtnWood = FindCanvas<Button> ("Canvas.CanvasButton.CanvasWood.Button");
		_BtnIron = FindCanvas<Button> ("Canvas.CanvasButton.CanvasIron.Button");

		_BtnFood.onClick.AddListener(delegate() {
			_MainPlayer.Currency.Food += 1;
			//_LabelFood.text = _MainPlayer.Currency.Food.ToString();

			ReqPacketLogin packet = PacketHelp.GetRequestPacket<ReqPacketLogin>();
			packet.Name = PacketHelp.GetByteText("1212", 15);
			packet.Password = PacketHelp.GetByteText("123", 20);

			PacketHelp.Send (packet);
		});

		_BtnWood.onClick.AddListener(delegate() {
			_MainPlayer.Currency.Wood += 1;
			//_LabelWood.text = _MainPlayer.Currency.Wood.ToString();
		});

		_BtnIron.onClick.AddListener(delegate() {
			_MainPlayer.Currency.Iron += 1;
			//_LabelIron.text = _MainPlayer.Currency.Iron.ToString();
		});
	}

	/// <summary>
	/// 初始化文本
	/// </summary>
	protected override void InitText()
	{
		/*
		_LabelFood.text = _MainPlayer.Currency.Food.ToString();
		_LabelWood.text = _MainPlayer.Currency.Wood.ToString();
		_LabelIron.text = _MainPlayer.Currency.Iron.ToString();
		*/

		_BtnFood.GetComponentInChildren<Text>().text = GetLocalText (2);
		_BtnWood.GetComponentInChildren<Text>().text = GetLocalText (1);
		_BtnIron.GetComponentInChildren<Text>().text = GetLocalText (3);

		UserDefault.GetInstance ().Set ("Name", "LinGan");
		UserDefault.GetInstance ().Set ("Password", "123456");
	}

	/// <summary>
	/// 初始化报文监听
	/// </summary>
	protected override void InitPacket()
	{
		PacketHelp.RegisterPacket (PacketID.Login, OnReceivePacket_Login);
	}

	private void OnReceivePacket_Login(byte[] bytes)
	{
		ReqPacketLogin packet = PacketHelp.GetResponsePacket<ReqPacketLogin> (bytes);

		Log.Info ("Player ID " + PacketHelp.GetStringText (packet.Name));
	}

	/// <summary>
	/// 返回键处理
	/// </summary>
	protected override void OnEscapeHandler()
	{
		Log.Info ("Press Escape Key");
	}
}

