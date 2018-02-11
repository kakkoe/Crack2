using UnityEngine;
using UnityEngine.UI;

public class PhysicsDemo : MonoBehaviour
{
	public GameObject[] spheres;

	public Text shellText;

	public Text shaderText;

	private const int shell10 = 0;

	private const int shell20 = 1;

	private const int shell40 = 2;

	private const int typeMain = 0;

	private const int typeSimple = 1;

	private int shellCount;

	private int type;

	private void Start()
	{
		this.type = 0;
		this.shellCount = 1;
		this.shellText.text = "Shells: 20";
		this.SetShaders();
	}

	public void ToggleShellCount()
	{
		this.shellCount++;
		if (this.shellCount > 2)
		{
			this.shellCount = 0;
		}
		this.SetShaders();
	}

	public void ToggleShader()
	{
		this.type++;
		if (this.type > 1)
		{
			this.type = 0;
		}
		this.SetShaders();
	}

	private void SetShaders()
	{
		string text = string.Empty;
		switch (this.type)
		{
		case 0:
			this.shaderText.text = "Shader: Blend";
			text = "Imperial Fur/Main/Specular/";
			break;
		case 1:
			this.shaderText.text = "Shader: Simple";
			text = "Imperial Fur/Simple/Specular Skin/";
			break;
		}
		switch (this.shellCount)
		{
		case 0:
			this.shellText.text = "Shells: 10";
			text += "10 Shell";
			break;
		case 1:
			this.shellText.text = "Shells: 20";
			text += "20 Shell";
			break;
		case 2:
			this.shellText.text = "Shells: 40";
			text += "40 Shell";
			break;
		}
		for (int i = 0; i < 6; i++)
		{
			Material material = this.spheres[i].GetComponent<Renderer>().material;
			material.shader = Shader.Find(text);
		}
	}
}
