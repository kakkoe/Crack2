using System;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
	public Func<Collision, bool, bool> callback;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.callback != null)
		{
			this.callback(collision, true);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (this.callback != null)
		{
			this.callback(collision, false);
		}
	}
}
