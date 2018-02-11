using UnityEngine;

public class ChemGunSelection : MonoBehaviour
{
	public string lastSelectedCompound = string.Empty;

	private void Start()
	{
	}

	private void Update()
	{
		if (UserSettings.data.selectedChemicalCompound != this.lastSelectedCompound)
		{
			base.GetComponent<RawTextureLoader>().tex = "chemicalbranding" + Game.PathDirectorySeparatorChar + UserSettings.data.selectedChemicalCompound.Split('.')[1];
			this.lastSelectedCompound = UserSettings.data.selectedChemicalCompound;
		}
	}
}
