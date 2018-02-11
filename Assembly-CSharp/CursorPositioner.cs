using UnityEngine;

public class CursorPositioner : Positioner
{
	public Camera projectionCamera;

	protected override void Start()
	{
		if ((Object)this.projectionCamera == (Object)null)
		{
			this.projectionCamera = Camera.main;
		}
		base.Start();
	}

	private void LateUpdate()
	{
		base.Reproject(this.projectionCamera.ScreenPointToRay(Input.mousePosition), float.PositiveInfinity, this.projectionCamera.transform.up);
	}
}
