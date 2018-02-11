using UnityEngine;

public class ClothingIcon : MonoBehaviour
{
	public Transform containerParent;

	public Vector3 iconPosition;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void loaded()
	{
		base.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Game.gameInstance.shader;
		base.GetComponentInChildren<SkinnedMeshRenderer>().material.CopyPropertiesFromMaterial(Game.gameInstance.defaultMaterial);
		Component[] componentsInChildren = base.GetComponentsInChildren<Collider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.Destroy(componentsInChildren[i]);
		}
		GameObject gameObject = new GameObject();
		gameObject.transform.SetParent(this.containerParent);
		base.transform.SetParent(gameObject.transform);
		Bounds maxBounds = Game.GetMaxBounds(base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject);
		Vector3 size = maxBounds.size;
		float num = size.x;
		Vector3 size2 = maxBounds.size;
		if (size2.y > num)
		{
			Vector3 size3 = maxBounds.size;
			num = size3.y;
		}
		Vector3 size4 = maxBounds.size;
		if (size4.z > num)
		{
			Vector3 size5 = maxBounds.size;
			num = size5.z;
		}
		Vector3 zero = Vector3.zero;
		base.transform.localScale = Vector3.one;
		if (num == 0f)
		{
			gameObject.transform.localScale = Vector3.one * 35f;
		}
		else
		{
			gameObject.transform.localScale = Vector3.one * 35f / num;
			float y = zero.y;
			Vector3 size6 = maxBounds.size;
			zero.y = y - size6.z * 0.5f;
		}
		base.transform.localPosition = zero;
		this.iconPosition.z -= 25f;
		gameObject.transform.localPosition = this.iconPosition;
		Game.recursiveSetLayer(gameObject, 5);
	}
}
