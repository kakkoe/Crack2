using System.Collections.Generic;
using System.Xml.Serialization;

public class LabItemDefinition
{
	[XmlElement("category")]
	public string category;

	[XmlElement("assetName")]
	public string assetName;

	[XmlElement("displayName")]
	public string displayName;

	[XmlElement("description")]
	public string description;

	[XmlElement("unlockCTA")]
	public string unlockCTA;

	[XmlElement("cost")]
	public int cost;

	[XmlElement("bagData")]
	public BagData bagData;

	[XmlElement("limitGroup")]
	public string limitGroup = string.Empty;

	[XmlElement("limitInRoom")]
	public int limitInRoom;

	[XmlElement("limitInPit")]
	public int limitInPit;

	[XmlElement("limitOverall")]
	public int limitOverall;

	[XmlElement("intersectionsAllowed")]
	public int intersectionsAllowed;

	[XmlElement("placementDescription")]
	public string placementDescription = string.Empty;

	[XmlElement("rotationAllowed")]
	public float rotationAllowed;

	[XmlElement("deliveryTime")]
	public float deliveryTime = 60f;

	[XmlElement("deliveryLocation")]
	public string deliveryLocation = "garage";

	[XmlArray("validSurfaces")]
	[XmlArrayItem("surface")]
	public List<LabItemPlacementSurface> validSurfaces = new List<LabItemPlacementSurface>();

	[XmlArray("clipFixes")]
	[XmlArrayItem("region")]
	public List<string> clipFixes = new List<string>();

	[XmlArrayItem("slot")]
	[XmlArray("equipSlots")]
	public List<LabItemEquipmentSlot> equipSlots = new List<LabItemEquipmentSlot>();

	[XmlArrayItem("slot")]
	[XmlArray("sexToySlots")]
	public List<string> sexToySlots = new List<string>();

	[XmlElement("requiresResearch")]
	public bool requiresResearch = true;

	[XmlArrayItem("shop")]
	[XmlArray("shops")]
	public List<string> shops = new List<string>();

	[XmlArrayItem("component")]
	[XmlArray("components")]
	public List<string> components = new List<string>();

	[XmlArrayItem("chemicalcost")]
	[XmlArray("chemicalcosts")]
	public List<int> chemicalcosts = new List<int>();
}
