using UnityEngine;

public class GlobalFollower : MonoBehaviour
{
	public bool initted;

	public GameObject globalPoint;

	public Transform globalPointTransform;

	private void Start()
	{
	}

	public void checkInit()
	{
		if (!this.initted)
		{
			this.globalPoint = new GameObject("globalFollower_" + base.name);
			this.globalPoint.AddComponent<GlobalFollowerPoint>().mother = this;
			this.globalPointTransform = this.globalPoint.transform;
			this.globalPointTransform.position = base.transform.position;
			this.globalPointTransform.SetParent(null);
			this.initted = true;
		}
	}

	private void Update()
	{
		this.checkInit();
		this.globalPointTransform.position = base.transform.position;
		this.globalPointTransform.rotation = base.transform.rotation;
	}

	private void OnDestroy()
	{
		Object.Destroy(this.globalPoint);
		this.initted = false;
	}
}
