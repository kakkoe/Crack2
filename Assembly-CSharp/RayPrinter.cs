using UnityEngine;

public class RayPrinter : Printer
{
	public LayerMask layers;

	public void PrintOnRay(Ray Ray, float RayLength, Vector3 DecalUp = default(Vector3))
	{
		if (DecalUp == Vector3.zero)
		{
			DecalUp = Vector3.up;
		}
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(Ray, out raycastHit, RayLength, this.layers.value))
		{
			base.Print(raycastHit.point, Quaternion.LookRotation(-raycastHit.normal, DecalUp), raycastHit.transform, raycastHit.collider.gameObject.layer, 1f, false);
		}
	}
}
