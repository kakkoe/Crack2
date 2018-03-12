using UnityEngine;

public class Elevator : MonoBehaviour
{
	public Game game;

	public RackCharacter pc;

	public Transform platform;

	public bool elevatorHover;

	public bool elevatorUp;

	public BoxCollider floorCollider;

	public Vector3 v3 = default(Vector3);

	public bool elevatorMoving;

	public float vel;

	public float shaftHeight = 15.8f;

	public Transform fence;

	public float fenceScale = 1f;

	private bool audioOn;

	private float audioVol;

	private void Start()
	{
		this.platform = base.transform.Find("Platform");
		this.floorCollider = ((Component)this.platform).GetComponent<BoxCollider>();
		this.fence = this.platform.Find("Fence");
	}

	public void Update()
	{
		if (this.elevatorHover && !this.elevatorMoving)
		{
			this.game.context(Localization.getPhrase("USE_ELEVATOR", string.Empty), this.activateElevator, this.platform.position, false);
		}
		this.elevatorHover = false;
		if ((Object)this.game == (Object)null)
		{
			this.game = Game.gameInstance;
		}
		else
		{
			this.pc = this.game.PC();
			if (this.pc != null)
			{
				Vector3 position = this.pc.GO.transform.position;
				float y = position.y;
				Vector3 position2 = this.platform.position;
				if (Mathf.Abs(y - position2.y) < 7f)
				{
					float magnitude = (this.pc.GO.transform.position - this.platform.position).magnitude;
					this.v3.x = (this.v3.z = 0f);
					float num = (magnitude - 2.7f) * 0.3f;
					if (num < 0f)
					{
						num = 0f;
					}
					if (num > 2f)
					{
						num = 2f;
					}
					this.v3.y = -9.477409f - num;
					this.floorCollider.center = this.v3;
					this.v3.y = -30f * num;
					this.fence.localPosition = this.v3;
					this.elevatorHover = (num == 0f);
				}
				else
				{
					if (this.elevatorUp)
					{
						Vector3 position3 = this.pc.GO.transform.position;
						float y2 = position3.y;
						Vector3 position4 = this.platform.position;
						if (y2 < position4.y - this.shaftHeight / 2f)
						{
							this.elevatorUp = false;
						}
					}
					if (!this.elevatorUp)
					{
						Vector3 position5 = this.pc.GO.transform.position;
						float y3 = position5.y;
						Vector3 position6 = this.platform.position;
						if (y3 > position6.y + this.shaftHeight / 2f)
						{
							this.elevatorUp = true;
						}
					}
				}
			}
		}
		this.elevatorMoving = false;
		if (this.elevatorUp)
		{
			Vector3 localPosition = this.platform.localPosition;
			if (localPosition.y < this.shaftHeight)
			{
				this.vel += 1.5f * Time.smoothDeltaTime;
				if (this.vel > 1f)
				{
					this.vel = 1f;
				}
			}
		}
		else
		{
			Vector3 localPosition2 = this.platform.localPosition;
			if (localPosition2.y > 0.25f)
			{
				this.vel -= 1.5f * Time.smoothDeltaTime;
				if (this.vel < -1f)
				{
					this.vel = -1f;
				}
			}
		}
		this.platform.Translate(0f, this.vel * Time.smoothDeltaTime * 6f, 0f);
		Vector3 localPosition3 = this.platform.localPosition;
		if (localPosition3.y >= this.shaftHeight)
		{
			Transform transform = this.platform;
			float num2 = this.shaftHeight;
			Vector3 localPosition4 = this.platform.localPosition;
			transform.Translate(0f, num2 - localPosition4.y, 0f);
			this.vel = 0f;
		}
		else if (this.elevatorUp)
		{
			this.elevatorMoving = true;
		}
		Vector3 localPosition5 = this.platform.localPosition;
		if (localPosition5.y <= 0.25f)
		{
			Transform transform2 = this.platform;
			Vector3 localPosition6 = this.platform.localPosition;
			transform2.Translate(0f, 0.25f - localPosition6.y, 0f);
			this.vel = 0f;
		}
		else if (!this.elevatorUp)
		{
			this.elevatorMoving = true;
		}
		if (this.elevatorMoving)
		{
			this.fenceScale += (0.5f - this.fenceScale) * 0.2f;
			this.fence.gameObject.SetActive(true);
			if (!this.audioOn)
			{
				((Component)base.transform.Find("audio")).GetComponent<AudioSource>().Play();
				this.audioOn = true;
				((Component)base.transform.Find("audio")).GetComponent<AudioSource>().PlayOneShot(Resources.Load("elevatorstart") as AudioClip);
			}
			this.audioVol += (1f - this.audioVol) * Game.cap(Time.deltaTime * 5f, 0f, 1f);
			((Component)base.transform.Find("audio")).GetComponent<AudioSource>().volume = this.audioVol;
		}
		else
		{
			this.fenceScale = 2f;
			this.fence.gameObject.SetActive(false);
			if (this.audioOn)
			{
				((Component)base.transform.Find("audio")).GetComponent<AudioSource>().Stop();
				((Component)base.transform.Find("audio")).GetComponent<AudioSource>().PlayOneShot(Resources.Load("elevatorstop") as AudioClip);
				this.audioOn = false;
				this.audioVol = 0f;
				if (NPC.tourProgress == 4)
				{
					NPC.requisitionsOfficer.npcData.openDialogue();
				}
			}
		}
		this.v3.x = (this.v3.z = this.fenceScale);
		this.v3.y = 1f;
		this.fence.localScale = this.v3;
	}

	private bool activateElevator()
	{
		this.elevatorUp = !this.elevatorUp;
		return true;
	}
}
