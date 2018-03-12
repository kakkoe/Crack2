using UnityEngine;

public class Bridge : MonoBehaviour
{
	public Game game;

	public RackCharacter pc;

	public bool bridgeOpen;

	public Vector3 closedPosition;

	public Vector3 openPosition;

	public float vel;

	public IndustrialRailing[] railings;

	public float openDistance;

	public float extraOpen;

	public Light[] lights;

	public bool lastBridgeOpen;

	private int bridgeStatus;

	private void Start()
	{
		this.lights = base.GetComponentsInChildren<Light>();
		this.openPosition = base.transform.localPosition;
		this.closedPosition = base.transform.localPosition;
		this.openDistance = 28f;
		this.closedPosition.z += this.openDistance;
		base.transform.Translate(0f, 0f, 28f);
		this.railings = base.GetComponentsInChildren<IndustrialRailing>();
		for (int i = 0; i < this.railings.Length; i++)
		{
			this.railings[i].activeAmount = 0f;
			this.railings[i].lastActiveAmount = -0.01f;
		}
		this.updateRailings();
	}

	private void Update()
	{
		if ((Object)this.game == (Object)null)
		{
			this.game = Game.gameInstance;
			return;
		}
		this.bridgeOpen = false;
		this.pc = this.game.PC();
		if (this.pc != null && this.game.currentZone != null && (Object)this.pc.standingOnSurface != (Object)null)
		{
			if (Game.gameInstance.currentZone == "LabEntrance" || Game.gameInstance.currentZone == "LabTower" || Game.gameInstance.currentZone == "LabTowerLower" || this.pc.standingOnSurface.name == "Bridge_metal")
			{
				goto IL_0114;
			}
			if (Game.gameInstance.currentZone == "LabFloor")
			{
				Vector3 position = this.pc.GO.transform.position;
				if (position.y > 0f)
				{
					goto IL_0114;
				}
			}
		}
		goto IL_011b;
		IL_011b:
		if (this.bridgeOpen)
		{
			this.vel -= 0.1f * Time.deltaTime;
			if (this.vel < -3f)
			{
				this.vel = -3f;
			}
			base.transform.Translate(0f, 0f, this.vel * Time.deltaTime * 60f);
		}
		else if (this.extraOpen > 0f)
		{
			this.extraOpen -= 0.4f * Time.deltaTime;
		}
		else
		{
			this.extraOpen = 0f;
			this.vel += 0.1f * Time.deltaTime;
			if (this.vel > 3f)
			{
				this.vel = 3f;
			}
			base.transform.Translate(0f, 0f, this.vel * Time.deltaTime * 60f);
		}
		Vector3 localPosition = base.transform.localPosition;
		if (localPosition.z <= this.openPosition.z)
		{
			base.transform.localPosition = this.openPosition;
			this.vel = 0f;
			if (this.bridgeOpen)
			{
				this.extraOpen += (1f - this.extraOpen) * 0.4f * Time.smoothDeltaTime;
			}
		}
		Vector3 localPosition2 = base.transform.localPosition;
		if (localPosition2.z >= this.closedPosition.z)
		{
			base.transform.localPosition = this.closedPosition;
			this.vel = 0f;
		}
		for (int i = 0; i < this.lights.Length; i++)
		{
			this.lights[i].enabled = this.bridgeOpen;
		}
		this.updateRailings();
		if (this.game.currentZone == "LabEntrance" || this.game.currentZone == "LabTower" || this.game.currentZone == "LabTowerLower" || this.game.currentZone == "LabFloor")
		{
			((Component)base.transform.parent.Find("engineAudio")).GetComponent<AudioSource>().volume += (0.5f - ((Component)base.transform.parent.Find("engineAudio")).GetComponent<AudioSource>().volume) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
			((Component)base.transform.parent.Find("endAudio")).GetComponent<AudioSource>().volume += (0.5f - ((Component)base.transform.parent.Find("endAudio")).GetComponent<AudioSource>().volume) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
			((Component)base.transform.parent.Find("startAudio")).GetComponent<AudioSource>().volume += (0.5f - ((Component)base.transform.parent.Find("startAudio")).GetComponent<AudioSource>().volume) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
		}
		else
		{
			((Component)base.transform.parent.Find("engineAudio")).GetComponent<AudioSource>().volume += (0.05f - ((Component)base.transform.parent.Find("engineAudio")).GetComponent<AudioSource>().volume) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
			((Component)base.transform.parent.Find("endAudio")).GetComponent<AudioSource>().volume += (0.05f - ((Component)base.transform.parent.Find("endAudio")).GetComponent<AudioSource>().volume) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
			((Component)base.transform.parent.Find("startAudio")).GetComponent<AudioSource>().volume += (0.05f - ((Component)base.transform.parent.Find("startAudio")).GetComponent<AudioSource>().volume) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
		}
		Vector3 localPosition3 = base.transform.localPosition;
		float num = (0f - (localPosition3.z - this.closedPosition.z)) / this.openDistance;
		if (num <= 0f)
		{
			this.bridgeStatus = 0;
		}
		if (num >= 1f)
		{
			this.bridgeStatus = 2;
		}
		if (num > 0.01f && this.bridgeStatus == 0)
		{
			this.bridgeStatus = 1;
			((Component)base.transform.parent.Find("engineAudio")).GetComponent<AudioSource>().Play();
			((Component)base.transform.parent.Find("endAudio")).GetComponent<AudioSource>().Play();
		}
		if (num < 0.99f && this.bridgeStatus == 2)
		{
			this.bridgeStatus = 1;
			((Component)base.transform.parent.Find("engineAudio")).GetComponent<AudioSource>().Play();
			((Component)base.transform.parent.Find("startAudio")).GetComponent<AudioSource>().Play();
		}
		return;
		IL_0114:
		this.bridgeOpen = true;
		goto IL_011b;
	}

	private void updateRailings()
	{
		Vector3 localPosition = base.transform.localPosition;
		float num = (0f - (localPosition.z - this.closedPosition.z)) / this.openDistance;
		for (int i = 0; i < this.railings.Length; i++)
		{
			IndustrialRailing obj = this.railings[i];
			float num2 = num + this.extraOpen;
			Vector3 localPosition2 = this.railings[i].transform.localPosition;
			obj.activeAmount = (num2 + (-1.2f - localPosition2.z / this.openDistance)) * 4f;
			if (this.railings[i].activeAmount < 0f)
			{
				this.railings[i].activeAmount = 0f;
			}
			if (this.railings[i].activeAmount > 1f)
			{
				this.railings[i].activeAmount = 1f;
			}
		}
	}
}
