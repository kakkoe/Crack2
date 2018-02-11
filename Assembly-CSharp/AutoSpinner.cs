using UnityEngine;

public class AutoSpinner : MonoBehaviour
{
	public bool spinX;

	public bool spinY = true;

	public bool spinZ;

	public float speed = 1f;

	private Vector3 v3;

	private void Start()
	{
		this.v3 = Vector3.zero;
		if (this.spinX)
		{
			this.v3.x = 18.57f;
		}
		if (this.spinY)
		{
			this.v3.y = 25f;
		}
		if (this.spinZ)
		{
			this.v3.z = 22.37f;
		}
	}

	private void Update()
	{
		base.transform.Rotate(this.v3 * Time.deltaTime * this.speed);
	}
}
