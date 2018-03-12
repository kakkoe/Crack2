using UnityEngine;
using UnityEngine.UI;

namespace TriLib.Samples
{
	public class ErrorDialog : MonoBehaviour
	{
		[SerializeField]
		private Button _okButton;

		[SerializeField]
		private InputField _errorText;

		[SerializeField]
		private GameObject _rendererGameObject;

		public static ErrorDialog Instance
		{
			get;
			private set;
		}

		public string Text
		{
			get
			{
				return this._errorText.text;
			}
			set
			{
				this._errorText.text = value;
			}
		}

		protected void Awake()
		{
			this._okButton.onClick.AddListener(this.HideDialog);
			ErrorDialog.Instance = this;
		}

		public void ShowDialog(string text)
		{
			this.Text = text;
			this._rendererGameObject.SetActive(true);
		}

		public void HideDialog()
		{
			this._rendererGameObject.SetActive(false);
		}
	}
}
