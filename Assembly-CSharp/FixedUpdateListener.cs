using System;
using UnityEngine;

public class FixedUpdateListener : MonoBehaviour
{
	public Func<bool> callback;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void FixedUpdate()
	{
		if (this.callback != null)
		{
			this.callback();
		}
	}
}
