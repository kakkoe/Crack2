using UnityEngine;

public abstract class Positioner : MonoBehaviour
{
	public Projection projection;

	public LayerMask layers;

	public bool alwaysVisible;

	private Projection proj;

	private void OnDisable()
	{
		if ((Object)this.proj != (Object)null)
		{
			this.proj.gameObject.SetActive(false);
		}
	}

	protected virtual void Start()
	{
		this.proj = Object.Instantiate(this.projection.gameObject, DynamicDecals.GetPool("Default").Parent).GetComponent<Projection>();
		this.proj.name = "Projection";
	}

	protected void Reproject(Ray Ray, float CastLength, Vector3 ReferenceUp)
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(Ray, out raycastHit, float.PositiveInfinity, this.layers.value))
		{
			this.proj.gameObject.SetActive(true);
			this.proj.transform.rotation = Quaternion.LookRotation(-raycastHit.normal, ReferenceUp);
			this.proj.transform.position = raycastHit.point;
		}
		else if (!this.alwaysVisible)
		{
			this.proj.gameObject.SetActive(false);
		}
	}

	private Vector3 Divide(Vector3 A, Vector3 B)
	{
		return new Vector3(A.x / B.x, A.y / B.y, A.z / B.z);
	}
}
