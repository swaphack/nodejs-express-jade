using UnityEngine;

namespace Game.Controller
{
	public class UICameraController : MonoBehaviour
	{
		public int Width = 1024;
		public int Height = 768;
		
		void Start()
		{
			float height;
			if ((1.0f * Screen.height / Screen.width) > (1.0f * Height / Width)) {
				height = 1.0f * Width / Screen.width * Screen.height;
			} else {
				height = Height;
			}

			float scale = height / Height;

			Camera camera = GetComponent<Camera> ();
			camera.fieldOfView *= scale;
		}
	}
}

