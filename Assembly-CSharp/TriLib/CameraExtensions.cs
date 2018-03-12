using UnityEngine;

namespace TriLib
{
	public static class CameraExtensions
	{
		public static void FitToBounds(this Camera camera, Transform transform, float distance)
		{
			Bounds bounds = transform.EncapsulateBounds();
			float magnitude = bounds.extents.magnitude;
			float num = magnitude / (2f * Mathf.Tan(0.5f * camera.fieldOfView * 0.0174532924f)) * distance;
			if (!float.IsNaN(num))
			{
				camera.farClipPlane = num * 2f;
				Transform transform2 = camera.transform;
				Vector3 center = bounds.center;
				float x = center.x;
				Vector3 center2 = bounds.center;
				float y = center2.y;
				Vector3 center3 = bounds.center;
				transform2.position = new Vector3(x, y, center3.z + num);
				camera.transform.LookAt(bounds.center);
			}
		}
	}
}
