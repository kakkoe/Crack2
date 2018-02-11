using UnityEngine;

public class FrontDoorDetector : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name == "MovementTarget")
		{
			Game.gameInstance.popup("NOTHING_TO_DO_OUTSIDE_YET", false, false);
		}
	}
}
