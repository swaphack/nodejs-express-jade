using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Game;

public class GOModel : MonoBehaviour {

	GameObject _3DGameObject;
	Shader _lastShader;

	// Use this for initialization
	void Start () {
		_3DGameObject = GameObject.Find ("cartoonmedhouse2");

		GOControl control = new GOControl (_3DGameObject);
		control.IsTouchEnable = true;
		control.OnTouch += delegate (ITouchDispatcher dispatcher, TouchPhase state, Vector2 vector) {
			if (state == TouchPhase.Began) 
			{
				Debug.Log("touch me" + dispatcher.Target.transform.name.ToString());	
			}
		};

		Debug.Log ("Screen Size(" + Screen.width + "," + Screen.height + ")");

		GameObject copy = (GameObject)Instantiate(_3DGameObject, _3DGameObject.transform.parent);
		copy.transform.Translate (0, 1f, 0);
		copy.transform.Rotate (10, copy.transform.rotation.y, copy.transform.rotation.z);
		copy.transform.localScale += new Vector3(100, copy.transform.localScale.y, copy.transform.localScale.z);

	}
	
	// Update is called once per frame
	void Update () {
		if (_3DGameObject != null) {
			_3DGameObject.transform.Rotate(0,1,0);
			//_3DGameObject.transform.Translate(0.01f,0, 0);
			//_3DGameObject.transform.localScale += new Vector3(-0.001f, -0.001f, -0.001f);
		}
	}
}