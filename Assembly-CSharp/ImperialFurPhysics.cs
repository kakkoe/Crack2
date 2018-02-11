using UnityEngine;

public class ImperialFurPhysics : MonoBehaviour
{
	public bool useRigidbody = true;

	public bool usePhysicsGravity = true;

	public bool physicsEnabled = true;

	public bool windEnabled;

	public Vector3 AdditionalGravity;

	public float forceScale = 1f;

	public float gravityScale = 0.25f;

	public float forceDamping = 3f;

	public bool rack2CharacterHack;

	private Material material;

	private Rigidbody rigidBody;

	private Transform thisTransform;

	private Vector3 oldPosition;

	private Vector3 forceSmooth = Vector3.zero;

	private void Start()
	{
		this.rigidBody = base.gameObject.GetComponent<Rigidbody>();
		this.material = base.gameObject.GetComponent<Renderer>().material;
		this.thisTransform = base.transform;
		this.oldPosition = this.thisTransform.position;
		if ((Object)this.rigidBody == (Object)null && this.useRigidbody)
		{
			Debug.LogWarning("No Rigidbody attached to fur object. Defaulting to non-Rigidbody simulation");
			this.useRigidbody = false;
		}
	}

	private void Update()
	{
		if (!this.useRigidbody)
		{
			Vector3 force = Vector3.zero;
			if (this.physicsEnabled && !this.useRigidbody)
			{
				Vector3 a = this.oldPosition - this.thisTransform.position;
				force = a / Time.deltaTime;
				this.oldPosition = this.thisTransform.position;
				force *= this.forceScale;
			}
			this.CalculateAdditionalForce(force);
		}
	}

	private void FixedUpdate()
	{
		if (this.useRigidbody)
		{
			Vector3 force = Vector3.zero;
			if (this.physicsEnabled)
			{
				force = -this.rigidBody.velocity;
				force *= this.forceScale;
			}
			this.CalculateAdditionalForce(force);
		}
	}

	private void CalculateAdditionalForce(Vector3 force)
	{
		if (this.usePhysicsGravity)
		{
			force += Physics.gravity * this.gravityScale;
		}
		force += this.AdditionalGravity;
		if (this.windEnabled)
		{
			force += ImperialFurWind.windForce;
		}
		force = Vector3.ClampMagnitude(force, 1f);
		this.forceSmooth = Vector3.Lerp(this.forceSmooth, force, Time.deltaTime * this.forceDamping);
		if (this.rack2CharacterHack)
		{
			this.material.SetVector("Displacement", this.forceSmooth);
		}
		else
		{
			this.material.SetVector("Displacement", base.transform.InverseTransformDirection(this.forceSmooth));
		}
	}

	public void UpdatePhysics()
	{
		if (this.rack2CharacterHack)
		{
			this.material.SetVector("Displacement", this.forceSmooth);
		}
		else
		{
			this.material.SetVector("Displacement", base.transform.InverseTransformDirection(this.forceSmooth));
		}
	}

	public void UpdateMaterial()
	{
		this.material = base.gameObject.GetComponent<Renderer>().material;
	}
}
