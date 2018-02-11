using UnityEngine;

public class PenisDetector : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name.IndexOf("Penis") != -1)
		{
			Debug.Log("Misplaced penis detected! " + Mathf.RoundToInt(Random.value * 10000f));
		}
	}
}
