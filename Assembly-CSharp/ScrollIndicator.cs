using UnityEngine;

public class ScrollIndicator : MonoBehaviour
{
	private float topY = 106f;

	private float bottomY = -106f;

	public float val;

	private Vector3 v3;

	private void Start()
	{
	}

	private void Update()
	{
		this.v3 = Vector3.zero;
		this.v3.y = this.topY + (this.bottomY - this.topY) * this.val;
		base.transform.Find("nub").localPosition = this.v3;
	}
}
