using UnityEngine;

public class AutomaticSlidingDoor : MonoBehaviour
{
	private Game game;

	private Vector3 trigger;

	private Transform door;

	private Vector3 closedDoorPosition;

	private Vector3 openDoorPosition;

	private static float triggerDistance = 10f;

	private float transitionVel;

	private Vector3 targetPos;

	public RackCharacter pc;

	public bool doorOpen;

	public bool lastDoorOpen;

	public bool unlockedDoor;

	private void Start()
	{
		this.trigger = base.transform.Find("Trigger").position;
		this.door = base.transform.Find("Pane");
		this.closedDoorPosition = this.door.localPosition;
		this.openDoorPosition = this.closedDoorPosition;
		this.openDoorPosition.x = -5f;
		this.lastDoorOpen = false;
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
			this.doorOpen = false;
			if (this.pc != null && this.pc.initted)
			{
				float magnitude = (this.pc.GO.transform.position - this.trigger).magnitude;
				this.doorOpen = (magnitude < AutomaticSlidingDoor.triggerDistance && (Inventory.getCharVar("startingStuffGiven") == 1f || this.unlockedDoor));
				if (NPC.requisitionsOfficer != null && NPC.requisitionsOfficer.initted)
				{
					this.doorOpen = (this.doorOpen || (NPC.requisitionsOfficer.GO.transform.position - this.trigger).magnitude < AutomaticSlidingDoor.triggerDistance);
				}
				((Component)this.door).GetComponent<BoxCollider>().enabled = (Inventory.getCharVar("startingStuffGiven") == 0f && !this.unlockedDoor);
			}
			if (this.doorOpen)
			{
				this.targetPos = this.openDoorPosition;
			}
			else
			{
				this.targetPos = this.closedDoorPosition;
			}
			this.transitionVel += ((this.targetPos - this.door.localPosition).magnitude - this.transitionVel) * 0.4f * Time.smoothDeltaTime;
			if (this.transitionVel > 1f)
			{
				this.transitionVel = 1f;
			}
			Transform transform = this.door;
			transform.localPosition += (this.targetPos - this.door.localPosition) * this.transitionVel * Time.smoothDeltaTime * 5.8f;
			if (this.doorOpen != this.lastDoorOpen)
			{
				this.lastDoorOpen = this.doorOpen;
				if (this.doorOpen)
				{
					if ((Object)base.transform.Find("openAudio") != (Object)null)
					{
						((Component)base.transform.Find("openAudio")).GetComponent<AudioSource>().Play();
					}
				}
				else if ((Object)base.transform.Find("closeAudio") != (Object)null)
				{
					((Component)base.transform.Find("closeAudio")).GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}
