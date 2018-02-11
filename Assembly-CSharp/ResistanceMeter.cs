using System.Collections.Generic;
using UnityEngine;

public class ResistanceMeter : MonoBehaviour
{
	private List<Transform> dots;

	private int numDots = 19;

	private float dotSpread = 16f;

	public float push;

	private Vector3 v3 = default(Vector3);

	private float distFromCenter;

	private float thickness;

	private void Start()
	{
		this.dots = new List<Transform>();
		this.dots.Add(base.transform.Find("dot"));
		for (int i = 1; i < this.numDots; i++)
		{
			GameObject gameObject = Object.Instantiate(this.dots[0].gameObject);
			gameObject.transform.SetParent(this.dots[0].parent);
			gameObject.transform.localScale = this.dots[0].localScale;
			gameObject.transform.localPosition = this.dots[0].localPosition;
			gameObject.transform.localEulerAngles = this.dots[0].localEulerAngles;
			this.dots.Add(gameObject.transform);
		}
	}

	public void setColor(Color col)
	{
		if (this.dots != null)
		{
			for (int i = 0; i < this.numDots; i++)
			{
				((Component)this.dots[i]).GetComponent<CanvasRenderer>().SetColor(col);
			}
		}
	}

	private void Update()
	{
		this.push = Game.cap(this.push, -1f, 1f);
		this.dotSpread = 7f - 2.5f * Mathf.Abs(this.push);
		this.thickness += (0.14f - Mathf.Abs(this.push) * 0.08f - this.thickness) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
		for (int num = this.numDots - 1; num >= 0; num--)
		{
			this.v3.x = this.dotSpread * ((float)num - (float)(this.numDots - 1) / 2f);
			this.distFromCenter = Mathf.Pow(Mathf.Abs(this.v3.x) / (((float)this.numDots - 1f) / 2f * this.dotSpread), 2f);
			this.v3.y = Mathf.Pow(0.95f - this.distFromCenter * 0.9f, Mathf.Abs(this.push)) * (this.push * 120f);
			this.v3.z = 0f;
			this.dots[num].localPosition = this.v3;
			if (num < this.numDots - 1)
			{
				this.v3.x = 0f;
				this.v3.y = 0f;
				ref Vector3 val = ref this.v3;
				Vector3 localPosition = this.dots[num + 1].localPosition;
				float y = localPosition.y;
				Vector3 localPosition2 = this.dots[num].localPosition;
				float y2 = y - localPosition2.y;
				Vector3 localPosition3 = this.dots[num + 1].localPosition;
				float x = localPosition3.x;
				Vector3 localPosition4 = this.dots[num].localPosition;
				val.z = Mathf.Atan2(y2, x - localPosition4.x) * 180f / 3.1415f;
				this.dots[num].localEulerAngles = this.v3;
				this.v3.x = (this.dots[num].localPosition - this.dots[num + 1].localPosition).magnitude * 0.063f;
				this.v3.y = this.thickness;
				this.v3.z = 1f;
				this.dots[num].localScale = this.v3;
			}
			else
			{
				this.dots[num].localScale = Vector3.zero;
			}
			((Component)this.dots[num]).GetComponent<CanvasRenderer>().SetAlpha(Mathf.Abs(this.push));
		}
	}
}
