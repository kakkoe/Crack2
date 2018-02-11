using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("InventoryData")]
public class InventoryData
{
	[XmlArray("completedResearchTasks")]
	[XmlArrayItem("completedResearchTask")]
	public List<string> completedResearch = new List<string>();

	[XmlElement("freeplay")]
	public bool freeplay;

	[XmlElement("money")]
	public int money;

	[XmlElement("specimen")]
	public float[] specimen = new float[6];

	[XmlElement("totalSpecimen")]
	public float totalSpecimen;

	[XmlArray("chemicals")]
	[XmlArrayItem("chemical")]
	public float[] chemicals = new float[6];

	[XmlElement("secondsPlayed")]
	public int secondsPlayed;

	[XmlElement("profilePic")]
	public string profilePic;

	[XmlElement("politeName")]
	public string politeName = string.Empty;

	[XmlElement("domName")]
	public string domName = string.Empty;

	[XmlElement("characterName")]
	public string characterName = string.Empty;

	[XmlElement("labReputation")]
	public float labReputation;

	[XmlArray("NPCs")]
	[XmlArrayItem("NPC")]
	public List<NPCAssignment> NPCs;

	[XmlArray("charVars")]
	[XmlArrayItem("charVar")]
	public List<UserVar> charVars = new List<UserVar>();

	[XmlArray("bags")]
	[XmlArrayItem("bag")]
	public List<Bag> bags = new List<Bag>();

	[XmlArray("subjectbags")]
	[XmlArrayItem("subjectbag")]
	public List<string> subjectbags = new List<string>();

	public string draggingItem = string.Empty;

	[XmlArray("hotkeyItems")]
	[XmlArrayItem("hotkeyItem")]
	public List<string> hotkeyItems = new List<string>();

	[XmlArray("favoriteCharacters")]
	[XmlArrayItem("favoriteCharacter")]
	public List<string> favoriteCharacters;

	[XmlArray("researchTasks")]
	[XmlArrayItem("researchTask")]
	public List<ResearchTask> researchTasks;

	[XmlElement("researchCompletion")]
	public float researchCompletion;

	[XmlArray("orders")]
	[XmlArrayItem("order")]
	public List<ItemOrder> orders = new List<ItemOrder>();

	[XmlArray("chemicalcompounds")]
	[XmlArrayItem("chemicalcompound")]
	public List<ChemicalCompound> chemicalcompounds = new List<ChemicalCompound>();
}
