using UnityEngine;

public class RotateAround : MonoBehaviour
{
	public Transform Target;

	public float Speed = 1f;

	protected Vector3 m_TargetPosition;

	private void Start()
	{
		this.m_TargetPosition = this.Target.position;
	}

	private void Update()
	{
		base.transform.RotateAround(this.m_TargetPosition, Vector3.up, Time.deltaTime * this.Speed);
	}
}
