using System.Collections;
using System.IO;
using UnityEngine;

public class AppendageBundle
{
	public string url;

	public bool loaded;

	public AssetBundle bundle;

	public void beginLoad()
	{
		Game.gameInstance.StartCoroutine(this.loadBundle());
	}

	public IEnumerator loadBundle()
	{
		if (!((Object)this.bundle != (Object)null))
		{
			if (!Caching.ready)
			{
				yield return (object)null;
			}
			if (!File.Exists(this.url))
			{
				UserSettings.rebuildGameAssets();
			}
            using (WWW www = new WWW("file:///" + this.url))
            {
                if (!www.isDone)
                {
                    Game.gameInstance.recentThinking = 1f;
                    yield return (object)null;
                }
                yield return (object)www;
                bool flag5 = !string.IsNullOrEmpty(www.error);
                if (flag5)
                {
                    this.loaded = false;
                    throw new System.Exception("Error downloading appendage bundle " + this.url + ": " + www.error);
                }
                this.bundle = www.assetBundle;
                this.loaded = true;
            }
		}
		yield break;
	}
}
