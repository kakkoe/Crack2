using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
	public Camera m_Camera;

	private void Update()
	{
		base.transform.LookAt(base.transform.position + this.m_Camera.transform.rotation * Vector3.forward, this.m_Camera.transform.rotation * Vector3.up);
	}
}
