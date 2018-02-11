using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ParticleSystem))]
public class ParticleCollisionPrinter : Printer
{
	public RotationSource rotationSource;

	private ParticleSystem partSystem;

	private List<ParticleCollisionEvent> collisionEvents;

	private Vector3[] recentImpactPoints = new Vector3[100];

	private bool impactPointsInitted;

	private int nextImpactPoint;

	private float closestDist;

	private float dist;

	private int closestImpactPoint;

	private int neighborsRequiredForSplat = 3;

	private void Start()
	{
		this.partSystem = base.GetComponent<ParticleSystem>();
		if (Application.isPlaying)
		{
			this.collisionEvents = new List<ParticleCollisionEvent>();
		}
	}

	private void Update()
	{
		if (!this.partSystem.collision.enabled)
		{
			Debug.LogWarning("Particle system collisions must be enabled for the particle system to print decals");
		}
		else if (!this.partSystem.collision.sendCollisionMessages)
		{
			Debug.LogWarning("Particle system must send collision messages for the particle system to print decals. This option can be enabled under the collisions menu.");
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		if (!this.impactPointsInitted)
		{
			for (int i = 0; i < this.recentImpactPoints.Length; i++)
			{
				this.recentImpactPoints[i] = default(Vector3);
			}
			this.impactPointsInitted = true;
		}
		if (Application.isPlaying)
		{
			int num = this.partSystem.GetCollisionEvents(other, this.collisionEvents);
			int j = 0;
			int num2 = 0;
			for (; j < num; j++)
			{
				Vector3 upwards;
				if (this.rotationSource == RotationSource.Link)
				{
					this.closestDist = 10f;
					for (int k = 0; k < this.recentImpactPoints.Length; k++)
					{
						this.dist = (this.recentImpactPoints[k] - this.collisionEvents[j].intersection).magnitude;
						if (this.dist < this.closestDist)
						{
							this.closestImpactPoint = k;
							this.closestDist = this.dist;
							if (this.dist < base.frequencyDistance * 1.25f)
							{
								num2++;
							}
							if (num2 >= this.neighborsRequiredForSplat)
							{
								break;
							}
						}
					}
					upwards = (this.collisionEvents[j].intersection - this.recentImpactPoints[this.closestImpactPoint]).normalized;
				}
				else
				{
					upwards = ((this.rotationSource != 0 || !(this.collisionEvents[j].velocity != Vector3.zero)) ? ((this.rotationSource != RotationSource.Random) ? Vector3.up : Random.insideUnitSphere.normalized) : this.collisionEvents[j].velocity.normalized);
				}
				if (base.Print(this.collisionEvents[j].intersection, Quaternion.LookRotation(-this.collisionEvents[j].normal, upwards), other.transform, other.layer, 1f, num2 >= this.neighborsRequiredForSplat))
				{
					this.recentImpactPoints[this.nextImpactPoint] = this.collisionEvents[j].intersection;
					this.nextImpactPoint++;
					if (this.nextImpactPoint == this.recentImpactPoints.Length)
					{
						this.nextImpactPoint = 0;
					}
				}
			}
		}
	}
}
