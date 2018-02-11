using UnityEngine;

public class RayCollisionPrinter : Printer
{
	public CollisionCondition condition;

	public float conditionTime = 1f;

	public LayerMask layers;

	public Transform castPoint;

	public Vector3 positionOffset;

	public Vector3 rotationOffset;

	public float castLength = 1f;

	private CollisionData collision;

	private float timeElapsed;

	private bool delayPrinted;

	private void FixedUpdate()
	{
		this.CastCollision(Time.fixedDeltaTime);
	}

	private void CastCollision(float deltaTime)
	{
		Transform transform = (!((Object)this.castPoint != (Object)null)) ? base.transform : this.castPoint;
		Quaternion rotation = transform.rotation * Quaternion.Euler(this.rotationOffset);
		Vector3 origin = transform.position + rotation * this.positionOffset;
		Ray ray = new Ray(origin, rotation * Vector3.forward);
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(ray, out raycastHit, this.castLength, this.layers.value))
		{
			this.collision = new CollisionData(raycastHit.point, Quaternion.LookRotation(-raycastHit.normal, rotation * Vector3.up), raycastHit.transform, raycastHit.collider.gameObject.layer);
			if (this.condition == CollisionCondition.Constant)
			{
				this.PrintCollision(this.collision);
			}
			if (this.timeElapsed == 0f && this.condition == CollisionCondition.Enter)
			{
				this.PrintCollision(this.collision);
			}
			this.timeElapsed += deltaTime;
			if (this.condition == CollisionCondition.Delay && this.timeElapsed >= this.conditionTime && !this.delayPrinted)
			{
				this.PrintCollision(this.collision);
				this.delayPrinted = true;
			}
		}
		else
		{
			if (this.timeElapsed > 0f && (this.condition == CollisionCondition.Exit || (this.condition == CollisionCondition.Delay && this.timeElapsed < this.conditionTime)))
			{
				this.PrintCollision(this.collision);
			}
			this.timeElapsed = 0f;
			this.delayPrinted = false;
		}
	}

	private void PrintCollision(CollisionData collision)
	{
		base.Print(collision.position, collision.rotation, collision.surface, collision.layer, 1f, false);
	}

	private void OnDrawGizmos()
	{
		Transform transform = (!((Object)this.castPoint != (Object)null)) ? base.transform : this.castPoint;
		Quaternion rotation = transform.rotation * Quaternion.Euler(this.rotationOffset);
		Vector3 from = transform.position + rotation * this.positionOffset;
		Gizmos.color = Color.black;
		Gizmos.DrawRay(from, rotation * Vector3.up * 0.4f);
		Gizmos.color = Color.white;
		Gizmos.DrawRay(from, rotation * Vector3.forward * this.castLength);
	}
}
