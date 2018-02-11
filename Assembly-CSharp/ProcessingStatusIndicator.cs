using UnityEngine;

public class ProcessingStatusIndicator : MonoBehaviour
{
	private float statusScale;

	private float statusScaleVel;

	private float statusPulseDelay;

	public int status;

	private int curStatus = -1;

	private float relativeScale = 1f;

	private Vector3 originalEulers;

	private void Start()
	{
		Vector3 localScale = base.transform.localScale;
		this.relativeScale = localScale.x / 0.21f;
		this.originalEulers = base.transform.localEulerAngles;
	}

	private void Update()
	{
		if (this.status != this.curStatus)
		{
			this.curStatus = this.status;
			this.statusScale = 0f;
			this.statusScaleVel = 0f;
			base.transform.localEulerAngles = this.originalEulers;
			base.transform.Find("ready").gameObject.SetActive(this.curStatus == 1);
			base.transform.Find("processing").gameObject.SetActive(this.curStatus == 2);
		}
		this.statusScaleVel += (0.21f - this.statusScale) * Game.cap(Time.deltaTime * 20f, 0f, 1f);
		this.statusScale += this.statusScaleVel * Game.cap(Time.deltaTime * 30f, 0f, 1f);
		this.statusScaleVel -= this.statusScaleVel * Game.cap(Time.deltaTime * 4f, 0f, 1f);
		this.statusPulseDelay -= Time.deltaTime;
		if (this.statusPulseDelay <= 0f)
		{
			this.statusPulseDelay += 3f;
			this.statusScaleVel += 0.025f;
		}
		if (this.curStatus == 2)
		{
			base.transform.Rotate(0f, 0f, (0f - Time.deltaTime) * 85f);
		}
		base.transform.localScale = Vector3.one * this.statusScale * this.relativeScale;
	}
}
