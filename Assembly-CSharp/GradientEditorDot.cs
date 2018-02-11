using UnityEngine;

public class GradientEditorDot : MonoBehaviour
{
	public GameObject system;

	private bool selected;

	private bool dragging;

	private bool goingToBeDeleted;

	public int pointID;

	public int cid;

	public int id;

	private Vector3 v3;

	public float size = 1f;

	public Vector3 lastMouse;

	private void Start()
	{
	}

	private void Update()
	{
		if (this.goingToBeDeleted)
		{
			this.size += (0.1f - this.size) * Time.deltaTime * 5f;
		}
		else if (this.selected)
		{
			this.size += (2f - this.size) * Time.deltaTime * 5f;
		}
		else
		{
			this.size += (1f - this.size) * Time.deltaTime * 5f;
		}
		base.transform.localScale = Vector3.one * this.size;
		this.v3 = base.transform.localPosition;
		if (this.dragging && Input.GetMouseButton(0))
		{
			this.goingToBeDeleted = false;
			this.v3 += Input.mousePosition - this.lastMouse;
			if (this.v3.x < -126f)
			{
				this.v3.x = -126f;
			}
			if (this.v3.x > 128f)
			{
				this.v3.x = 128f;
			}
			if (this.v3.y < -16f)
			{
				this.goingToBeDeleted = true;
			}
			if (this.v3.y > 16f)
			{
				this.goingToBeDeleted = true;
			}
			this.lastMouse = Input.mousePosition;
		}
		else
		{
			if (this.dragging)
			{
				if (this.goingToBeDeleted)
				{
					Game.gameInstance.PC().data.embellishmentColorGradientPoints.RemoveAt(this.pointID);
					Game.gameInstance.needEmbellishmentColorRebuildDots = true;
				}
				else
				{
					EmbellishmentColorGradientPoint embellishmentColorGradientPoint = Game.gameInstance.PC().data.embellishmentColorGradientPoints[this.pointID];
					Vector3 localPosition = base.transform.localPosition;
					embellishmentColorGradientPoint.position = (localPosition.x + 128f) / 256f;
				}
				Game.gameInstance.needEmbellishmentColorRedraw = true;
				Game.gameInstance.characterRedrawDelay = 2;
				this.dragging = false;
			}
			this.v3.y += (0f - this.v3.y) * Time.deltaTime * 8f;
		}
		base.transform.localPosition = this.v3;
	}

	public void dragStart()
	{
		GradientEditorDot[] componentsInChildren = this.system.GetComponentsInChildren<GradientEditorDot>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].selected = false;
		}
		this.selected = true;
		this.dragging = true;
		Vector3 localPosition = base.transform.localPosition;
		if (localPosition.x <= -128f)
		{
			this.dragging = false;
		}
		this.lastMouse = Input.mousePosition;
		Game.gameInstance.selectedEmbellishmentGradientDotC = this.cid;
		Game.gameInstance.selectedEmbellishmentGradientDot = this.id;
		Game.gameInstance.embellishmentGradientColorPickerWasOpen = false;
	}
}
