using UnityEngine;

public class HairAddonColorMenu : MonoBehaviour
{
	public int id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void hairstyleDelete()
	{
		Game.gameInstance.deleteHairPiece(base.transform.parent.gameObject);
	}

	public void hairstyleChanged()
	{
		Game.gameInstance.updateHairPiece(base.transform.parent.gameObject);
	}

	public void hairColorClicked(int c)
	{
		Game.gameInstance.hairAddonColorClicked(base.transform.parent.gameObject, c);
	}
}
