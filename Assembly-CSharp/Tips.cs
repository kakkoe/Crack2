using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
	public Text[] tip;

	public Material[] mat;

	public Renderer torus;

	public ImperialFurPhysics wind;

	private int currTip;

	private void Start()
	{
		this.currTip = 0;
		this.ShowTip(this.currTip);
	}

	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.currTip++;
			if (this.currTip >= this.tip.Length)
			{
				this.currTip = this.tip.Length - 1;
			}
			this.ShowTip(this.currTip);
		}
		if (!Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.RightShift))
		{
			return;
		}
		this.currTip--;
		if (this.currTip < 0)
		{
			this.currTip = 0;
		}
		this.ShowTip(this.currTip);
	}

	private void ShowTip(int index)
	{
		for (int i = 0; i < this.tip.Length; i++)
		{
			this.tip[i].gameObject.SetActive(false);
		}
		this.tip[index].gameObject.SetActive(true);
		this.torus.material = this.mat[index];
		this.wind.UpdateMaterial();
	}
}
