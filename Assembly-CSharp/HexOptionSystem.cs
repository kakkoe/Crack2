using System;
using UnityEngine;

public class HexOptionSystem : MonoBehaviour
{
	public hexOption[] options;

	public Func<bool> onChange;

	private void Start()
	{
		this.options = base.GetComponentsInChildren<hexOption>();
	}

	private void Update()
	{
	}

	public void clearAll()
	{
		for (int i = 0; i < this.options.Length; i++)
		{
			this.options[i].selected = false;
		}
	}

	public void changed()
	{
		if (this.onChange != null)
		{
			this.onChange();
		}
	}
}
