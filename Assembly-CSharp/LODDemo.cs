using UnityEngine;
using UnityEngine.UI;

public class LODDemo : MonoBehaviour
{
	public GameObject lodSphere;

	public Text buttonLabel;

	public bool lodOn = true;

	private void Start()
	{
		float num = -17.5f;
		for (int i = 0; i < 15; i++)
		{
			float num2 = 1f;
			for (int j = 0; j < 15; j++)
			{
				Object.Instantiate(this.lodSphere, new Vector3(num, 1f, num2), Quaternion.identity);
				num2 += 5f;
			}
			num += 2.5f;
		}
		this.buttonLabel.text = "LOD On: " + this.lodOn.ToString();
	}

	public void ToggleLod()
	{
		this.lodOn = !this.lodOn;
		this.buttonLabel.text = "LOD On: " + this.lodOn.ToString();
	}
}
