using UnityEngine;

public class StepLift : MonoBehaviour
{
	private bool lifting;

	private float liftAmount;

	public RackCharacter owner;

	private bool initted;

	private bool GOwasOn = true;

	private Vector3 lastKnownPosition;

	private Transform platform;

	private Transform lift;

	private GameObject platformGO;

	private GameObject liftGO;

	private void Start()
	{
		if (!this.initted)
		{
			this.lift = base.transform.Find("gfx").Find("Lift");
			this.platform = base.transform.Find("gfx").Find("Platform");
			this.liftGO = this.lift.gameObject;
			this.platformGO = this.platform.gameObject;
			this.initted = true;
		}
	}

	public void useLift(float amount)
	{
		this.lifting = (amount > 0f);
		if (this.lifting)
		{
			this.liftAmount = amount;
		}
	}

	public void process()
	{
		if (this.initted)
		{
			if (this.liftAmount == 0f)
			{
				if (this.GOwasOn)
				{
					this.platformGO.SetActive(false);
					this.liftGO.SetActive(false);
					this.GOwasOn = false;
				}
			}
			else
			{
				if (!this.GOwasOn)
				{
					this.platformGO.SetActive(true);
					this.liftGO.SetActive(true);
					this.GOwasOn = true;
				}
				if (this.lifting)
				{
					this.lift.localScale = Vector3.one * this.owner.height_act;
					this.platform.localScale = Vector3.one * this.owner.height_act;
					this.lift.rotation = this.owner.GO.transform.rotation;
					this.platform.rotation = this.lift.rotation;
					this.lift.position = this.owner.GO.transform.position;
				}
				else
				{
					this.liftAmount -= Time.deltaTime;
					if (this.liftAmount < 0f)
					{
						this.liftAmount = 0f;
					}
					this.lift.position = this.lastKnownPosition + Vector3.up * this.liftAmount;
				}
				this.platform.position = this.lift.position + Vector3.down * Game.cap(this.liftAmount, 0.15f, 99f);
				if (this.lifting)
				{
					this.lastKnownPosition = this.platform.position;
				}
			}
			this.lifting = false;
		}
	}
}
