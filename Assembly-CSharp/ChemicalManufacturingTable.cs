using UnityEngine;

public class ChemicalManufacturingTable : MonoBehaviour
{
	private Game game;

	private RackCharacter pc;

	private GameObject gfx;

	private float lastTriggerTime;

	private void Start()
	{
		this.gfx = base.transform.Find("gfx").gameObject;
	}

	public bool manufactureChemicals()
	{
		Game.gameInstance.openChemicalSynthesis();
		return false;
	}

	private void OnEnable()
	{
		if (!((Object)this.gfx == (Object)null))
		{
			this.gfx.SetActive(Inventory.data.completedResearch.Contains("ChemicalSynthesisBay"));
		}
	}

	private void Update()
	{
		if ((Object)this.game == (Object)null)
		{
			this.game = Game.gameInstance;
		}
		else
		{
			this.pc = this.game.PC();
			if (this.pc != null && this.gfx.activeInHierarchy && (base.transform.position - this.pc.GO.transform.position).magnitude < 6f && Time.time - this.lastTriggerTime >= 5f)
			{
				this.game.context(Localization.getPhrase("MANUFACTURE_CHEMICALS", string.Empty), this.manufactureChemicals, base.transform.position, false);
			}
		}
	}
}
