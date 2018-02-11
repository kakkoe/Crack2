using UnityEngine;

public class RayPositioner : Positioner
{
	public Transform rayTransform;

	public Vector3 positionOffset;

	public Vector3 rotationOffset;

	public float castLength = 100f;

	private void LateUpdate()
	{
		Transform transform = (!((Object)this.rayTransform != (Object)null)) ? base.transform : this.rayTransform;
		Quaternion rotation = transform.rotation * Quaternion.Euler(this.rotationOffset);
		Vector3 origin = transform.position + rotation * this.positionOffset;
		Ray ray = new Ray(origin, rotation * Vector3.forward);
		base.Reproject(ray, this.castLength, rotation * Vector3.up);
	}

	private void OnDrawGizmos()
	{
		Transform transform = (!((Object)this.rayTransform != (Object)null)) ? base.transform : this.rayTransform;
		Quaternion rotation = transform.rotation * Quaternion.Euler(this.rotationOffset);
		Vector3 from = transform.position + rotation * this.positionOffset;
		Gizmos.color = Color.black;
		Gizmos.DrawRay(from, rotation * Vector3.up * 0.4f);
		Gizmos.color = Color.white;
		Gizmos.DrawRay(from, rotation * Vector3.forward * this.castLength);
	}
}
