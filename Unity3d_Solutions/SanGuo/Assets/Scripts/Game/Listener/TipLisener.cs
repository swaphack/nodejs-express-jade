using System;
using UnityEngine;

namespace Game
{
	public class TipLisener : MonoBehaviour
	{
		public TipLisener ()
		{
		}

		void Start()
		{
		}

		void Update()
		{
			Collider collider = this.GetComponent<Collider> ();
			Camera camera = Camera.main;

			Vector3 size = collider.bounds.size;
		}
	}
}

