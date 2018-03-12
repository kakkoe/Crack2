using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TriLib.Samples
{
	public class AssetLoaderWindow : MonoBehaviour
	{
		[SerializeField]
		private Button _loadAssetButton;

		[SerializeField]
		private Text _spinningText;

		[SerializeField]
		private Toggle _spinXToggle;

		[SerializeField]
		private Toggle _spinYToggle;

		[SerializeField]
		private Button _resetRotationButton;

		[SerializeField]
		private Button _stopAnimationButton;

		[SerializeField]
		private Text _animationsText;

		[SerializeField]
		private ScrollRect _animationsScrollRect;

		[SerializeField]
		private Transform _containerTransform;

		[SerializeField]
		private AnimationText _animationTextPrefab;

		[SerializeField]
		private Canvas _backgroundCanvas;

		private GameObject _rootGameObject;

		public static AssetLoaderWindow Instance
		{
			get;
			private set;
		}

		public void HandleEvent(string animationName)
		{
			this._rootGameObject.GetComponent<Animation>().Play(animationName);
			this._stopAnimationButton.interactable = true;
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
			this._loadAssetButton.onClick.AddListener(this.LoadAssetButtonClick);
			this._stopAnimationButton.onClick.AddListener(this.StopAnimationButtonClick);
			this._resetRotationButton.onClick.AddListener(this.ResetRotationButtonClick);
			this.HideControls();
			AssetLoaderWindow.Instance = this;
		}

		protected void Update()
		{
			if ((UnityEngine.Object)this._rootGameObject != (UnityEngine.Object)null)
			{
				this._rootGameObject.transform.Rotate((!this._spinXToggle.isOn) ? 0f : (20f * Time.deltaTime), (!this._spinYToggle.isOn) ? 0f : (-20f * Time.deltaTime), 0f, Space.World);
			}
		}

		private void HideControls()
		{
			this._spinningText.gameObject.SetActive(false);
			this._spinXToggle.gameObject.SetActive(false);
			this._spinYToggle.gameObject.SetActive(false);
			this._resetRotationButton.gameObject.SetActive(false);
			this._stopAnimationButton.gameObject.SetActive(false);
			this._animationsText.gameObject.SetActive(false);
			this._animationsScrollRect.gameObject.SetActive(false);
		}

		private void LoadAssetButtonClick()
		{
			FileOpenDialog instance = FileOpenDialog.Instance;
			instance.Title = "Please select a File";
			instance.Filter = AssetLoader.GetSupportedFileExtensions();
			instance.ShowFileOpenDialog(delegate(string filename)
			{
				this.HideControls();
				if ((UnityEngine.Object)this._rootGameObject != (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(this._rootGameObject);
					this._rootGameObject = null;
				}
				AssetLoaderOptions assetLoaderOptions = AssetLoaderOptions.CreateInstance();
				assetLoaderOptions.DontLoadCameras = false;
				assetLoaderOptions.DontLoadLights = false;
				using (AssetLoader assetLoader = new AssetLoader())
				{
					try
					{
						this._rootGameObject = assetLoader.LoadFromFile(filename, assetLoaderOptions, null);
					}
					catch (Exception ex)
					{
						ErrorDialog.Instance.ShowDialog(ex.ToString());
					}
				}
				if ((UnityEngine.Object)this._rootGameObject != (UnityEngine.Object)null)
				{
					Camera main = Camera.main;
					main.FitToBounds(this._rootGameObject.transform, 3f);
					this._backgroundCanvas.planeDistance = main.farClipPlane * 0.99f;
					this._spinningText.gameObject.SetActive(true);
					this._spinXToggle.isOn = false;
					this._spinXToggle.gameObject.SetActive(true);
					this._spinYToggle.isOn = false;
					this._spinYToggle.gameObject.SetActive(true);
					this._resetRotationButton.gameObject.SetActive(true);
					this.DestroyItems();
					Animation component = this._rootGameObject.GetComponent<Animation>();
					if ((UnityEngine.Object)component != (UnityEngine.Object)null)
					{
						this._animationsText.gameObject.SetActive(true);
						this._animationsScrollRect.gameObject.SetActive(true);
						this._stopAnimationButton.gameObject.SetActive(true);
						this._stopAnimationButton.interactable = false;
						IEnumerator enumerator = component.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								AnimationState animationState = (AnimationState)enumerator.Current;
								this.CreateItem(animationState.name);
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
				}
			});
		}

		private void CreateItem(string text)
		{
			AnimationText animationText = UnityEngine.Object.Instantiate(this._animationTextPrefab, this._containerTransform);
			animationText.Text = text;
		}

		private void ResetRotationButtonClick()
		{
			this._spinXToggle.isOn = false;
			this._spinYToggle.isOn = false;
			this._rootGameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}

		private void StopAnimationButtonClick()
		{
			this._rootGameObject.GetComponent<Animation>().Stop();
			this._stopAnimationButton.interactable = false;
		}
	}
}
