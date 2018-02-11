using UnityEngine;

internal struct CollisionData
{
	public Vector3 position;

	public Quaternion rotation;

	public Transform surface;

	public int layer;

	public CollisionData(Vector3 Position, Quaternion Rotation, Transform Surface, int Layer)
	{
		this.position = Position;
		this.rotation = Rotation;
		this.surface = Surface;
		this.layer = Layer;
	}
}
