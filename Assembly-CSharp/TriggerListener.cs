using System;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
	public Func<Collider, bool, bool> callback;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (this.callback != null)
		{
			this.callback(collider, true);
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		if (this.callback != null)
		{
			this.callback(collider, false);
		}
	}
}
