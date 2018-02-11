using UnityEngine;

public class Objective
{
	public string description;

	public string type;

	public int source;

	public RackCharacter sourceCharacter;

	public float currentQuantity;

	public float targetQuantity;

	public float progress;

	public float completion;

	public bool completed;

	public bool wasCompleted;

	public bool bad;

	public bool secret;

	public bool dead;

	public Vector3 markerPosition;

	public bool hasMarker;
}
