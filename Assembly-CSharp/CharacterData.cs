using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterData
{
	[XmlElement("gameVersion")]
	public int gameVersion;

	[XmlElement("uid")]
	public string uid;

	[XmlElement("created")]
	public bool created;

	[XmlElement("customized")]
	public bool customized;

	[XmlElement("identifiesMale")]
	public bool identifiesMale;

	[XmlElement("species")]
	public string species;

	[XmlElement("name")]
	public string name;

	[XmlElement("title")]
	public string title = string.Empty;

	[XmlElement("assetBundleName")]
	public string assetBundleName;

	[XmlElement("headType")]
	public string headType;

	[XmlElement("aggression")]
	public float aggression;

	[XmlElement("genResult")]
	public string genResult = string.Empty;

	[XmlElement("subjectType")]
	public int subjectType;

	[XmlArrayItem("headMorphs")]
	[XmlArray("headMorphs")]
	public List<CharacterDataFloatProperty> headMorphs = new List<CharacterDataFloatProperty>();

	[XmlArrayItem("colorDefinition")]
	[XmlArray("colorDefinitions")]
	public List<ColorDefinition> colorDefinitions = new List<ColorDefinition>();

	[XmlArrayItem("colorDefinitionAlt")]
	[XmlArray("colorDefinitionAlts")]
	public List<ColorAltDefinition> colorDefinitionAlts = new List<ColorAltDefinition>();

	[XmlArrayItem("textureLayer")]
	[XmlArray("textureLayers")]
	public List<TextureLayer> textureLayers = new List<TextureLayer>();

	[XmlArray("embellishmentLayers")]
	[XmlArrayItem("embellishmentLayer")]
	public List<EmbellishmentLayer> embellishmentLayers = new List<EmbellishmentLayer>();

	[XmlArray("embellishmentColors")]
	[XmlArrayItem("embellishmentColor")]
	public List<Color> embellishmentColors = new List<Color>();

	[XmlArray("EmbellishmentColorGradientPoints")]
	[XmlArrayItem("EmbellishmentColorGradientPoint")]
	public List<EmbellishmentColorGradientPoint> embellishmentColorGradientPoints = new List<EmbellishmentColorGradientPoint>();

	[XmlElement("baseColor")]
	public Color baseColor;

	[XmlElement("furType")]
	public string furType;

	[XmlElement("skinType")]
	public string skinType = "skin";

	[XmlElement("rightHanded")]
	public bool rightHanded = true;

	[XmlElement("breastSize")]
	public float breastSize;

	[XmlElement("height")]
	public float height = 0.4f;

	[XmlElement("bodyMass")]
	public float bodyMass = 0.5f;

	[XmlElement("adiposity")]
	public float adiposity;

	[XmlElement("bodyFemininity")]
	public float bodyFemininity = 0.5f;

	[XmlElement("headFemininity")]
	public float headFemininity = 0.5f;

	[XmlElement("hairStyle")]
	public string hairstyle = "none";

	[XmlElement("hairVariant")]
	public int hairvariant;

	[XmlElement("hairColor")]
	public int hairColor;

	[XmlArrayItem("hairAddon")]
	[XmlArray("hairAddons")]
	public List<HairLayer> hairAddons = new List<HairLayer>();

	[XmlElement("furLength")]
	public float furLength = 0.1f;

	[XmlElement("growerShower")]
	public float growerShower = 0.2f;

	[XmlElement("penisCurveX")]
	public float penisCurveX = 0.5f;

	[XmlElement("penisCurveY")]
	public float penisCurveY = 0.6f;

	[XmlElement("penisLength")]
	public float penisLength = 0.5f;

	[XmlElement("penisGirth")]
	public float penisGirth = 0.5f;

	[XmlElement("penisSize")]
	public float penisSize = 0.5f;

	[XmlElement("ballSize")]
	public float ballSize = 0.5f;

	[XmlElement("scrotumLength")]
	public float scrotumLength = 0.5f;

	[XmlElement("breastPerk")]
	public float breastPerk = 0.1f;

	[XmlElement("nippleSize")]
	public float nippleSize = 0.1f;

	[XmlElement("vaginaPlumpness")]
	public float vaginaPlumpness = 0.3f;

	[XmlElement("vaginaShape")]
	public float vaginaShape = 0.2f;

	[XmlElement("clitSize")]
	public float clitSize = 0.2f;

	[XmlElement("belly")]
	public float belly;

	[XmlElement("muscle")]
	public float muscle;

	[XmlElement("hipWidth")]
	public float hipWidth;

	[XmlElement("buttSize")]
	public float buttSize;

	[XmlElement("tailFlick")]
	public bool tailFlick;

	[XmlElement("tailWag")]
	public bool tailWag;

	[XmlElement("tailTuck")]
	public bool tailTuck;

	[XmlElement("tailCurlX")]
	public float tailCurlX = 0.5f;

	[XmlElement("tailCurlY")]
	public float tailCurlY;

	[XmlElement("tailTaper")]
	public float tailTaper;

	[XmlElement("tailThickness")]
	public float tailThickness = 0.5f;

	[XmlElement("tailFurDensity")]
	public float tailFurDensity = 0.5f;

	[XmlElement("tailTipSize")]
	public float tailTipSize = 0.5f;

	[XmlElement("tailFurType")]
	public int tailFurType;

	[XmlElement("tailFurLength")]
	public float tailFurLength = 0.5f;

	[XmlArray("tailFurSizes")]
	[XmlArrayItem("tailFurSize")]
	public List<float> tailFurSizes = new List<float>();

	public float tailLength = 1f;

	[XmlElement("tailStiffness")]
	public float tailStiffness = 0.3f;

	[XmlElement("tailLift")]
	public float tailLift = 0.4f;

	[XmlElement("tailSize")]
	public float tailSize = 0.4f;

	[XmlElement("prehensileTail")]
	public bool prehensileTail;

	[XmlElement("genitalType")]
	public int genitalType;

	[XmlElement("penisType")]
	public int penisType;

	[XmlElement("ballsType")]
	public int ballsType;

	[XmlElement("specialFoot")]
	public string specialFoot = string.Empty;

	[XmlElement("legType")]
	public int legType;

	[XmlElement("numToes")]
	public int numToes = 4;

	[XmlElement("numFingers")]
	public int numFingers = 5;

	[XmlElement("specialHands")]
	public string specialHands = string.Empty;

	[XmlElement("wingType")]
	public int wingType;

	[XmlElement("wingSize")]
	public float wingSize = 1f;

	[XmlElement("hasKnot")]
	public bool hasKnot;

	[XmlElement("hasSheath")]
	public bool hasSheath;

	[XmlElement("avatarPixels")]
	public string avatarPixels;

	[XmlElement("negativeExperienceModifier")]
	public float negativeExperienceModifier = 5f;

	[XmlElement("cumVolume")]
	public float cumVolume = 1f;

	[XmlElement("squirtAmount")]
	public float squirtAmount = 0.2f;

	[XmlElement("cumSpurtStrength")]
	public float cumSpurtStrength = 1f;

	[XmlElement("cumSpurtFrequency")]
	public float cumSpurtFrequency = 1f;

	[XmlElement("stamina")]
	public float stamina = 1f;

	[XmlElement("orgasmDuration")]
	public float orgasmDuration = 1f;

	[XmlElement("orgasmAnticipationFactor")]
	public float orgasmAnticipationFactor = 0.2f;

	[XmlElement("refractoryDuration")]
	public float refractoryDuration = 1f;

	[XmlElement("orgasmSensitivity")]
	public float orgasmSensitivity = 0.2f;

	[XmlElement("refractorySensitivity")]
	public float refractorySensitivity = 3f;

	[XmlElement("proximitySensitivity")]
	public float proximitySensitivity = 0.1f;

	[XmlElement("sensitivity")]
	public float sensitivity = 1f;

	[XmlElement("precumThreshold")]
	public float precumThreshold = 0.8f;

	[XmlElement("wetnessThreshold")]
	public float wetnessThreshold = 0.4f;

	[XmlElement("tailholeTightness")]
	public float tailholeTightness = 0.5f;

	[XmlElement("vaginalTightness")]
	public float vaginalTightness = 0.5f;

	[XmlElement("analPleasure")]
	public float analPleasure = 1f;

	[XmlArrayItem("preference")]
	[XmlArray("preferences")]
	public List<SexualPreferenceValue> preferences = new List<SexualPreferenceValue>();

	[XmlArrayItem("confidence")]
	[XmlArray("confidences")]
	public List<ConfidenceValue> confidences = new List<ConfidenceValue>();

	[XmlElement("mod_cumSpurtRatio")]
	public float mod_cumSpurtRatio = 0.5f;
}
