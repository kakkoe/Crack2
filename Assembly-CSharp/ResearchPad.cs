using UnityEngine;

public class ResearchPad : MonoBehaviour
{
	private Game game;

	private RackCharacter pc;

	private float lastTriggerTime;

	private void Start()
	{
	}

	public bool openResearch()
	{
		this.game.openResearch();
		return false;
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
			if (this.pc != null && (base.transform.position - this.pc.GO.transform.position).magnitude < 5f && Time.time - this.lastTriggerTime >= 5f)
			{
				this.game.context(Localization.getPhrase("OPEN_RESEARCH_INTERFACE", string.Empty), this.openResearch, base.transform.position, false);
			}
		}
	}
}
