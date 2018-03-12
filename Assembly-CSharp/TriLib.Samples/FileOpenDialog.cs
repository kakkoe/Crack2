using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace TriLib.Samples
{
	public class FileOpenDialog : MonoBehaviour
	{
		public string Filter = "*.*";

		[SerializeField]
		private Transform _containerTransform;

		[SerializeField]
		private FileText _fileTextPrefab;

		[SerializeField]
		private GameObject _fileLoaderRenderer;

		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private Text _headerText;

		private string _directory;

		public static FileOpenDialog Instance
		{
			get;
			private set;
		}

		public string Title
		{
			get
			{
				return this._headerText.text;
			}
			set
			{
				this._headerText.text = value;
			}
		}

        private event FileOpenEventHandle OnFileOpen;

		public void ShowFileOpenDialog(FileOpenEventHandle onFileOpen)
		{
			this.OnFileOpen = onFileOpen;
			this.ReloadItemNames();
			this._fileLoaderRenderer.SetActive(true);
		}

		public void HideFileOpenDialog()
		{
			this.DestroyItems();
			this._fileLoaderRenderer.SetActive(false);
		}

		public void HandleEvent(ItemType itemType, string filename)
		{
			switch (itemType)
			{
			case ItemType.ParentDirectory:
			{
				DirectoryInfo parent = Directory.GetParent(this._directory);
				if (parent != null)
				{
					this._directory = parent.FullName;
					this.ReloadItemNames();
				}
				else
				{
					this.ShowDirectoryNames();
				}
				break;
			}
			case ItemType.Directory:
				this._directory = filename;
				this.ReloadItemNames();
				break;
			default:
				this.OnFileOpen(Path.Combine(this._directory, filename));
				this.HideFileOpenDialog();
				break;
			}
		}

		public void DestroyItems()
		{
			IEnumerator enumerator = this._containerTransform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		protected void Awake()
		{
			this._directory = Path.GetDirectoryName(Application.dataPath);
			this._closeButton.onClick.AddListener(this.HideFileOpenDialog);
			FileOpenDialog.Instance = this;
		}

		private void ReloadItemNames()
		{
			this.DestroyItems();
			this.CreateItem(ItemType.ParentDirectory, "[Parent Directory]");
			string[] directories = Directory.GetDirectories(this._directory);
			string[] array = directories;
			foreach (string text in array)
			{
				this.CreateItem(ItemType.Directory, text);
			}
			string[] array2 = Directory.GetFiles(this._directory, "*.*");
			if (!string.IsNullOrEmpty(this.Filter) && this.Filter != "*.*")
			{
				array2 = (from x in array2
				where this.Filter.Contains(Path.GetExtension(x).ToLower())
				select x).ToArray();
			}
			string[] array3 = array2;
			foreach (string path in array3)
			{
				this.CreateItem(ItemType.File, Path.GetFileName(path));
			}
		}

		private void ShowDirectoryNames()
		{
			this.DestroyItems();
			string[] logicalDrives = Directory.GetLogicalDrives();
			string[] array = logicalDrives;
			foreach (string text in array)
			{
				this.CreateItem(ItemType.Directory, text);
			}
		}

		private void CreateItem(ItemType itemType, string text)
		{
			FileText fileText = UnityEngine.Object.Instantiate(this._fileTextPrefab, this._containerTransform);
			fileText.ItemType = itemType;
			fileText.Text = text;
		}
	}
}
