using UnityEngine;

public class RackCamera : MonoBehaviour
{
	private Game game;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPostRender()
	{
		Game.gameInstance.postRender();
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		if (otherCollider.tag == "Zone")
		{
			if ((Object)this.game == (Object)null)
			{
				this.game = Game.gameInstance;
			}
			if ((Object)this.game != (Object)null)
			{
				this.game.currentZone = otherCollider.name;
			}
		}
	}
}
