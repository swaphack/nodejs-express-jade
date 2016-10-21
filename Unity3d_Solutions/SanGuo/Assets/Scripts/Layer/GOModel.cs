using UnityEngine;
using System.Collections;
using Game;

public class GOModel : MonoBehaviour {

	GameObject _3DGameObject;

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
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vect = new Vector3 ();
		vect.y = 1;
		if (_3DGameObject != null) {
			_3DGameObject.transform.Rotate (vect);
		}
	}
}