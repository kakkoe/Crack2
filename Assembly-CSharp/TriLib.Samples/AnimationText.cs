using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TriLib.Samples
{
	public class AnimationText : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		public string Text
		{
			get
			{
				return base.GetComponent<Text>().text;
			}
			set
			{
				base.GetComponent<Text>().text = value;
			}
		}

		public void OnSelect(BaseEventData eventData)
		{
			AssetLoaderWindow.Instance.HandleEvent(this.Text);
		}
	}
}
