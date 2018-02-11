using UnityEngine;

public class ToolbarItem : MonoBehaviour
{
	public float defaultY = 180f;

	public float offset;

	public float pulseBlipDistance = 0.5f;

	public float pulseLength = 6f;

	public float blipSize = 0.2f;

	public float originalScale = 1f;

	public float screenPositionX = 0.5f;

	public float screenPositionY = 0.5f;

	public Vector3 originalAngles;

	public bool manuallySetAngles;

	public bool useOriginals;

	private Vector3 v3;

	private float p;

	private float t;

	private float f;

	private void Start()
	{
		if (!this.manuallySetAngles)
		{
			this.originalAngles = base.transform.localEulerAngles;
		}
	}

	private void Update()
	{
		this.p = Time.time % this.pulseLength;
		this.t = this.offset % this.pulseLength;
		if (this.p - this.t > this.pulseBlipDistance)
		{
			this.p -= this.pulseLength;
		}
		else if (this.t - this.p > this.pulseBlipDistance)
		{
			this.t -= this.pulseLength;
		}
		this.f = Mathf.Abs(this.p - this.t) / this.pulseBlipDistance;
		if (this.f > 1f)
		{
			this.f = 1f;
		}
		this.f = Game.smoothLerp(this.f, 2f);
		this.v3.y = (0f - Game.cap(Game.gameInstance.mX - this.screenPositionX, -0.05f, 0.05f)) * 20f;
		this.v3.x = Game.cap(Game.gameInstance.mY - this.screenPositionY, -0.05f, 0.05f) * 20f;
		this.p = this.v3.x;
		this.v3.x *= 35f * (1f - Mathf.Abs(this.v3.y));
		this.v3.y *= 35f;
		this.v3.y += this.defaultY;
		this.v3.z = 0f;
		this.v3.x += Mathf.Cos(Time.time + this.screenPositionX * 10f + this.screenPositionY * 10f) * 10f;
		this.v3.y += Mathf.Cos(Time.time * 0.97f + this.screenPositionX * 10f + this.screenPositionY * 10f) * 10f;
		this.v3.z += Mathf.Cos(Time.time * 0.93f + this.screenPositionX * 10f + this.screenPositionY * 10f) * 10f;
		if (this.useOriginals)
		{
			this.v3 += this.originalAngles;
		}
		base.transform.localEulerAngles = this.v3;
		base.transform.localScale = Vector3.one * this.originalScale * (1f + this.blipSize * (1f - this.f));
	}
}
