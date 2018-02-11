using UnityEngine;

public class WallForce : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionStay(Collision col)
	{
		Vector3 a = base.transform.up + new Vector3(0.1f, 0.1f, 0.1f);
		col.rigidbody.AddForce(a * 125f);
	}
}
