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

	[XmlArrayItem("chemical")]
	[XmlArray("chemicals")]
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

	[XmlArrayItem("NPC")]
	[XmlArray("NPCs")]
	public List<NPCAssignment> NPCs;

	[XmlArrayItem("charVar")]
	[XmlArray("charVars")]
	public List<UserVar> charVars = new List<UserVar>();

	[XmlArray("bags")]
	[XmlArrayItem("bag")]
	public List<Bag> bags = new List<Bag>();

	[XmlArrayItem("subjectbag")]
	[XmlArray("subjectbags")]
	public List<string> subjectbags = new List<string>();

	public string draggingItem = string.Empty;

	[XmlArrayItem("hotkeyItem")]
	[XmlArray("hotkeyItems")]
	public List<string> hotkeyItems = new List<string>();

	[XmlArrayItem("favoriteCharacter")]
	[XmlArray("favoriteCharacters")]
	public List<string> favoriteCharacters;

	[XmlArrayItem("researchTask")]
	[XmlArray("researchTasks")]
	public List<ResearchTask> researchTasks;

	[XmlElement("researchCompletion")]
	public float researchCompletion;

	[XmlArray("orders")]
	[XmlArrayItem("order")]
	public List<ItemOrder> orders = new List<ItemOrder>();

	[XmlArrayItem("chemicalcompound")]
	[XmlArray("chemicalcompounds")]
	public List<ChemicalCompound> chemicalcompounds = new List<ChemicalCompound>();
}
