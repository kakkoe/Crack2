using UnityEngine;

public class Sink : MonoBehaviour
{
	public Game game;

	public RackCharacter pc;

	private void Start()
	{
	}

	private bool openCharacterCustomization()
	{
		this.game.customizeCharacter();
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
			if (this.pc != null && (base.transform.position - this.pc.GO.transform.position).magnitude < 12f)
			{
				this.game.context(Localization.getPhrase("CUSTOMIZE_YOUR_CHARACTER", string.Empty), this.openCharacterCustomization, base.transform.position, false);
			}
		}
	}
}
