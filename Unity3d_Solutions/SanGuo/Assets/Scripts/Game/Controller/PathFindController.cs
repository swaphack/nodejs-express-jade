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

		private NavMeshAgent _Agent;

		// Use this for initialization
		void Start ()
		{
			_Agent = this.GetComponent<NavMeshAgent> ();
		}

		// Update is called once per frame
		void Update ()
		{	
			_Agent.SetDestination (Target.position);
		}
	}
}