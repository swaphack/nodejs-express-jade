using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Game.Listener;

public class GOModel : TouchListener {
	// Use this for initialization
	void Start () {
		Target = this.GetComponent<Collider>();

		IsTouchEnable = true;

		Log.Info ("Screen Size(" + Screen.width + "," + Screen.height + ")");

		/*
		this.transform.Translate (0, 1f, 0);
		this.transform.Rotate (10, this.transform.rotation.y, this.transform.rotation.z);
		this.transform.localScale += new Vector3(100, this.transform.localScale.y, this.transform.localScale.z);
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnTouchBegan(Vector2 vector)
	{
	}

	protected override void OnTouchMoved(Vector2 vector)
	{
	}

	protected override void OnTouchEnded(Vector2 vector)
	{
	}
}