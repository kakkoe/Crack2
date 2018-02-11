using UnityEngine;

public class TutorialTooltip : MonoBehaviour
{
	public string tutorialFlag = string.Empty;

	private float originalScale;

	public static int recentTutorialCompletion;

	private void Start()
	{
		Vector3 localScale = base.transform.localScale;
		this.originalScale = localScale.x;
		this.disableIfNotNeeded();
	}

	private void Update()
	{
		if (TutorialTooltip.recentTutorialCompletion > 0)
		{
			this.disableIfNotNeeded();
		}
		base.transform.localScale = Vector3.one * this.originalScale * (1f + Mathf.Cos(Time.time * 10f) * 0.1f);
	}

	public void hover()
	{
		Tutorials.forcedTutorialFromTooltip = true;
		Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase(this.tutorialFlag, string.Empty));
		Tutorials.tutorialDroneAnchorX = 0;
		Tutorials.tutorialDroneAnchorY = 0;
		Tutorials.tutorialDronePosition = Game.gameInstance.UI.transform.InverseTransformPoint(base.transform.position);
		UserSettings.completeTutorial(this.tutorialFlag);
	}

	private void disableIfNotNeeded()
	{
		if (!UserSettings.needTutorial(this.tutorialFlag))
		{
			base.gameObject.SetActive(false);
		}
	}
}
