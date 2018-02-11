using UnityEngine;

public class Flash : MonoBehaviour
{
	public float speed = 1f;

	private float f;

	private void Start()
	{
	}

	private void Update()
	{
		this.f += Time.deltaTime * this.speed;
		CanvasRenderer[] componentsInChildren = base.GetComponentsInChildren<CanvasRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Color color = componentsInChildren[i].GetColor();
			color.a = Mathf.Cos(this.f) * -0.5f + 0.5f;
			componentsInChildren[i].SetColor(color);
		}
	}
}
