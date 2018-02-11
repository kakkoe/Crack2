using UnityEngine;

public class ChemicalCounters : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Inventory.data != null)
		{
			((Component)base.transform.Find("txtR")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.chemicals[0]);
			((Component)base.transform.Find("txtO")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.chemicals[1]);
			((Component)base.transform.Find("txtY")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.chemicals[2]);
			((Component)base.transform.Find("txtG")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.chemicals[3]);
			((Component)base.transform.Find("txtB")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.chemicals[4]);
			((Component)base.transform.Find("txtP")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.chemicals[5]);
			((Component)base.transform.Find("txtSpecimen")).GetComponent<TextMesh>().text = this.formatNumber(Inventory.data.totalSpecimen);
			((Component)base.transform.Find("txtSpecimenCapacity")).GetComponent<TextMesh>().text = Mathf.FloorToInt(Inventory.data.totalSpecimen / (float)LayoutManager.determineSpecimenCapacity() * 100f) + "% " + Localization.getPhrase("CAPACITY", string.Empty).ToUpper();
			((Component)base.transform.Find("txtRlabel")).GetComponent<TextMesh>().text = Localization.getPhrase("CHEMICAL_RED", string.Empty);
			((Component)base.transform.Find("txtOlabel")).GetComponent<TextMesh>().text = Localization.getPhrase("CHEMICAL_ORANGE", string.Empty);
			((Component)base.transform.Find("txtYlabel")).GetComponent<TextMesh>().text = Localization.getPhrase("CHEMICAL_YELLOW", string.Empty);
			((Component)base.transform.Find("txtGlabel")).GetComponent<TextMesh>().text = Localization.getPhrase("CHEMICAL_GREEN", string.Empty);
			((Component)base.transform.Find("txtBlabel")).GetComponent<TextMesh>().text = Localization.getPhrase("CHEMICAL_BLUE", string.Empty);
			((Component)base.transform.Find("txtPlabel")).GetComponent<TextMesh>().text = Localization.getPhrase("CHEMICAL_PURPLE", string.Empty);
			((Component)base.transform.Find("txtSpecimenLabel")).GetComponent<TextMesh>().text = Localization.getPhrase("RAW_SPECIMEN", string.Empty);
			int status = 0;
			if (Inventory.data.totalSpecimen > 0f)
			{
				status = ((!Centrifuge.anythingActuallyProcessing) ? 1 : 2);
			}
			((Component)base.transform.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = status;
		}
	}

	private string formatNumber(float n)
	{
		string text = ((float)Mathf.RoundToInt(n * 10f) / 10f).ToString();
		if (!text.Contains("."))
		{
			text += ".0";
		}
		while (text.Length < 3)
		{
			text = "0" + text;
		}
		return text;
	}
}
