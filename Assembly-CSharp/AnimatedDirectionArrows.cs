using UnityEngine;

public class AnimatedDirectionArrows : MonoBehaviour
{
	private Transform[] arrows = new Transform[3];

	public bool on;

	private float t;

	private float s;

	private float trans;

	public float sharpness = 5f;

	public float speed = -10f;

	private Vector3 v3 = default(Vector3);

	public float onTime;

	private void Start()
	{
		this.arrows[0] = base.transform.Find("arrow0");
		this.arrows[1] = base.transform.Find("arrow1");
		this.arrows[2] = base.transform.Find("arrow2");
		for (int i = 0; i < 3; i++)
		{
			this.arrows[i].localScale = Vector3.zero;
		}
	}

	private void FixedUpdate()
	{
		this.t += Time.deltaTime * this.speed;
		if (this.t < 0f)
		{
			this.t += 10000f;
		}
		if (this.t > 10000f)
		{
			this.t -= 10000f;
		}
		if (this.on || this.onTime > 0f)
		{
			this.trans += (1f - this.trans) * Game.cap(Time.deltaTime * 15f, 0f, 1f);
		}
		else if (this.trans > 0f)
		{
			this.trans += (-0.1f - this.trans) * Game.cap(Time.deltaTime * 15f, 0f, 1f);
			if (this.trans < 0f)
			{
				this.trans = 0f;
				for (int i = 0; i < 3; i++)
				{
					this.arrows[i].localScale = Vector3.zero;
				}
			}
		}
		if (this.trans > 0f)
		{
			for (int j = 0; j < 3; j++)
			{
				this.s = Mathf.Pow(0.75f + Mathf.Cos((float)j + this.t) * 0.25f, this.sharpness);
				this.arrows[j].localScale = Vector3.one * this.s * this.trans;
			}
		}
		if (this.onTime > 0f)
		{
			this.onTime -= Time.deltaTime;
			if (this.onTime < 0f)
			{
				this.onTime = 0f;
			}
		}
	}
}
