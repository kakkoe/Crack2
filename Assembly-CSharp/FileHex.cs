using UnityEngine;

public class FileHex : MonoBehaviour
{
	public string filename;

	public string shortname;

	public int id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void clicked()
	{
		Game.gameInstance.selectedImportFile = this.id;
	}

	public void hover()
	{
		Game.gameInstance.hoverImportFile = this.id;
	}

	public void unHover()
	{
		Game.gameInstance.hoverImportFile = -1;
	}
}
