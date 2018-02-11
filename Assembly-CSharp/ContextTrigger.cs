using UnityEngine;

public class ContextTrigger : MonoBehaviour
{
	public float range = 8f;

	public string prompt = string.Empty;

	[SerializeField]
	public contextEvent function;

	private void Start()
	{
	}

	public bool callFunc()
	{
		if (this.function != null)
		{
			this.function.Invoke(base.transform);
		}
		else
		{
			Debug.Log("No function set for " + base.transform.name);
		}
		return true;
	}

	private void Update()
	{
		if ((Object)Game.gameInstance != (Object)null && Game.gameInstance.PC() != null && (Game.gameInstance.PC().GO.transform.position - base.transform.position).magnitude < this.range)
		{
			Game.gameInstance.context(Localization.getPhrase(this.prompt, string.Empty), this.callFunc, base.transform.position, false);
		}
	}
}
