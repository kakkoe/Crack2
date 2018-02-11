using UnityEngine;

public class LabDoor : MonoBehaviour
{
	private Game game;

	private Vector3 trigger;

	private Transform door;

	private Vector3 closedDoorPosition;

	private Vector3 openDoorPosition;

	private static float triggerDistance = 4f;

	private float transitionVel;

	private Vector3 targetPos;

	public RackCharacter pc;

	public bool doorOpen;

	private bool lastDoorOpen;

	private float closeDelay;

	private void Start()
	{
		this.trigger = base.transform.position;
		this.door = base.transform.Find("Door");
		this.closedDoorPosition = this.door.localPosition;
		this.openDoorPosition = this.closedDoorPosition;
		this.openDoorPosition.y = 7f;
	}

	private bool openDoor()
	{
		this.doorOpen = true;
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
			bool flag = false;
			this.pc = this.game.PC();
			if (this.pc != null)
			{
				float magnitude = (this.pc.GO.transform.position - this.trigger).magnitude;
				flag = (magnitude < LabDoor.triggerDistance);
			}
			if (flag && !this.doorOpen)
			{
				this.game.context(Localization.getPhrase("OPEN_DOOR", string.Empty), this.openDoor, base.transform.position, false);
			}
			if (this.doorOpen)
			{
				if (flag)
				{
					this.closeDelay = 1.5f;
				}
				else
				{
					this.closeDelay -= Time.deltaTime;
					if (this.closeDelay <= 0f)
					{
						this.doorOpen = false;
					}
				}
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
