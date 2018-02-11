using UnityEngine;
using UnityEngine.Rendering;

public class RealtimeReflections : MonoBehaviour
{
	public int cubemapSize = 128;

	public float nearClip = 0.01f;

	public float farClip = 500f;

	public bool oneFacePerFrame;

	public Material[] materials;

	public ReflectionProbe[] reflectionProbes;

	public LayerMask layerMask = -1;

	private Camera cam;

	private RenderTexture renderTexture;

	private void OnEnable()
	{
		this.layerMask.value = -1;
	}

	private void Start()
	{
		ReflectionProbe[] array = this.reflectionProbes;
		foreach (ReflectionProbe reflectionProbe in array)
		{
			reflectionProbe.mode = ReflectionProbeMode.Realtime;
			reflectionProbe.boxProjection = true;
			reflectionProbe.resolution = this.cubemapSize;
			reflectionProbe.transform.parent = base.transform.parent;
			reflectionProbe.transform.localPosition = Vector3.zero;
		}
		if (this.materials.Length > 0)
		{
			this.UpdateCubemap(63);
		}
	}

	private void LateUpdate()
	{
		if (this.materials.Length > 0)
		{
			if (this.oneFacePerFrame)
			{
				int num = Time.frameCount % 6;
				int faceMask = 1 << num;
				this.UpdateCubemap(faceMask);
			}
			else
			{
				this.UpdateCubemap(63);
			}
		}
	}

	private void UpdateCubemap(int faceMask)
	{
		if (!(bool)this.cam)
		{
			GameObject gameObject = new GameObject("CubemapCamera", typeof(Camera));
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			gameObject.transform.position = base.transform.position;
			gameObject.transform.rotation = Quaternion.identity;
			this.cam = gameObject.GetComponent<Camera>();
			this.cam.cullingMask = this.layerMask;
			this.cam.nearClipPlane = this.nearClip;
			this.cam.farClipPlane = this.farClip;
			this.cam.enabled = false;
		}
		if (!(bool)this.renderTexture)
		{
			this.renderTexture = new RenderTexture(this.cubemapSize, this.cubemapSize, 16);
			this.renderTexture.isPowerOfTwo = true;
			this.renderTexture.isCubemap = true;
			this.renderTexture.hideFlags = HideFlags.HideAndDontSave;
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				Material[] sharedMaterials = renderer.sharedMaterials;
				foreach (Material material in sharedMaterials)
				{
					if (material.HasProperty("_Cube"))
					{
						material.SetTexture("_Cube", this.renderTexture);
					}
				}
			}
			ReflectionProbe[] array = this.reflectionProbes;
			foreach (ReflectionProbe reflectionProbe in array)
			{
				reflectionProbe.customBakedTexture = this.renderTexture;
			}
		}
		this.cam.transform.position = base.transform.position;
		this.cam.RenderToCubemap(this.renderTexture, faceMask);
	}

	private void OnDisable()
	{
		Object.DestroyImmediate(this.cam);
		Object.DestroyImmediate(this.renderTexture);
	}
}
