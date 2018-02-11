using UnityEngine;

public class MainGarageDoor : MonoBehaviour
{
	private Light light0;

	private Light light1;

	private Light light2;

	private float doorOpenAmount;

	private bool doorOpen;

	private float doorVel;

	private int doorTransitionStatus;

	private float lightDelay;

	private void Start()
	{
		this.light0 = ((Component)base.transform.Find("light0")).GetComponentInChildren<Light>();
		this.light1 = ((Component)base.transform.Find("light1")).GetComponentInChildren<Light>();
		this.light2 = ((Component)base.transform.Find("light2")).GetComponentInChildren<Light>();
		((Component)base.transform.Find("light0")).GetComponentInChildren<Light>().enabled = false;
		((Component)base.transform.Find("light1")).GetComponentInChildren<Light>().enabled = false;
		((Component)base.transform.Find("light2")).GetComponentInChildren<Light>().enabled = true;
	}

	public bool toggleDoor()
	{
		this.doorOpen = !this.doorOpen;
		this.doorTransitionStatus = 1;
		((Component)base.transform.Find("audio")).GetComponent<AudioSource>().Play();
		return base.transform;
	}

	private void Update()
	{
		base.GetComponent<Animator>().Play("garagedoor", 0, this.doorOpenAmount * 0.99f);
		if (this.doorTransitionStatus == 1)
		{
			if (this.doorOpen)
			{
				this.doorVel += Time.deltaTime * 0.075f;
			}
			else
			{
				this.doorVel -= Time.deltaTime * 0.075f;
			}
			this.doorOpenAmount += this.doorVel * Time.deltaTime;
			if (this.doorOpenAmount >= 1f)
			{
				this.doorOpenAmount = 1f;
				this.doorVel = 0f;
				this.doorTransitionStatus = 2;
			}
			if (this.doorOpenAmount <= 0f)
			{
				this.doorOpenAmount = 0f;
				this.doorVel = 0f;
				this.doorTransitionStatus = 0;
			}
			this.lightDelay = 1f;
		}
		else if ((Object)Game.gameInstance != (Object)null && Game.gameInstance.PC() != null)
		{
			if (this.doorOpen && (base.transform.Find("Trigger").position - Game.gameInstance.PC().GO.transform.position).magnitude > 40f)
			{
				this.toggleDoor();
			}
			if ((base.transform.Find("Trigger").position - Game.gameInstance.PC().GO.transform.position).magnitude < 9f)
			{
				if (this.doorOpen)
				{
					Game.gameInstance.context(Localization.getPhrase("CLOSE_GARAGE_DOOR", string.Empty), this.toggleDoor, base.transform.position, false);
				}
				else
				{
					Game.gameInstance.context(Localization.getPhrase("OPEN_GARAGE_DOOR", string.Empty), this.toggleDoor, base.transform.position, false);
				}
			}
		}
		if (this.doorTransitionStatus != 1 && this.lightDelay > -0.5f)
		{
			this.lightDelay -= Time.deltaTime;
			if (this.lightDelay < 0.5f)
			{
				if (this.light0.enabled != (this.doorTransitionStatus == 2))
				{
					((Component)this.light0).GetComponent<AudioSource>().Play();
				}
				this.light0.enabled = (this.doorTransitionStatus == 2);
			}
			if (this.lightDelay <= 0f)
			{
				if (this.light1.enabled != (this.doorTransitionStatus == 2))
				{
					((Component)this.light1).GetComponent<AudioSource>().Play();
				}
				this.light1.enabled = (this.doorTransitionStatus == 2);
			}
			if (this.lightDelay <= -0.5f)
			{
				if (this.light2.enabled != (this.doorTransitionStatus == 0))
				{
					((Component)this.light2).GetComponent<AudioSource>().Play();
				}
				this.light2.enabled = (this.doorTransitionStatus == 0);
			}
		}
	}
}
