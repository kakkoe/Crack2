using System.Collections.Generic;
using System.Xml.Serialization;

public class ResearchTask
{
	[XmlAttribute("id")]
	public string id;

	[XmlAttribute("category")]
	public string category;

	[XmlAttribute("fetish")]
	public string fetish;

	[XmlArray("solutionPoints")]
	[XmlArrayItem("solutionPoint")]
	public List<int> solutionPoints = new List<int>(128);

	[XmlArray("guesses")]
	[XmlArrayItem("guess")]
	public List<int> guesses = new List<int>(128);

	[XmlAttribute("completed")]
	public bool completed;

	[XmlAttribute("completionAmount")]
	public float completionAmount;

	[XmlAttribute("globePositionX")]
	public float globePositionX;

	[XmlAttribute("globePositionY")]
	public float globePositionY;

	[XmlAttribute("value")]
	public int value;

	[XmlAttribute("type")]
	public int type;
}
