using System;
using UnityEngine;

public class LittleColorPicker : MonoBehaviour
{
	public Func<bool> onChange;

	private bool changeMade;

	private string guid;

	private RaycastHit hitInfo;

	private float hue;

	private float sat;

	private Color col;

	private float timeSinceLastDragTick;

	private void Start()
	{
		this.guid = Guid.NewGuid().ToString();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			bool flag = Physics.Raycast(Game.gameInstance.uiCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out this.hitInfo);
			if (this.timeSinceLastDragTick < 10f)
			{
				this.timeSinceLastDragTick += Time.deltaTime;
			}
			if (flag && ((Component)this.hitInfo.transform.parent).GetComponent<LittleColorPicker>().guid == this.guid)
			{
				float num = (float)base.GetComponentInChildren<MeshRenderer>().material.mainTexture.width;
				float num2 = (float)base.GetComponentInChildren<MeshRenderer>().material.mainTexture.height;
				Vector2 textureCoord = this.hitInfo.textureCoord;
				textureCoord.y = 1f - textureCoord.y;
				textureCoord.x *= num;
				textureCoord.y *= num2;
				this.hue = textureCoord.x;
				this.hue /= num;
				if (this.hue < 0f)
				{
					this.hue = 0f;
				}
				if (this.hue > 1f)
				{
					this.hue = 1f;
				}
				this.sat = textureCoord.y;
				this.sat /= num2;
				if (this.sat < 0f)
				{
					this.sat = 0f;
				}
				if (this.sat > 1f)
				{
					this.sat = 1f;
				}
				this.sat = 1f - this.sat;
				this.col = ColorPicker.HsvToColor(this.hue * 360f, this.sat, 1f);
				this.col.a = 1f;
				((Component)base.transform.Find("color")).GetComponent<CanvasRenderer>().SetColor(this.col);
				this.changeMade = true;
				if (Game.gameInstance.mouseChange.magnitude > 0f && this.timeSinceLastDragTick >= 0.06f)
				{
					this.timeSinceLastDragTick -= 0.06f;
					Game.gameInstance.playSound("ui_dragtick", 1f, 1f);
				}
			}
		}
		if (this.changeMade)
		{
			if (this.onChange != null)
			{
				this.onChange();
			}
			this.changeMade = false;
		}
	}
}
