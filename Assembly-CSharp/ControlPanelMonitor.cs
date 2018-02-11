using UnityEngine;

public class ControlPanelMonitor : MonoBehaviour
{
	public bool turnedOn;

	private GameObject onScreen;

	private GameObject offScreen;

	private void Start()
	{
		this.onScreen = base.transform.Find("ControlPanel_screenBottom").gameObject;
		this.offScreen = base.transform.Find("ControlPanel_screenTop").gameObject;
	}

	private void Update()
	{
		this.onScreen.SetActive(this.turnedOn);
		this.offScreen.SetActive(!this.turnedOn);
	}
}
