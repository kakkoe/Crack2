using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CollisionPrinter : Printer
{
	public RotationSource rotationSource;

	public CollisionCondition condition;

	public float conditionTime;

	public LayerMask layers;

	private float timeElapsed;

	private bool delayPrinted;

	private void OnCollisionEnter(Collision collision)
	{
		if (this.condition == CollisionCondition.Enter || this.condition == CollisionCondition.Constant)
		{
			this.PrintCollision(collision);
		}
		this.timeElapsed = 0f;
		this.delayPrinted = false;
	}

	private void OnCollisionStay(Collision collision)
	{
		this.timeElapsed += Time.deltaTime;
		if (this.condition == CollisionCondition.Constant)
		{
			this.PrintCollision(collision);
		}
		if (this.condition == CollisionCondition.Delay && this.timeElapsed > this.conditionTime && !this.delayPrinted)
		{
			this.PrintCollision(collision);
			this.delayPrinted = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (this.condition == CollisionCondition.Exit)
		{
			this.PrintCollision(collision);
		}
		if (this.condition == CollisionCondition.Delay && !this.delayPrinted)
		{
			this.PrintCollision(collision);
		}
	}

	public void PrintCollision(Collision collision)
	{
		Transform surface = null;
		Vector3 a = Vector3.zero;
		Vector3 a2 = Vector3.zero;
		int num = 0;
		ContactPoint[] contacts = collision.contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			ContactPoint contactPoint = contacts[i];
			if (this.layers.value == (this.layers.value | 1 << contactPoint.otherCollider.gameObject.layer))
			{
				num++;
				if (num == 1)
				{
					surface = contactPoint.otherCollider.transform;
				}
				if (num == 1)
				{
					a = contactPoint.point;
				}
				if (num == 1)
				{
					a2 = contactPoint.normal;
				}
			}
		}
		if (num > 0)
		{
			RaycastHit raycastHit = default(RaycastHit);
			if (Physics.Raycast(a + a2 * 0.4f, -a2, out raycastHit, 0.8f, this.layers.value))
			{
				a = raycastHit.point;
				a2 = raycastHit.normal;
				Vector3 upwards = (this.rotationSource != 0 || !(base.GetComponent<Rigidbody>().velocity != Vector3.zero)) ? ((this.rotationSource != RotationSource.Random) ? Vector3.up : Random.insideUnitSphere.normalized) : base.GetComponent<Rigidbody>().velocity.normalized;
				base.Print(a, Quaternion.LookRotation(-a2, upwards), surface, raycastHit.collider.gameObject.layer, 1f, false);
			}
			else
			{
				Debug.Log("Bounce!");
				Debug.DrawRay(a + a2 * 0.25f, -a2, Color.red, float.PositiveInfinity);
			}
		}
	}
}
