using UnityEngine;

public class DynamicLight : MonoBehaviour
{
	private MeshRenderer bulb;

	private Material glowMaterial;

	public float intensityModifier = 1f;

	private void Start()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).name.IndexOf("bulb") != -1)
			{
				this.bulb = ((Component)base.transform.GetChild(i)).GetComponent<MeshRenderer>();
				this.glowMaterial = this.bulb.material;
			}
		}
	}

	private void Update()
	{
		if ((Object)base.GetComponentInChildren<LabItemInWorld>() != (Object)null && base.GetComponentInChildren<LabItemInWorld>().layoutData != null)
		{
			base.GetComponentInChildren<Light>().enabled = base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.enabled;
			base.GetComponentInChildren<Light>().color = base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.color;
			base.GetComponentInChildren<Light>().range = 30f + base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.power * 100f;
			base.GetComponentInChildren<Light>().intensity = 0.5f + base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.power * 0.5f * this.intensityModifier;
			if ((Object)this.bulb != (Object)null)
			{
				if (base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.enabled)
				{
					this.glowMaterial.SetColor("_EmissionColor", base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.color * 2f * base.GetComponentInChildren<LabItemInWorld>().layoutData.customProperties.power);
				}
				else
				{
					this.glowMaterial.SetColor("_EmissionColor", Color.black);
				}
			}
		}
	}
}
