using UnityEngine;

public class SizeDifferencePositionAdjuster : MonoBehaviour
{
	public static float sizeDifference = 1f;

	private Vector3 originalLocation;

	private Vector3 originalEulers;

	public Vector3 largeLocationOffset;

	public Vector3 smallLocationOffset;

	public Vector3 largeRotationOffset;

	public Vector3 smallRotationOffset;

	public static bool editingSizeDifferenceAdjusters;

	private void Start()
	{
		this.originalLocation = base.transform.localPosition;
		this.originalEulers = base.transform.localEulerAngles;
	}

	private void Update()
	{
		if (SizeDifferencePositionAdjuster.editingSizeDifferenceAdjusters)
		{
			if (SizeDifferencePositionAdjuster.sizeDifference > 1f)
			{
				this.largeLocationOffset = (base.transform.localPosition - this.originalLocation) / (SizeDifferencePositionAdjuster.sizeDifference - 1f);
				this.largeRotationOffset = Game.degreeDist3(base.transform.localEulerAngles, this.originalEulers) / (SizeDifferencePositionAdjuster.sizeDifference - 1f);
			}
			else
			{
				this.smallLocationOffset = (base.transform.localPosition - this.originalLocation) / (1f / SizeDifferencePositionAdjuster.sizeDifference - 1f);
				this.smallRotationOffset = Game.degreeDist3(base.transform.localEulerAngles, this.originalEulers) / (1f / SizeDifferencePositionAdjuster.sizeDifference - 1f);
			}
			string text = "large location offset: " + this.largeLocationOffset.x + "," + this.largeLocationOffset.y + "," + this.largeLocationOffset.z;
			text = text + "\r\nlarge rotation offset: " + this.largeRotationOffset.x + "," + this.largeRotationOffset.y + "," + this.largeRotationOffset.z;
			text = text + "\r\nsmall location offset: " + this.smallLocationOffset.x + "," + this.smallLocationOffset.y + "," + this.smallLocationOffset.z;
			text = text + "\r\nsmall rotation offset: " + this.smallRotationOffset.x + "," + this.smallRotationOffset.y + "," + this.smallRotationOffset.z;
			Debug.Log(text);
		}
		else if (SizeDifferencePositionAdjuster.sizeDifference > 1f)
		{
			base.transform.localPosition = this.originalLocation + this.largeLocationOffset * (SizeDifferencePositionAdjuster.sizeDifference - 1f);
			base.transform.localEulerAngles = this.originalEulers + this.largeRotationOffset * (SizeDifferencePositionAdjuster.sizeDifference - 1f);
		}
		else
		{
			base.transform.localPosition = this.originalLocation + this.smallLocationOffset * (1f / SizeDifferencePositionAdjuster.sizeDifference - 1f);
			base.transform.localEulerAngles = this.originalEulers + this.smallRotationOffset * (1f / SizeDifferencePositionAdjuster.sizeDifference - 1f);
		}
	}
}
