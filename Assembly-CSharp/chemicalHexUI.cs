using UnityEngine;

public class chemicalHexUI : MonoBehaviour
{
	public float offset;

	private float yRot;

	private Vector3 v3;

	private float f;

	private float v;

	private void Start()
	{
	}

	private void Update()
	{
		this.v3.x = 0f;
		this.f = Mathf.Abs(Time.time * 1.6f % 6f - (2f + this.offset));
		this.v = 0.2f;
		if (this.f < 2f)
		{
			this.v += Mathf.Pow((2f - this.f) / 2f, 2f) * 2f;
		}
		this.yRot += this.v * 710f * Time.smoothDeltaTime;
		this.v3.y = this.yRot;
		this.v3.z = -90f;
		base.transform.localEulerAngles = this.v3;
	}

	private float signOf(float f)
	{
		if (f < 0f)
		{
			return -1f;
		}
		return 1f;
	}
}
