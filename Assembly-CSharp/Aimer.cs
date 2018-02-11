using UnityEngine;

public class Aimer : MonoBehaviour
{
	public Transform target;

	private void Start()
	{
	}

	private void Update()
	{
		if ((Object)this.target != (Object)null)
		{
			base.transform.LookAt(this.target);
		}
	}
}
