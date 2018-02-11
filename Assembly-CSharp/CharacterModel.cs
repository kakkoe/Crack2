using UnityEngine;

public class CharacterModel : MonoBehaviour
{
	public RackCharacter owner;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Footstep()
	{
		if (this.owner != null)
		{
			this.owner.footstep(0);
		}
	}

	public void Footstep2()
	{
		if (this.owner != null)
		{
			this.owner.footstep(1);
		}
	}
}
