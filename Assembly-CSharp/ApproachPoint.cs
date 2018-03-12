using UnityEngine;

public class ApproachPoint : MonoBehaviour
{
	public bool floored = true;

	private bool hasLittlePoint;

	private Vector3 originalPosition;

	private Transform littlePoint;

	public float performerScale = 1f;

	private float littleness;

	private RaycastHit hInfo;

	private void Start()
	{
		Vector3 position = base.transform.position;
		if (position.z > -220f)
		{
			this.hasLittlePoint = (this.hasLittlePoint || (Object)base.transform.Find("LittlePoint") != (Object)null);
			if (this.hasLittlePoint && (Object)this.littlePoint == (Object)null)
			{
				this.littlePoint = base.transform.Find("LittlePoint");
				base.transform.Find("LittlePoint").SetParent(base.transform.parent);
				this.originalPosition = base.transform.localPosition;
			}
		}
	}

	private void Update()
	{
		if (this.hasLittlePoint)
		{
			base.transform.localPosition = this.originalPosition;
			this.littleness = 1f - this.performerScale;
			Transform transform = base.transform;
			transform.position += (this.littlePoint.transform.position - base.transform.position) * this.littleness;
		}
		if (this.floored && Physics.Raycast(base.transform.position + Vector3.up * 5f, Vector3.down, out this.hInfo, 10f, LayerMask.GetMask("StaticObjects")))
		{
			base.transform.position = this.hInfo.point;
		}
	}
}
