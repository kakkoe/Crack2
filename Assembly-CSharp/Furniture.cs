using UnityEngine;

public class Furniture : MonoBehaviour
{
	public bool occupied;

	public RackCharacter occupant;

	public string useString;

	public string unuseString;

	public string animName;

	public float smallCutoffAmount = 0.9f;

	public float smallForwardAmount;

	public float usePromptRange = 5f;

	public bool instantUse;

	private Vector3 originalRoot;

	public bool scalesToFitOccupant;

	public bool playerCanUse;

	private Vector3 v3;

	private void Start()
	{
		this.originalRoot = base.transform.Find("Root").transform.localPosition;
	}

	public bool useFurniture()
	{
		if (this.instantUse)
		{
			this.attachPCtoFurniture();
		}
		else
		{
			RackCharacter rackCharacter = Game.gameInstance.PC();
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			float z = position3.z;
			Vector3 eulerAngles = base.transform.eulerAngles;
			rackCharacter.autoWalk(x, y, z, 0f, eulerAngles.y, 0f, this.attachPCtoFurniture, 999f);
		}
		return true;
	}

	public void attachPCtoFurniture()
	{
		Game.gameInstance.PC().useFurniture(this);
	}

	public bool unuseFurniture()
	{
		Game.gameInstance.PC().leaveFurniture();
		return true;
	}

	private void Update()
	{
		if ((Object)Game.gameInstance != (Object)null && this.playerCanUse && Game.gameInstance.PC() != null && (Game.gameInstance.PC().GO.transform.position - base.transform.position).magnitude < this.usePromptRange)
		{
			if (!this.occupied)
			{
				Game.gameInstance.context(Localization.getPhrase(this.useString, string.Empty), this.useFurniture, base.transform.position, false);
			}
			else if (this.occupant.uid == Game.gameInstance.PC().uid)
			{
				Game.gameInstance.context(Localization.getPhrase(this.unuseString, string.Empty), this.unuseFurniture, base.transform.position, true);
			}
		}
	}

	private void FixedUpdate()
	{
		if ((Object)Game.gameInstance != (Object)null)
		{
			if (this.occupant != null && (Object)this.occupant.GO == (Object)null)
			{
				this.occupied = false;
				this.occupant = null;
			}
			if (this.occupied)
			{
				this.v3 = this.originalRoot;
				float num = this.smallCutoffAmount;
				Vector3 localScale = this.occupant.GO.transform.localScale;
				float num2 = Game.cap((num - localScale.x) / this.smallCutoffAmount, 0f, 1f);
				this.v3.z += this.smallForwardAmount * num2;
				base.transform.Find("Root").localPosition = this.v3;
			}
		}
	}
}
