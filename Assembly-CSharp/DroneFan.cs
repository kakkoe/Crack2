using UnityEngine;

public class DroneFan : MonoBehaviour
{
	public float speed = 100f;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Rotate(0f, Time.deltaTime * this.speed, 0f);
	}
}
