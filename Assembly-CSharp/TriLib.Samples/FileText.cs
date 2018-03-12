using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TriLib.Samples
{
	public class FileText : MonoBehaviour, ISelectHandler, IEventSystemHandler
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

		public ItemType ItemType
		{
			get;
			set;
		}

		public void OnSelect(BaseEventData eventData)
		{
			FileOpenDialog.Instance.HandleEvent(this.ItemType, this.Text);
		}
	}
}
