using System.Xml.Serialization;

public class FetishPref
{
	[XmlElement("fetish")]
	public string fetish;

	[XmlElement("pref")]
	public float pref;
}
