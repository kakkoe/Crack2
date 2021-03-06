using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
	public Game game;

	public RackCharacter pc;

	private void Start()
	{
	}

	private bool openLocker()
	{
		Game.gameInstance.inventoryOpen = true;
		Game.gameInstance.interactingWithBags = new List<string>();
		this.game.interactingWithBags.Add(Inventory.getBagByName("LOCKER_SHELF").uid);
		this.game.interactingWithBags.Add(Inventory.getBagByName("LOCKER").uid);
		Game.gameInstance.playSound("ui_contextopen", 1f, 1f);
		Game.gameInstance.playSound("ui_locker", 1f, 1f);
		return true;
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
			if (this.pc != null && (base.transform.position - this.pc.GO.transform.position).magnitude < 4f)
			{
				this.game.context(Localization.getPhrase("OPEN_LOCKER", string.Empty), this.openLocker, base.transform.position, false);
			}
		}
	}
}
