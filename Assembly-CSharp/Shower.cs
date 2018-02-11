using UnityEngine;

public class Shower : MonoBehaviour
{
	public Game game;

	public RackCharacter pc;

	public bool showerOn;

	public ControlPanelMonitor controlPanel;

	public Vector3 trigger;

	public bool lastSlowmo;

	public bool slowmo;

	private float lastTriggerTime;

	public float vol;

	public bool sfxPlaying;

	private void Start()
	{
		this.controlPanel = base.GetComponentInChildren<ControlPanelMonitor>();
		this.trigger = base.transform.Find("Trigger").position;
	}

	private void OnEnable()
	{
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		if (this.showerOn)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
		}
		else
		{
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].Stop();
			}
		}
	}

	public bool toggleShower()
	{
		this.controlPanel.turnedOn = !this.controlPanel.turnedOn;
		if (this.controlPanel.turnedOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(Resources.Load("showeron") as AudioClip, 0.4f);
		}
		else
		{
			base.GetComponent<AudioSource>().PlayOneShot(Resources.Load("showeroff") as AudioClip, 0.4f);
		}
		this.lastTriggerTime = Time.time;
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
			if (this.pc != null && (this.trigger - this.pc.GO.transform.position).magnitude < 3f && Time.time - this.lastTriggerTime >= 5f)
			{
				this.game.context(Localization.getPhrase("TOGGLE_SHOWER", string.Empty), this.toggleShower, base.transform.position, false);
			}
		}
		if (this.showerOn)
		{
			this.vol += (1f - this.vol) * Time.deltaTime * 2f;
			if (!this.sfxPlaying)
			{
				base.GetComponent<AudioSource>().Play();
				this.sfxPlaying = true;
			}
		}
		else
		{
			this.vol += (0f - this.vol) * Time.deltaTime * 4f;
			if (this.vol < 0.05f && this.sfxPlaying)
			{
				base.GetComponent<AudioSource>().Stop();
				this.sfxPlaying = false;
			}
		}
		if (this.sfxPlaying)
		{
			base.GetComponent<AudioSource>().volume = this.vol;
		}
		if (this.showerOn != this.controlPanel.turnedOn)
		{
			this.showerOn = this.controlPanel.turnedOn;
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			if (this.showerOn)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Play();
				}
			}
			else
			{
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].Stop();
				}
			}
		}
		if (this.slowmo != this.lastSlowmo)
		{
			this.lastSlowmo = this.slowmo;
			ParticleSystem[] componentsInChildren2 = base.GetComponentsInChildren<ParticleSystem>();
			if (this.slowmo)
			{
				for (int k = 0; k < componentsInChildren2.Length; k++)
				{
					componentsInChildren2[k].playbackSpeed = 0.01f;
				}
			}
			else
			{
				for (int l = 0; l < componentsInChildren2.Length; l++)
				{
					componentsInChildren2[l].playbackSpeed = 1f;
				}
			}
		}
	}
}
