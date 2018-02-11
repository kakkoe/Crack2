using UnityEngine;

public class Tooltip : MonoBehaviour
{
	private float size;

	private float vel;

	private void OnDisable()
	{
		this.size = 0f;
		this.vel = 0f;
	}

	private void OnEnable()
	{
		this.size = 0f;
		this.vel = 0f;
		base.transform.localScale = Vector3.one * this.size;
	}

	private void Update()
	{
		this.vel += (1f - this.size) * Game.cap(Time.deltaTime * 14f, 0f, 1f);
		this.size += this.vel * Game.cap(Time.deltaTime * 75f, 0f, 1f);
		this.vel -= this.vel * 0.3f * Game.cap(Time.deltaTime * 75f, 0f, 1f);
		base.transform.localScale = Vector3.one * this.size;
	}
}
