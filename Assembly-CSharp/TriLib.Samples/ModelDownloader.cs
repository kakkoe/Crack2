using System.Collections;
using System.IO;
using UnityEngine;

namespace TriLib.Samples
{
	public class ModelDownloader : MonoBehaviour
	{
		private const string ModelURI = "https://github.com/assimp/assimp/blob/master/test/models/FBX/spider.fbx?raw=true";

		private const string ModelLocalPath = "/spider.fbx";

		private const float ModelScale = 0.0025f;

		private WWW _www;

		private GUIStyle _centeredStyle;

		private Rect _centeredRect;

		protected void Start()
		{
			base.StartCoroutine(this.DownloadModel());
			this._centeredRect = new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height / 2 - 25), 200f, 50f);
		}

		protected void OnGUI()
		{
			if (!this._www.isDone)
			{
				if (this._centeredStyle == null)
				{
					this._centeredStyle = GUI.skin.GetStyle("Label");
					this._centeredStyle.alignment = TextAnchor.UpperCenter;
				}
				GUI.Label(this._centeredRect, string.Format("Downloaded {0:P2}", this._www.progress), this._centeredStyle);
			}
		}

        private IEnumerator DownloadModel()
        {
            this._www = new WWW("https://github.com/assimp/assimp/blob/master/test/models/FBX/spider.fbx?raw=true");
            while (!this._www.isDone)
            {
                yield return null;
            }
            string fullPath = Application.persistentDataPath + "/spider.fbx";
            File.WriteAllBytes(fullPath, this._www.bytes);
            using (AssetLoader assetLoader = new AssetLoader())
            {
                AssetLoaderOptions assetLoaderOptions = AssetLoaderOptions.CreateInstance();
                assetLoaderOptions.Scale = 0.0025f;
                assetLoader.LoadFromFile(fullPath, assetLoaderOptions, base.gameObject);
            }
            yield break;
        }
    }
}
