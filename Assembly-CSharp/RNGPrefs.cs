using System.Xml.Serialization;

public class RNGPrefs
{
	[XmlElement("negativeExperienceModifier")]
	public RNGRange negativeExperienceModifier = new RNGRange(1f, 10f, 3f, 0);

	[XmlElement("height")]
	public RNGRange height = new RNGRange(0f, 0f, 0f, -1);

	[XmlElement("breastSize")]
	public RNGRange breastSize = new RNGRange(0f, 0f, 0f, -1);

	[XmlElement("growerShower")]
	public RNGRange growerShower = new RNGRange(0.1f, 1f, 2f, 0);

	[XmlElement("penisCurveX")]
	public RNGRange penisCurveX = new RNGRange(0.2f, 0.8f, 6f, 1);

	[XmlElement("penisCurveY")]
	public RNGRange penisCurveY = new RNGRange(0.3f, 0.7f, 4f, 1);

	[XmlElement("penisLength")]
	public RNGRange penisLength = new RNGRange(0f, 1f, 3f, 1);

	[XmlElement("penisGirth")]
	public RNGRange penisGirth = new RNGRange(0f, 1f, 3f, 1);

	[XmlElement("penisSize")]
	public RNGRange penisSize = new RNGRange(0f, 1f, 3f, 1);

	[XmlElement("ballSize")]
	public RNGRange ballSize = new RNGRange(0.15f, 0.85f, 4f, 1);

	[XmlElement("tailholeTightness")]
	public RNGRange tailholeTightness = new RNGRange(0f, 1f, 3f, 1);

	[XmlElement("vaginalTightness")]
	public RNGRange vaginalTightness = new RNGRange(0f, 0.6f, 3f, 1);

	[XmlElement("scrotumLength")]
	public RNGRange scrotumLength = new RNGRange(0f, 1f, 3f, 0);

	[XmlElement("breastPerk")]
	public RNGRange breastPerk = new RNGRange(0f, 1f, 3f, 0);

	[XmlElement("nippleSize")]
	public RNGRange nippleSize = new RNGRange(0f, 1f, 3f, 0);

	[XmlElement("vaginaPlumpness")]
	public RNGRange vaginaPlumpness = new RNGRange(0f, 1f, 1f, 0);

	[XmlElement("vaginaShape")]
	public RNGRange vaginaShape = new RNGRange(0f, 1f, 1f, 0);

	[XmlElement("wetnessThreshold")]
	public RNGRange wetnessThreshold;

	[XmlElement("clitSize")]
	public RNGRange clitSize;

	[XmlElement("cumVolume")]
	public RNGRange cumVolume;

	[XmlElement("cumSpurtStrength")]
	public RNGRange cumSpurtStrength;

	[XmlElement("cumSpurtRatio")]
	public RNGRange cumSpurtRatio;

	[XmlElement("cumSpurtFrequency")]
	public RNGRange cumSpurtFrequency;

	[XmlElement("precumThreshold")]
	public RNGRange precumThreshold;

	[XmlElement("stamina")]
	public RNGRange stamina;

	[XmlElement("orgasmDuration")]
	public RNGRange orgasmDuration;

	[XmlElement("orgasmAnticipationFactor")]
	public RNGRange orgasmAnticipationFactor;

	[XmlElement("refractoryDuration")]
	public RNGRange refractoryDuration;

	[XmlElement("orgasmSensitivity")]
	public RNGRange orgasmSensitivity;

	[XmlElement("refractorySensitivity")]
	public RNGRange refractorySensitivity;

	[XmlElement("sensitivity")]
	public RNGRange sensitivity;

	[XmlElement("proximitySensitivity")]
	public RNGRange proximitySensitivity;

	[XmlElement("analPleasure")]
	public RNGRange analPleasure;

	[XmlElement("squirtAmount")]
	public RNGRange squirtAmount = new RNGRange(0f, 1f, 2f, 0);

	public RNGPrefs()
	{
		this.wetnessThreshold = new RNGRange(0.1f, 0.7f, 2f, 1);
		this.clitSize = new RNGRange(0f, 1f, 3f, 0);
		this.cumVolume = new RNGRange(0.7f, 1.1f, 2f, 1);
		this.cumSpurtStrength = new RNGRange(0.8f, 1.6f, 2f, 1);
		this.cumSpurtRatio = new RNGRange(0f, 1f, 3f, -1);
		this.cumSpurtFrequency = new RNGRange(0.7f, 1.3f, 2f, 1);
		this.precumThreshold = new RNGRange(0.6f, 1f, 2f, 1);
		this.stamina = new RNGRange(0.3f, 1.7f, 2f, 1);
		this.orgasmDuration = new RNGRange(0.7f, 1.3f, 2f, 1);
		this.orgasmAnticipationFactor = new RNGRange(0f, 0.9f, 3f, 0);
		this.refractoryDuration = new RNGRange(0.1f, 1.9f, 5f, 1);
		this.orgasmSensitivity = new RNGRange(0f, 2f, 1f, 0);
		this.refractorySensitivity = new RNGRange(0f, 4f, 2f, 2);
		this.sensitivity = new RNGRange(0.7f, 1.3f, 2f, 1);
		this.proximitySensitivity = new RNGRange(0f, 1f, 2f, 0);
		this.analPleasure = new RNGRange(0.3f, 1.7f, 2f, 1);
	}
}
