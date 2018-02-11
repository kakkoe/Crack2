using System;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
	public bool on = true;

	public Func<bool> onClick;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Find("on").gameObject.SetActive(this.on);
		base.transform.Find("off").gameObject.SetActive(!this.on);
	}

	public void clicked()
	{
		this.on = !this.on;
		if (this.onClick != null)
		{
			this.onClick();
		}
	}
}
