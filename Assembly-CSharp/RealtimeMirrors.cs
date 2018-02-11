using UnityEngine;

public class RealtimeMirrors : MonoBehaviour
{
	private Vector3 v3;

	private Vector3 center = default(Vector3);

	private float dist;

	private int id;

	public static int nextID;

	public static float lowestDist;

	public static int lowestID;

	public static int lastLowest;

	private void Start()
	{
		this.center.x = 0.5f;
		this.center.y = 0.5f;
		this.center.z = 0f;
		this.id = RealtimeMirrors.nextID;
		RealtimeMirrors.nextID++;
	}

	public static void process()
	{
		RealtimeMirrors.lastLowest = RealtimeMirrors.lowestID;
		RealtimeMirrors.lowestDist = 10000f;
		RealtimeMirrors.lowestID = -1;
	}

	private void Update()
	{
		this.v3 = Game.gameInstance.mainCam.transform.InverseTransformPoint(base.transform.position);
		if (this.v3.z > 0f)
		{
			this.dist = (Game.gameInstance.worldToScreen(base.transform.position, true, 3000f) - this.center).magnitude;
			if (this.dist < RealtimeMirrors.lowestDist)
			{
				RealtimeMirrors.lowestDist = this.dist;
				RealtimeMirrors.lowestID = this.id;
			}
		}
		base.GetComponent<MeshRenderer>().enabled = (this.id == RealtimeMirrors.lastLowest);
		base.GetComponent<PlanarRealtimeReflection>().enabled = (this.id == RealtimeMirrors.lastLowest);
	}
}
