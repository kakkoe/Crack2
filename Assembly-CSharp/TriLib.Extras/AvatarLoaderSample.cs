using System.IO;
using System.Linq;
using UnityEngine;

namespace TriLib.Extras
{
	[ExecuteInEditMode]
	public class AvatarLoaderSample : MonoBehaviour
	{
		public GameObject FreeLookCamPrefab;

		public GameObject ThirdPersonControllerPrefab;

		public GameObject ActiveCameraGameObject;

		public string ModelsDirectory = "Models";

		private string[] _files;

		private Rect _windowRect;

		private Vector3 _scrollPosition;

		private AvatarLoader _avatarLoader;

		protected void Start()
		{
			this._avatarLoader = Object.FindObjectOfType<AvatarLoader>();
			if (!((Object)this._avatarLoader == (Object)null))
			{
				string path = Path.Combine(Path.GetFullPath("."), this.ModelsDirectory);
				string supportedExtensions = AssetLoader.GetSupportedFileExtensions();
				this._files = (from x in Directory.GetFiles(path, "*.*")
				where supportedExtensions.Contains(Path.GetExtension(x).ToLower())
				select x).ToArray();
				this._windowRect = new Rect(20f, 20f, 240f, (float)(Screen.height - 40));
			}
		}

		protected void OnGUI()
		{
			if (this._files != null && !((Object)this._avatarLoader == (Object)null) && !((Object)this.FreeLookCamPrefab == (Object)null) && !((Object)this.ThirdPersonControllerPrefab == (Object)null))
			{
				this._windowRect = GUI.Window(0, this._windowRect, this.HandleWindowFunction, "Available Models");
			}
		}

		private void HandleWindowFunction(int id)
		{
			GUILayout.BeginVertical();
			this._scrollPosition = GUILayout.BeginScrollView(this._scrollPosition);
			string[] files = this._files;
			foreach (string text in files)
			{
				if (GUILayout.Button(Path.GetFileName(text)))
				{
					GameObject gameObject = Object.Instantiate(this.ThirdPersonControllerPrefab);
					gameObject.transform.DestroyChildren(true);
					if (this._avatarLoader.LoadAvatar(text, gameObject))
					{
						if ((Object)this.ActiveCameraGameObject != (Object)null)
						{
							Object.Destroy(this.ActiveCameraGameObject.gameObject);
						}
						this.ActiveCameraGameObject = Object.Instantiate(this.FreeLookCamPrefab);
					}
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
		}
	}
}
