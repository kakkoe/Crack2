using UnityEngine;

public class PlayerScreenFX : MonoBehaviour
{
	private GameObject pleasureEffect;

	private GameObject pleasureParticles;

	private ParticleSystem pleasureParticleSystem;

	private GameObject overstimEffect;

	private GameObject overstimParticles;

	private ParticleSystem overstimParticleSystem;

	private GameObject painEffect;

	private GameObject painParticles;

	private ParticleSystem painParticleSystem;

	private Color pColor;

	private Color ovColor;

	private Color paColor;

	private Color oColor;

	private float org;

	private float ov;

	private float pa;

	private CanvasRenderer pleasureEffectCR;

	private CanvasRenderer overstimEffectCR;

	private CanvasRenderer painEffectCR;

	private ParticleSystem.MainModule pleasureMain;

	private ParticleSystem.MainModule overstimMain;

	private ParticleSystem.MainModule painMain;

	private float f;

	private void Start()
	{
		this.pleasureEffect = base.transform.Find("pleasure").gameObject;
		this.pleasureParticles = base.transform.Find("pleasureParticles").gameObject;
		this.pleasureParticleSystem = this.pleasureParticles.GetComponent<ParticleSystem>();
		this.overstimEffect = base.transform.Find("overstim").gameObject;
		this.overstimParticles = base.transform.Find("overstimParticles").gameObject;
		this.overstimParticleSystem = this.overstimParticles.GetComponent<ParticleSystem>();
		this.painEffect = base.transform.Find("pain").gameObject;
		this.painParticles = base.transform.Find("painParticles").gameObject;
		this.painParticleSystem = this.painParticles.GetComponent<ParticleSystem>();
		this.pColor = new Color(1f, 0.8392157f, 0.796078444f, 0f);
		this.ovColor = Color.white;
		this.paColor = new Color(1f, 0.7254902f, 0f, 0f);
		this.oColor = Color.white;
		this.pleasureMain = this.pleasureParticleSystem.main;
		this.overstimMain = this.overstimParticleSystem.main;
		this.painMain = this.painParticleSystem.main;
		this.pleasureEffectCR = this.pleasureEffect.GetComponent<CanvasRenderer>();
		this.overstimEffectCR = this.overstimEffect.GetComponent<CanvasRenderer>();
		this.painEffectCR = this.painEffect.GetComponent<CanvasRenderer>();
	}

	private void Update()
	{
		if ((Object)Game.gameInstance != (Object)null && Game.gameInstance.PC() != null)
		{
			this.org += (Game.gameInstance.PC().orgasming - this.org) * Game.cap(Time.deltaTime, 0f, 1f);
			this.f = Mathf.Pow((Game.gameInstance.PC().proximityToOrgasm + Game.cap(this.org / Game.gameInstance.PC().currentOrgasmDuration, 0f, 1f)) * 0.5f, 2f);
			this.pleasureEffect.SetActive(this.f > 0.01f);
			this.pleasureParticles.SetActive(this.f > 0.01f);
			if (this.f > 0.01f)
			{
				this.pColor.a = this.f * 0.5f + Game.gameInstance.PC().orgasmSoftPulse * 0.2f;
				if (Game.gameInstance.PC().orgasming <= 0f)
				{
					this.pColor.a += Mathf.Pow(Game.gameInstance.PC().proximityToOrgasm, 7f) * Game.gameInstance.PC().breath[0] * 0.5f;
				}
				this.pleasureEffectCR.SetColor(this.pColor);
				this.pleasureMain.startSize = this.pColor.a * 0.3f + Game.gameInstance.PC().orgasmSoftPulse * 0.5f;
				this.pleasureMain.startSpeed = this.pColor.a * 0.3f + Game.gameInstance.PC().orgasmSoftPulse * 20.5f + Mathf.Pow(Game.gameInstance.PC().proximityToOrgasm, 5f) * Game.gameInstance.PC().cockTwitch * 20f;
				this.pleasureMain.startLifetime = 1.5f - Game.gameInstance.PC().orgasmSoftPulse * 1f;
			}
			if (Game.gameInstance.PC() != null)
			{
				this.ov += (Game.gameInstance.PC().overstimulation - this.ov) * Game.cap(Time.deltaTime * 4f, 0f, 1f);
				this.f = Mathf.Pow(this.ov, 2f);
				this.overstimEffect.SetActive(this.ov > 0.01f);
				this.overstimParticles.SetActive(this.ov > 0.01f);
				if (this.ov > 0.01f)
				{
					this.ovColor.a = this.f;
					this.ovColor.a *= 1f + Game.gameInstance.PC().breath[0] * 0.4f;
					this.overstimEffectCR.SetColor(this.ovColor);
					this.overstimMain.startSize = this.f * 0.2f;
					this.overstimMain.startSpeed = this.f * 4f;
					this.overstimMain.startLifetime = 0.1f + this.f * 0.3f;
				}
			}
			if (Game.gameInstance.PC() != null)
			{
				this.pa += (Game.cap(Game.gameInstance.PC().discomfort * 4f, 0f, 1f) - this.pa) * Game.cap(Time.deltaTime * 4f, 0f, 1f);
				this.f = Mathf.Pow(this.pa, 2f);
				this.painEffect.SetActive(this.pa > 0.01f);
				this.painParticles.SetActive(this.pa > 0.01f);
				if (this.pa > 0.01f)
				{
					this.paColor.a = this.f;
					this.paColor.a *= 1f + Game.gameInstance.PC().breath[0] * 0.4f;
					this.painEffectCR.SetColor(this.paColor);
					this.painMain.startLifetime = 0.1f + this.f * 0.3f;
					this.oColor.a = this.f;
					this.painMain.startColor = this.oColor;
				}
			}
		}
	}
}
