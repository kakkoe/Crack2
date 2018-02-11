using UnityEngine;

public class IndustrialRailing : MonoBehaviour
{
	public float activeAmount = 1f;

	public float lastActiveAmount;

	public Vector3 origPosition;

	public Vector3 v3 = default(Vector3);

	public bool hasBothArms = true;

	private void Start()
	{
		this.origPosition = base.transform.localPosition;
		this.lastActiveAmount = -0.01f;
		this.hasBothArms = ((Object)base.transform.Find("Arm0") != (Object)null && (Object)base.transform.Find("Arm1") != (Object)null);
	}

	private void FixedUpdate()
	{
		if (this.hasBothArms)
		{
			float num = this.activeAmount / 0.4f;
			if (num > 1f)
			{
				num = 1f;
			}
			this.v3.x = this.origPosition.x;
			this.v3.y = this.origPosition.y - (1f - num) * 2.95f;
			this.v3.z = this.origPosition.z;
			base.transform.localPosition = this.v3;
			float num2 = (this.activeAmount - 0.5f) / 0.5f;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			this.v3.x = -90f * num2;
			this.v3.y = 0f;
			this.v3.z = 0f;
			base.transform.Find("Arm0").localRotation = Quaternion.Euler(this.v3);
			this.v3.x = 180f + 90f * num2;
			this.v3.z = 180f;
			base.transform.Find("Arm1").localRotation = Quaternion.Euler(this.v3);
		}
	}
}
