using UnityEngine;

public class Drone : MonoBehaviour
{
	public Vector3 target;

	private AudioSource audioS;

	private Vector3 vel;

	private float throttle;

	private float actualThrottle;

	public Vector3 manualTarget;

	private Transform projectorArm;

	private Transform laserArm;

	private Vector3 projectorTarget;

	private Vector3 laserTarget;

	private Vector3 randomTarget;

	private Vector3 laserTarget_act;

	private Vector3 projectorTarget_act;

	private bool randomLaser;

	private float randomDelay;

	private int randomSwitch;

	private float windDelay;

	private Vector3 wind;

	private Transform body;

	private Vector3 v3;

	private float vol;

	private void Start()
	{
		this.target = base.transform.position;
		this.audioS = base.GetComponent<AudioSource>();
		this.manualTarget = Vector3.zero;
		this.body = base.transform.Find("helperdrone");
		this.projectorArm = base.transform.Find("helperdrone").Find("projectorArm");
		this.laserArm = base.transform.Find("helperdrone").Find("laserArm");
		this.projectorTarget = Vector3.zero;
		this.laserTarget = Vector3.zero;
		this.laserTarget_act = this.laserTarget;
		this.projectorTarget_act = this.projectorTarget;
	}

	private void Update()
	{
		this.vol = 0.7f;
		if (this.manualTarget.magnitude == 0f)
		{
			if (Game.gameInstance.PC() != null)
			{
				this.v3 = Game.gameInstance.PC().bones.Head.position + Vector3.up * 1.4f;
				this.v3 -= this.target;
				if (this.v3.magnitude > 4f)
				{
					float d = this.v3.magnitude - 4f;
					this.target += this.v3.normalized * d;
				}
				if (this.v3.magnitude > 20f)
				{
					base.transform.position = this.target;
				}
				this.v3 = base.transform.position - Game.gameInstance.PC().bones.Neck.position;
				if ((double)this.v3.magnitude < 3.0)
				{
					float d2 = 3f - this.v3.magnitude;
					this.target += this.v3.normalized * d2;
				}
				if (this.randomLaser)
				{
					this.projectorTarget = Game.gameInstance.PC().bones.Head.position;
					this.laserTarget = this.randomTarget;
				}
				else
				{
					this.projectorTarget = this.randomTarget;
					this.laserTarget = Game.gameInstance.PC().bones.Head.position;
				}
				this.randomDelay -= Time.deltaTime;
				if (this.randomDelay <= 0f)
				{
					this.randomTarget = base.transform.position + Vector3.down * 5f;
					this.randomTarget.x += -10f + Random.value * 20f;
					this.randomTarget.y += -2f + Random.value * 12f;
					this.randomTarget.z += -10f + Random.value * 20f;
					this.randomDelay += 0.2f + Random.value;
					this.randomSwitch--;
					if (this.randomSwitch <= 0)
					{
						this.randomSwitch = Mathf.FloorToInt(2f + Random.value * 5f);
						this.randomLaser = !this.randomLaser;
					}
				}
				if (Game.gameInstance.PC().interactionSubject != null)
				{
					this.vol = 0.2f;
				}
			}
		}
		else
		{
			this.target = this.manualTarget;
		}
		this.laserTarget_act += (this.laserTarget - this.laserTarget_act) * Game.cap(Time.deltaTime * 30f, 0f, 1f);
		this.laserArm.LookAt(this.laserTarget_act);
		this.projectorTarget_act += (this.projectorTarget - this.projectorTarget_act) * Game.cap(Time.deltaTime * 30f, 0f, 1f);
		this.projectorArm.LookAt(this.projectorTarget_act);
		ref Vector3 val = ref this.v3;
		float z = this.target.z;
		Vector3 position = base.transform.position;
		val.x = Game.cap((z - position.z) * 0.25f, -1f, 1f) * 20f;
		this.v3.y = 0f;
		ref Vector3 val2 = ref this.v3;
		float x = this.target.x;
		Vector3 position2 = base.transform.position;
		val2.z = Game.cap((x - position2.x) * 0.25f, -1f, 1f) * -20f;
		base.transform.eulerAngles = this.v3;
		Vector3 gravity = Physics.gravity;
		float num = (0f - gravity.y) * 1f;
		float y = this.target.y;
		Vector3 position3 = base.transform.position;
		this.throttle = num + Game.cap((y - position3.y) * 2.5f, -1f, 1f) * 4f + this.v3.magnitude / 20f;
		this.actualThrottle += (this.throttle - this.actualThrottle) * Game.cap(Time.deltaTime * 30f, 0f, 1f);
		this.vel += Physics.gravity;
		this.vel += this.actualThrottle * base.transform.up;
		this.vel -= this.vel * Game.cap(Time.deltaTime * 10f, 0f, 1f);
		this.windDelay -= Time.deltaTime;
		if (this.windDelay <= 0f)
		{
			this.windDelay += Random.value * 3f;
			this.wind.x = -1f + Random.value * 2f;
			this.wind.y = -1f + Random.value * 2f;
			this.wind.z = -1f + Random.value * 2f;
		}
		this.vel += this.wind;
		if (this.vel.magnitude > 30f)
		{
			this.vel = this.vel.normalized * 30f;
		}
		Transform transform = base.transform;
		transform.position += this.vel * Time.deltaTime;
		this.body.Rotate(0f, Time.deltaTime * 90f, 0f);
		AudioSource audioSource = this.audioS;
		float num2 = this.actualThrottle;
		Vector3 gravity2 = Physics.gravity;
		audioSource.volume = num2 / (0f - gravity2.y) * this.vol;
		this.audioS.pitch = this.audioS.volume / this.vol;
	}
}
