using UnityEngine;

public class BagTile : MonoBehaviour
{
	public Bag bag;

	public BagUI container;

	public int bagContentID = -1;

	public int xx;

	public int yy;

	public bool[] connections = new bool[6];

	private void Start()
	{
	}

	private void Update()
	{
	}
}
