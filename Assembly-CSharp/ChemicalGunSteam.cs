using UnityEngine;

public class ChemicalGunSteam : MonoBehaviour
{
	private ParticleSystem pSystem;

	private ParticleSystem.EmissionModule emit;

	private ParticleSystem.MainModule module;

	private float firstBurst;

	private void Start()
	{
		this.pSystem = base.GetComponent<ParticleSystem>();
		this.module = this.pSystem.main;
		this.emit = this.pSystem.emission;
	}

	private void Update()
	{
		this.emit.enabled = (Interaction.chemicalInjectionRate > 0f);
		if (this.emit.enabled)
		{
			this.module.startSpeedMultiplier = 0.5f + Interaction.chemicalInjectionRate * 4f + this.firstBurst * 3f;
			this.firstBurst -= Time.deltaTime;
			if (this.firstBurst < 0f)
			{
				this.firstBurst = 0f;
				this.emit.rateOverTime = 50f;
			}
			else
			{
				this.emit.rateOverTime = 200f;
			}
		}
		else
		{
			this.firstBurst = 0.5f;
		}
	}
}
