using System.Collections.Generic;
using UnityEngine;

public class CumEmitter : MonoBehaviour
{
	public ParticleSystem part;

	public List<ParticleCollisionEvent> collisionEvents;

	public ParticleSystem.MainModule mainModule;

	public ParticleSystem.EmissionModule emissionModule;

	private Vector3 startAngle;

	private Vector3 v3;

	private float eRate;

	private void Start()
	{
		this.part = base.GetComponent<ParticleSystem>();
		this.collisionEvents = new List<ParticleCollisionEvent>();
		this.emissionModule = this.part.emission;
		this.mainModule = this.part.main;
		this.startAngle = base.transform.eulerAngles;
	}

	private void Update()
	{
	}

	private void OnParticleCollision(GameObject other)
	{
	}
}
