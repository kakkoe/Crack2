using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ToyMaterials
{
	public static List<string> materialsUnlocked = new List<string>();

	public static Dictionary<string, Material> mats = new Dictionary<string, Material>();

	public static void applyMaterialToObject(GameObject GO, string materialName)
	{
		Material material;
		if (ToyMaterials.mats.ContainsKey(materialName))
		{
			material = ToyMaterials.mats[materialName];
		}
		else
		{
			material = ToyMaterials.makeMaterial(materialName);
			ToyMaterials.mats.Add(materialName, material);
		}
		MeshRenderer[] componentsInChildren = GO.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].name.IndexOf("keepMaterial") == -1)
			{
				componentsInChildren[i].material = material;
			}
		}
		SkinnedMeshRenderer[] componentsInChildren2 = GO.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			if (componentsInChildren2[j].name.IndexOf("keepMaterial") == -1)
			{
				componentsInChildren2[j].material = material;
			}
		}
	}

	public static Material makeMaterial(string name)
	{
		Material material = new Material(Game.gameInstance.shader);
		material.EnableKeyword("_NORMALMAP");
		material.EnableKeyword("_METALLICGLOSSMAP");
		material.EnableKeyword("_EMISSION");
		Texture2D texture2D = new Texture2D(4, 4);
		byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/toymaterials/" + name + ".png");
		texture2D.LoadImage(data);
		material.SetTexture("_MainTex", texture2D);
		Texture2D texture2D2 = new Texture2D(4, 4);
		data = File.ReadAllBytes(Application.persistentDataPath + "/toymaterials/" + name + "NRM.png");
		texture2D2.LoadImage(data);
		Color[] pixels = texture2D2.GetPixels();
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i].a = pixels[i].r;
			pixels[i].r = (pixels[i].b = 0f);
		}
		texture2D2.SetPixels(pixels);
		texture2D2.Apply();
		material.SetTexture("_BumpMap", texture2D2);
		Texture2D texture2D3 = new Texture2D(4, 4);
		data = File.ReadAllBytes(Application.persistentDataPath + "/toymaterials/" + name + "FX.png");
		texture2D3.LoadImage(data);
		Texture2D texture2D4 = new Texture2D(texture2D3.width, texture2D3.height);
		Color[] pixels2 = texture2D3.GetPixels();
		for (int j = 0; j < pixels2.Length; j++)
		{
			pixels2[j].a = pixels2[j].r;
			pixels2[j].r = pixels2[j].b;
			pixels2[j].g = (pixels2[j].b = 0f);
		}
		texture2D4.SetPixels(pixels2);
		texture2D4.Apply();
		Texture2D texture2D5 = new Texture2D(texture2D3.width, texture2D3.height);
		pixels2 = texture2D3.GetPixels();
		Color[] pixels3 = texture2D.GetPixels();
		for (int k = 0; k < pixels2.Length; k++)
		{
			pixels2[k].r = pixels3[k].r * pixels2[k].g;
			pixels2[k].b = pixels3[k].b * pixels2[k].g;
			pixels2[k].g = pixels3[k].g * pixels2[k].g;
		}
		texture2D5.SetPixels(pixels2);
		texture2D5.Apply();
		material.SetTexture("_MetallicGlossMap", texture2D4);
		material.SetTexture("_EmissionMap", texture2D5);
		material.SetColor("_EmissionColor", Color.white);
		return material;
	}

	public static void update()
	{
		ToyMaterials.materialsUnlocked = new List<string>();
		new FileInfo(Application.persistentDataPath + "/toymaterials/").Directory.Create();
		string[] files = Directory.GetFiles(Application.persistentDataPath + "/toymaterials/", "*.png");
		for (int i = 0; i < files.Length; i++)
		{
			string text = (files[i] + ".png").Replace("FX.png", string.Empty).Replace("NRM.png", string.Empty).Replace(".png", string.Empty);
			text = text.Split('/')[text.Split('/').Length - 1];
			if (!ToyMaterials.materialsUnlocked.Contains(text))
			{
				ToyMaterials.materialsUnlocked.Add(text);
			}
		}
	}
}
