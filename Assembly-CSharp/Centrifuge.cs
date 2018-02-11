using UnityEngine;

public class Centrifuge : MonoBehaviour
{
	private bool spinning;

	private float spinSpeed;

	private bool sfxPlaying;

	private float vol;

	public static bool anythingActuallyProcessing;

	private Game game;

	private RackCharacter pc;

	private void Start()
	{
	}

	private bool toggle()
	{
		this.spinning = !this.spinning;
		if (this.spinning)
		{
			this.game.playSound("showeron", 1f, 1f);
		}
		else
		{
			base.GetComponent<AudioSource>().PlayOneShot(Resources.Load("showeroff") as AudioClip, 0.4f);
		}
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
			Centrifuge.anythingActuallyProcessing = false;
			bool flag = false;
			int num = Mathf.FloorToInt(Random.value * 6f);
			for (int i = 0; i < 6; i++)
			{
				if (Inventory.data.specimen[(i + num) % 6] > 0f)
				{
					float num2 = Time.deltaTime * 0.1f;
					if (num2 >= Inventory.data.specimen[(i + num) % 6])
					{
						num2 = Inventory.data.specimen[(i + num) % 6];
					}
					if (this.spinning)
					{
						Inventory.data.specimen[(i + num) % 6] -= num2;
						Inventory.data.totalSpecimen -= num2;
						Inventory.data.chemicals[(i + num) % 6] += num2 * 16f;
						Centrifuge.anythingActuallyProcessing = true;
					}
					flag = true;
					i = 6;
				}
			}
			if (flag)
			{
				if (!this.spinning)
				{
					this.pc = this.game.PC();
					if (this.pc != null && (base.transform.position - this.pc.GO.transform.position).magnitude < 6f)
					{
						this.game.context(Localization.getPhrase("PROCESS_SPECIMEN", string.Empty), this.toggle, base.transform.position, false);
					}
				}
			}
			else if (this.spinning)
			{
				Inventory.saveInventoryData();
				this.toggle();
			}
		}
		if (this.spinning)
		{
			this.vol += (0.12f - this.vol) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
			if (!this.sfxPlaying)
			{
				base.GetComponent<AudioSource>().Play();
				this.sfxPlaying = true;
			}
		}
		else
		{
			if (this.spinSpeed * 0.12f < this.vol)
			{
				this.vol = this.spinSpeed * 0.12f;
			}
			if (this.vol < 0.03f && this.sfxPlaying)
			{
				base.GetComponent<AudioSource>().Stop();
				this.sfxPlaying = false;
			}
		}
		if (this.sfxPlaying)
		{
			base.GetComponent<AudioSource>().volume = this.vol;
			base.GetComponent<AudioSource>().pitch = this.spinSpeed;
		}
		if (this.spinning)
		{
			if (this.spinSpeed < 1f)
			{
				this.spinSpeed += Time.deltaTime * 0.2f;
			}
			if (this.spinSpeed > 1f)
			{
				this.spinSpeed = 1f;
			}
		}
		else
		{
			if (this.spinSpeed > 0f)
			{
				this.spinSpeed -= Time.deltaTime * 0.1f;
			}
			if (this.spinSpeed < 0f)
			{
				this.spinSpeed = 0f;
			}
		}
		base.GetComponent<Animator>().speed = this.spinSpeed;
	}
}
