using UnityEngine;

public class CloseupParticles : MonoBehaviour
{
	public float range = 15f;

	private bool on;

	private float originalStartSize;

	private void Start()
	{
		this.originalStartSize = base.gameObject.GetComponent<ParticleSystem>().startSize;
	}

	private void Update()
	{
		if ((Object)Game.gameInstance != (Object)null && (Object)Game.gameInstance.mainCam != (Object)null)
		{
			float magnitude = (Game.gameInstance.mainCam.transform.position - base.transform.position).magnitude;
			if (magnitude <= this.range * 2f)
			{
				base.gameObject.GetComponent<ParticleSystem>().startSize = this.originalStartSize * Game.cap(2f - magnitude / this.range, 0f, 1f);
				if (!this.on)
				{
					base.gameObject.GetComponent<ParticleSystem>().Play();
					this.on = true;
				}
			}
			else if (this.on)
			{
				base.gameObject.GetComponent<ParticleSystem>().Pause();
				this.on = false;
			}
		}
	}
}
