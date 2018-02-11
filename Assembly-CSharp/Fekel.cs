using UnityEngine;

public class Fekel : MonoBehaviour
{
	private float offset;

	private float stretch;

	public float speed = 1f;

	private Vector3 v3;

	private float f;

	private void Start()
	{
		this.offset = Random.value * 1000f;
		this.stretch = 1f + Random.value * 0.1f;
	}

	private void Update()
	{
		this.f = Mathf.Cos(Time.timeSinceLevelLoad * this.speed * this.stretch * 0.04f + this.offset);
		this.v3.x = this.signOf(this.f) * Mathf.Pow(this.f, 3f) * 5f;
		this.f = Mathf.Cos(Time.timeSinceLevelLoad * this.speed * this.stretch * 0.3f + this.offset);
		this.v3.y = this.signOf(this.f) * Mathf.Pow(this.f, 3f) * 950f + Time.timeSinceLevelLoad * 270f * this.speed;
		this.f = Mathf.Cos(Time.timeSinceLevelLoad * this.speed * this.stretch * 0.07f + this.offset);
		this.v3.z = this.signOf(this.f) * Mathf.Pow(this.f, 3f) * 5f;
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
