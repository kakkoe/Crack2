using UnityEngine;

public class MainCam : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnPostRender()
	{
		Game.gameInstance.postRender();
	}
}
