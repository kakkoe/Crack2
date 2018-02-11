using UnityEngine;

public class GameModeButton : MonoBehaviour
{
	public bool over;

	private float spin;

	private Vector3 v3 = default(Vector3);

	private void Start()
	{
	}

	public void setOver(bool o)
	{
		this.over = o;
	}

	private void Update()
	{
		if (this.over)
		{
			if (this.spin < 1f)
			{
				this.spin += Time.smoothDeltaTime * 1.1f;
				if (this.spin > 1f)
				{
					this.spin = 1f;
				}
			}
		}
		else
		{
			this.spin *= 0.5f;
		}
		this.v3.z = Game.smoothLerp(this.spin, 1.5f) * 360f;
		this.v3.x = 0f;
		this.v3.y = 0f;
		base.transform.Find("button").localEulerAngles = this.v3;
		this.v3.x = (this.v3.y = (this.v3.z = 0.7f + Game.smoothLerp(this.spin, 2f) * 0.3f));
		base.transform.Find("button").localScale = this.v3;
	}
}
