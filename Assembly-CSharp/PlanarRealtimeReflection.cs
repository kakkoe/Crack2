using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class PlanarRealtimeReflection : MonoBehaviour
{
	public bool m_DisablePixelLights = true;

	public int m_TextureResolution = 1024;

	public float m_clipPlaneOffset = 0.07f;

	private float m_finalClipPlaneOffset;

	public bool m_NormalsFromMesh;

	public bool m_BaseClipOffsetFromMesh;

	public bool m_BaseClipOffsetFromMeshInverted;

	private Vector3 m_calculatedNormal = Vector3.zero;

	public LayerMask m_ReflectLayers = -1;

	private Hashtable m_ReflectionCameras = new Hashtable();

	private RenderTexture m_ReflectionTexture;

	private int m_OldReflectionTextureSize;

	private static bool s_InsideRendering;

	public void OnWillRenderObject()
	{
		if (base.enabled && (bool)base.GetComponent<Renderer>() && (bool)base.GetComponent<Renderer>().sharedMaterial && base.GetComponent<Renderer>().enabled)
		{
			Camera current = Camera.current;
			if ((bool)current)
			{
				if (this.m_NormalsFromMesh && (UnityEngine.Object)base.GetComponent<MeshFilter>() != (UnityEngine.Object)null)
				{
					this.m_calculatedNormal = base.transform.TransformDirection(base.GetComponent<MeshFilter>().sharedMesh.normals[0]);
				}
				if (this.m_BaseClipOffsetFromMesh && (UnityEngine.Object)base.GetComponent<MeshFilter>() != (UnityEngine.Object)null)
				{
					this.m_finalClipPlaneOffset = (base.transform.position - base.transform.TransformPoint(base.GetComponent<MeshFilter>().sharedMesh.vertices[0])).magnitude + this.m_clipPlaneOffset;
				}
				else if (this.m_BaseClipOffsetFromMeshInverted && (UnityEngine.Object)base.GetComponent<MeshFilter>() != (UnityEngine.Object)null)
				{
					this.m_finalClipPlaneOffset = 0f - (base.transform.position - base.transform.TransformPoint(base.GetComponent<MeshFilter>().sharedMesh.vertices[0])).magnitude + this.m_clipPlaneOffset;
				}
				else
				{
					this.m_finalClipPlaneOffset = this.m_clipPlaneOffset;
				}
				if (!PlanarRealtimeReflection.s_InsideRendering)
				{
					PlanarRealtimeReflection.s_InsideRendering = true;
					Camera camera = default(Camera);
					this.CreateSurfaceObjects(current, out camera);
					Vector3 position = base.transform.position;
					Vector3 vector = (!this.m_NormalsFromMesh || !((UnityEngine.Object)base.GetComponent<MeshFilter>() != (UnityEngine.Object)null)) ? base.transform.up : this.m_calculatedNormal;
					int pixelLightCount = QualitySettings.pixelLightCount;
					if (this.m_DisablePixelLights)
					{
						QualitySettings.pixelLightCount = 0;
					}
					this.UpdateCameraModes(current, camera);
					float w = 0f - Vector3.Dot(vector, position) - this.m_finalClipPlaneOffset;
					Vector4 plane = new Vector4(vector.x, vector.y, vector.z, w);
					Matrix4x4 zero = Matrix4x4.zero;
					PlanarRealtimeReflection.CalculateReflectionMatrix(ref zero, plane);
					Vector3 position2 = current.transform.position;
					Vector3 position3 = zero.MultiplyPoint(position2);
					camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
					Vector4 clipPlane = this.CameraSpacePlane(camera, position, vector, 1f);
					Matrix4x4 projectionMatrix = current.projectionMatrix;
					PlanarRealtimeReflection.CalculateObliqueMatrix(ref projectionMatrix, clipPlane);
					camera.projectionMatrix = projectionMatrix;
					camera.cullingMask = (-17 & this.m_ReflectLayers.value);
					camera.targetTexture = this.m_ReflectionTexture;
					GL.SetRevertBackfacing(true);
					camera.transform.position = position3;
					Vector3 eulerAngles = current.transform.eulerAngles;
					camera.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
					camera.Render();
					camera.transform.position = position2;
					GL.SetRevertBackfacing(false);
					Material[] sharedMaterials = base.GetComponent<Renderer>().sharedMaterials;
					Material[] array = sharedMaterials;
					foreach (Material material in array)
					{
						if (material.HasProperty("_ReflectionTex"))
						{
							material.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
						}
					}
					Matrix4x4 lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
					Vector3 lossyScale = base.transform.lossyScale;
					Matrix4x4 rhs = base.transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z));
					rhs = lhs * current.projectionMatrix * current.worldToCameraMatrix * rhs;
					Material[] array2 = sharedMaterials;
					foreach (Material material2 in array2)
					{
						material2.SetMatrix("_ProjMatrix", rhs);
					}
					if (this.m_DisablePixelLights)
					{
						QualitySettings.pixelLightCount = pixelLightCount;
					}
					PlanarRealtimeReflection.s_InsideRendering = false;
				}
			}
		}
	}

	private void OnDisable()
	{
		if ((bool)this.m_ReflectionTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			this.m_ReflectionTexture = null;
		}
		IDictionaryEnumerator enumerator = this.m_ReflectionCameras.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)enumerator.Current).Value).gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.m_ReflectionCameras.Clear();
	}

	private void UpdateCameraModes(Camera src, Camera dest)
	{
		if (!((UnityEngine.Object)dest == (UnityEngine.Object)null))
		{
			dest.clearFlags = src.clearFlags;
			dest.backgroundColor = src.backgroundColor;
			if (src.clearFlags == CameraClearFlags.Skybox)
			{
				Skybox skybox = src.GetComponent(typeof(Skybox)) as Skybox;
				Skybox skybox2 = dest.GetComponent(typeof(Skybox)) as Skybox;
				if (!(bool)skybox || !(bool)skybox.material)
				{
					skybox2.enabled = false;
				}
				else
				{
					skybox2.enabled = true;
					skybox2.material = skybox.material;
				}
			}
			dest.farClipPlane = src.farClipPlane;
			dest.nearClipPlane = src.nearClipPlane;
			dest.orthographic = src.orthographic;
			dest.fieldOfView = src.fieldOfView;
			dest.aspect = src.aspect;
			dest.orthographicSize = src.orthographicSize;
		}
	}

	private void CreateSurfaceObjects(Camera currentCamera, out Camera reflectionCamera)
	{
		reflectionCamera = null;
		if (!(bool)this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.m_TextureResolution)
		{
			if ((bool)this.m_ReflectionTexture)
			{
				UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			}
			this.m_ReflectionTexture = new RenderTexture(this.m_TextureResolution, this.m_TextureResolution, 16);
			this.m_ReflectionTexture.name = "__SurfaceReflection" + base.GetInstanceID();
			this.m_ReflectionTexture.isPowerOfTwo = true;
			this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
			this.m_OldReflectionTextureSize = this.m_TextureResolution;
		}
		reflectionCamera = (this.m_ReflectionCameras[currentCamera] as Camera);
		if (!(bool)reflectionCamera)
		{
			GameObject gameObject = new GameObject("Surface Refl Camera id" + base.GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
			reflectionCamera = gameObject.GetComponent<Camera>();
			reflectionCamera.enabled = false;
			reflectionCamera.transform.position = base.transform.position;
			reflectionCamera.transform.rotation = base.transform.rotation;
			reflectionCamera.gameObject.AddComponent<FlareLayer>();
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			this.m_ReflectionCameras[currentCamera] = reflectionCamera;
		}
	}

	private static float sgn(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 point = pos + normal * this.m_finalClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, 0f - Vector3.Dot(lhs, rhs));
	}

	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(PlanarRealtimeReflection.sgn(clipPlane.x), PlanarRealtimeReflection.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
	}

	private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
	}
}
