using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Game.Controller
{
	/// <summary>
	/// 自带的寻路
	/// </summary>
	public class PathFindController : MonoBehaviour
	{
		public Transform Target;

		// Use this for initialization
		void Start ()
		{
			GetComponent<NavMeshAgent> ().SetDestination (Target.position);
		}

		// Update is called once per frame
		void Update ()
		{	
			
		}
	}
}