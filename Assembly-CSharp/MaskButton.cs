using UnityEngine;

public class MaskButton : MonoBehaviour
{
	public int id;

	public TextureLayerUI owner;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DeleteClicked()
	{
		this.owner.deleteMask(this.id);
	}

	public void NewClicked()
	{
		this.owner.addMask();
	}

	public void ToggleModeClicked()
	{
		this.owner.toggleMaskMode(this.id);
	}

	public void ToggleInvertClicked()
	{
		this.owner.toggleInvertMode(this.id);
	}
}
