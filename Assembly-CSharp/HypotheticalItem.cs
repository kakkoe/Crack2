using UnityEngine;

public class HypotheticalItem : MonoBehaviour
{
	public int touchingThings;

	public void OnTriggerEnter(Collider otherThing)
	{
		if (!otherThing.isTrigger)
		{
			this.touchingThings++;
		}
	}

	public void OnTriggerExit(Collider otherThing)
	{
		if (!otherThing.isTrigger)
		{
			this.touchingThings--;
		}
	}
}
