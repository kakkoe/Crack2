using UnityEngine;

public class ApparatusHologram : MonoBehaviour
{
	private Transform tube;

	private Transform screen;

	private Vector3 v3;

	private void Start()
	{
		this.tube = base.transform.Find("HologramTube");
		this.v3.x = 1f;
		this.v3.y = 1f;
		this.v3.z = 1f;
	}

	private void Update()
	{
		if ((Object)this.screen == (Object)null)
		{
			this.screen = base.transform.Find("contents");
		}
		else
		{
			this.tube.LookAt(this.screen);
			this.v3.x = (this.screen.position - this.tube.position).magnitude * 0.31f;
			this.v3.z = (this.screen.position - this.tube.position).magnitude * 0.31f;
			this.tube.localScale = this.v3;
		}
	}
}
