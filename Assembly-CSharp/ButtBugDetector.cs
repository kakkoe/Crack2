using UnityEngine;

public class ButtBugDetector : MonoBehaviour
{
	public RackCharacter owner;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.owner != null && this.owner.initted && collision.collider.name == "Butt_L")
		{
			this.owner.preparePhysics();
		}
	}
}
