using System.Collections.Generic;
using UnityEngine;

public class CumDot
{
	public Vector3 lastPos;

	public Vector3 lastTailPos;

	public Vector3 position;

	public Vector3 velocity;

	public Vector3 tailvelocity;

	public Vector3 impactNormal;

	public Vector3 slideNormal;

	public GameObject GO;

	public Transform head;

	public Transform tail;

	public Transform link;

	public Color color;

	public CumDot linkedCumDot;

	public List<Vector3> travelPoints;

	public List<Vector3> travelVelocities;

	public Transform parent;

	public Material mat;

	public float travelTime;

	public float thickness;

	public float life;

	public float scaleIn;

	public bool finishedTraveling;

	public float raycastDelay;

	public int breaksRemaining = 3;

	public bool stillAttached = true;

	public bool slider;
}
