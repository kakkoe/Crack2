using UnityEngine;

public class RelativePositionMaintainer : MonoBehaviour
{
	public Transform anchor;

	private Vector3 originalOffset;

	public float scale = 1f;

	private void Start()
	{
		this.originalOffset = base.transform.position - this.anchor.position;
	}

	private void Update()
	{
		base.transform.position = this.anchor.position + this.originalOffset * this.scale;
	}
}
