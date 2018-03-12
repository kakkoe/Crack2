using System.Collections;
using UnityEngine;

public class LODSphere : MonoBehaviour
{
	private ImperialFurLOD lodScript;

	private LODDemo demoScript;

	private bool lodEnabled = true;

	private Material material;

	private string shaderBase;

    private IEnumerator Start()
    {
        this.lodScript = base.GetComponent<ImperialFurLOD>();
        this.demoScript = GameObject.Find("LODDemo").GetComponent<LODDemo>();
        this.material = base.gameObject.GetComponent<Renderer>().material;
        int index = this.material.shader.name.LastIndexOf('/');
        this.shaderBase = this.material.shader.name.Substring(0, index + 1);
        Vector3 pointA = base.transform.position;
        Vector3 pointB = base.transform.position - new Vector3(0f, 0f, 10f);
        for (; ; )
        {
            yield return base.StartCoroutine(this.MoveObject(base.transform, pointA, pointB, 3f));
            yield return base.StartCoroutine(this.MoveObject(base.transform, pointB, pointA, 3f));
        }
        yield break;
    }

    private void Update()
	{
		if (this.demoScript.lodOn && !this.lodEnabled)
		{
			this.lodEnabled = true;
			this.lodScript.enabled = true;
			this.lodScript.Reset();
		}
		if (!this.demoScript.lodOn && this.lodEnabled)
		{
			this.lodEnabled = false;
			this.lodScript.enabled = false;
			this.material.shader = Shader.Find(this.shaderBase + "40 Shell");
		}
	}

    private IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 0.5f / time;
        while (i < 1f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        yield break;
    }
}
