using UnityEngine;
using System;
using UnityEngine.UI;
using Game.Helper;
using Game.Layer;
using Controller.Role;

namespace View.Layer
{
	/// <summary>
	/// 主界面
	/// </summary>
	public class UIHomeLayer : NormalLayer
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
		/// <summary>
		/// 时间
		/// </summary>
		private Text _LabelTime;

		public UIHomeLayer ()
		{
			FileName = "HomeLayer";
		}

		/// <summary>
		/// 初始化UI
		/// </summary>
		protected override void InitControl()
		{
			_LabelFood = FindCanvas<Text> ("CanvasResource.CanvasFood.Text");
			_LabelWood = FindCanvas<Text> ("CanvasResource.CanvasWood.Text");
			_LabelIron = FindCanvas<Text> ("CanvasResource.CanvasIron.Text");

			_BtnFood = FindCanvas<Button> ("CanvasButton.CanvasFood.Button");
			_BtnWood = FindCanvas<Button> ("CanvasButton.CanvasWood.Button");
			_BtnIron = FindCanvas<Button> ("CanvasButton.CanvasIron.Button");

			_LabelTime = FindCanvas<Text> ("CanvasMiddle.Text");

			_BtnFood.onClick.AddListener(delegate() {
				Player.MainPlayer.Resource.Food += 1;

				ReqPacketLogin packet = PacketHelp.GetRequestPacket<ReqPacketLogin>();
				packet.Name = PacketHelp.GetByteText("1212", 15);
				packet.Password = PacketHelp.GetByteText("123", 20);
				PacketHelp.Send (packet);
			});

			_BtnWood.onClick.AddListener(delegate() {
				Player.MainPlayer.Resource.Wood += 1;
			});

			_BtnIron.onClick.AddListener(delegate() {
				Player.MainPlayer.Resource.Iron += 1;
			});
		}

		/// <summary>
		/// 初始化文本
		/// </summary>
		protected override void InitText()
		{
			_LabelFood.text = Player.MainPlayer.Resource.Food.ToString();
			_LabelWood.text = Player.MainPlayer.Resource.Wood.ToString();
			_LabelIron.text = Player.MainPlayer.Resource.Iron.ToString();

			_BtnFood.GetComponentInChildren<Text>().text = GetLocalText (2);
			_BtnWood.GetComponentInChildren<Text>().text = GetLocalText (1);
			_BtnIron.GetComponentInChildren<Text>().text = GetLocalText (3);

			UserDefault.GetInstance ().Set ("Name", "LinGan");
			UserDefault.GetInstance ().Set ("Password", "123456");
		}

		/// <summary>
		/// 注册报文监听
		/// </summary>
		protected override void InitPacketListeners()
		{
			PacketHelp.RegisterPacketHandler (PacketID.Login, OnReceivePacket_Login);
			PacketHelp.RegisterPacketHandler (PacketID.Error, OnReceivePacket_Error);
		}

		/// <summary>
		/// 初始化事件监听
		/// </summary>
		protected override void InitEventListeners ()
		{
			Player.MainPlayer.Resource.AddChangedNotify (ResType.Food, onFoodChanged);
			Player.MainPlayer.Resource.AddChangedNotify (ResType.Wood, onWoodChanged);
			Player.MainPlayer.Resource.AddChangedNotify (ResType.Iron, onIronChanged);
		}

		private void onFoodChanged() 
		{
			_LabelFood.text = Player.MainPlayer.Resource.Food.ToString();
		}

		private void onWoodChanged()
		{
			_LabelWood.text = Player.MainPlayer.Resource.Wood.ToString();
		}

		private void onIronChanged()
		{
			_LabelIron.text = Player.MainPlayer.Resource.Iron.ToString();
		}

		/// <summary>
		/// 移除报文监听
		/// </summary>
		protected override void DisposePacketListeners()
		{
			PacketHelp.UnregisterPacketHandler (PacketID.Login, OnReceivePacket_Login);
			PacketHelp.UnregisterPacketHandler (PacketID.Error, OnReceivePacket_Error);
		}

		/// <summary>
		/// 移除事件监听
		/// </summary>
		protected override void DisposeEventListeners()
		{
			PacketHelp.UnregisterPacketHandler (PacketID.Login, OnReceivePacket_Login);
			PacketHelp.UnregisterPacketHandler (PacketID.Error, OnReceivePacket_Error);

			Player.MainPlayer.Resource.RemoveChangedNotify (ResType.Food, onFoodChanged);
			Player.MainPlayer.Resource.RemoveChangedNotify (ResType.Wood, onWoodChanged);
			Player.MainPlayer.Resource.RemoveChangedNotify (ResType.Iron, onIronChanged);
		}

		private void OnReceivePacket_Login(byte[] bytes)
		{
			ReqPacketLogin packet = PacketHelp.GetResponsePacket<ReqPacketLogin> (bytes);
			Log.Info ("Player ID " + PacketHelp.GetStringText (packet.Name));
		}

		private void OnReceivePacket_Error(byte[] bytes)
		{
			RespPacketError packet = PacketHelp.GetResponsePacket<RespPacketError> (bytes);
			Log.Info ("Resp Error Packet, ID : %d" + packet.Header.PacketID);
		}

		/// <summary>
		/// 返回键处理
		/// </summary>
		protected override void OnEscapeHandler()
		{
			Log.Info ("Press Escape Key");
		}

		/// <summary>
		/// 更新UI
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected override void UpdateUI(float dt)
		{
			string time = DateTime.Now.ToString("HH:mm:ss");
			_LabelTime.text = time;
		}
	}
}