using UnityEngine;
using UnityEngine.UI;
using System;
using Game;
using Logic;

public class UIHomeLayer : UILayer
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
		_LabelFood = FindCanvas<Text> ("Canvas.CanvasResource.CanvasFood.Text");
		_LabelWood = FindCanvas<Text> ("Canvas.CanvasResource.CanvasWood.Text");

		_BtnFood = FindCanvas<Button> ("Canvas.CanvasButton.ButtonFood");
		_BtnWood = FindCanvas<Button> ("Canvas.CanvasButton.ButtonWood");

		_BtnFood.onClick.AddListener(delegate() {
			_MainPlayer.Food += 1;
			_LabelFood.text = _MainPlayer.Food.ToString();
		});

		_BtnWood.onClick.AddListener(delegate() {
			_MainPlayer.Wood += 1;
			_LabelWood.text = _MainPlayer.Wood.ToString();
		});
	}

	/// <summary>
	/// 初始化文本
	/// </summary>
	protected override void InitText()
	{
		_LabelFood.text = _MainPlayer.Food.ToString();
		_LabelWood.text = _MainPlayer.Wood.ToString();
	}

	void Update()
	{

	}

	/// <summary>
	/// 初始化报文监听
	/// </summary>
	protected override void InitPacket()
	{
	}
}

