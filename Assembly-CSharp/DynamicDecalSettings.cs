using UnityEngine;

public class DynamicDecalSettings : ScriptableObject
{
	public PoolInstance[] pools;

	public string[] layerNames;

	public bool highPrecision;

	public bool forceForward;

	public DynamicDecalSettings()
	{
		this.pools = new PoolInstance[1]
		{
			new PoolInstance("Default", null)
		};
		this.layerNames = new string[4]
		{
			"Layer 1",
			"Layer 2",
			"Layer 3",
			"Layer 4"
		};
		this.highPrecision = false;
		this.forceForward = false;
	}
}
