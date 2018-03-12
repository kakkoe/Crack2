using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class RandomCharacterGenerator
{
	public static List<GeneratorWeight> weights;

	public static string forceSpecies;

	public static string randColClass;

	public static List<UnityEngine.Color> naturalEyeColors;

	public static List<string> baseLayers;

	public static List<string> lightLayers;

	public static List<string> darkLayers;

	public static List<string> accentLayers;

	public static List<string> accentTextures;

	public static int selectedOddity;

	public static bool glowAccents;

	public static UnityEngine.Color accentColor;

	public static Vector4[] threadedImagePixels;

	public static UnityEngine.Color[] pix;

	public static System.Drawing.Color curPix;

	public static BitmapData bitmapData;

	public static Bitmap image;

	public static int bytesPerPixel;

	public static int byteCount;

	public static byte[] pixels;

	public static IntPtr ptrFirstPixel;

	public static int heightInPixels;

	public static int widthInBytes;

	public static void wipe()
	{
		RandomCharacterGenerator.weights = new List<GeneratorWeight>();
		RandomCharacterGenerator.setGenderWeights(Mathf.Pow(UserSettings.data.genderPreferences[1], 2f), Mathf.Pow(UserSettings.data.genderPreferences[0], 2f), Mathf.Pow(UserSettings.data.genderPreferences[3], 2f), Mathf.Pow(UserSettings.data.genderPreferences[2], 2f), Mathf.Pow(UserSettings.data.genderPreferences[5], 2f), Mathf.Pow(UserSettings.data.genderPreferences[4], 2f), Mathf.Pow(UserSettings.data.genderPreferences[6], 2f));
		RandomCharacterGenerator.setBodyTypeWeights(Mathf.Pow(2f, UserSettings.data.bodyTypePreferences[0]) - 1f, Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[0]) - 1f + Mathf.Pow(1.9f, UserSettings.data.bodyTypePreferences[3]) - 1f, Mathf.Pow(2f, UserSettings.data.bodyTypePreferences[3]) - 1f, Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[0]) - 1f + Mathf.Pow(1.5f, UserSettings.data.bodyTypePreferences[1]) - 1f + Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[3]) - 1f, Mathf.Pow(2f, UserSettings.data.bodyTypePreferences[2]) - 1f + Mathf.Pow(1.3f, UserSettings.data.bodyTypePreferences[3]) - 1f, Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[2]) - 1f, Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[2]) - 1f, Mathf.Pow(1.5f, UserSettings.data.bodyTypePreferences[2]) - 1f, Mathf.Pow(1.5f, UserSettings.data.bodyTypePreferences[0]) - 1f + Mathf.Pow(2f, UserSettings.data.bodyTypePreferences[1]) - 1f, Mathf.Pow(1.3f, UserSettings.data.bodyTypePreferences[0]) - 1f + Mathf.Pow(2f, UserSettings.data.bodyTypePreferences[1]) - 1f, Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[0]) - 1f + Mathf.Pow(1.5f, UserSettings.data.bodyTypePreferences[3]) - 1f, Mathf.Pow(1.7f, UserSettings.data.bodyTypePreferences[0]) - 1f + Mathf.Pow(1.5f, UserSettings.data.bodyTypePreferences[1]) - 1f);
		RandomCharacterGenerator.addBodyTypeWeights(Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f), Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f) * 0.3f + Mathf.Pow(UserSettings.data.bodyTypePreferences[3], 2f) * 0.7f, Mathf.Pow(UserSettings.data.bodyTypePreferences[3], 2f), Mathf.Pow(UserSettings.data.bodyTypePreferences[1], 2f) * 0.3f + Mathf.Pow(UserSettings.data.bodyTypePreferences[1], 2f) * 0.2f + Mathf.Pow(UserSettings.data.bodyTypePreferences[3], 2f) * 0.5f, Mathf.Pow(UserSettings.data.bodyTypePreferences[2], 2f) * 0.8f + Mathf.Pow(UserSettings.data.bodyTypePreferences[3], 2f) * 0.2f, Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f) * 0.1f + Mathf.Pow(UserSettings.data.bodyTypePreferences[2], 2f) * 0.9f, Mathf.Pow(UserSettings.data.bodyTypePreferences[2], 2f), Mathf.Pow(UserSettings.data.bodyTypePreferences[2], 2f) * 0.2f, Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f) * 0.3f + Mathf.Pow(UserSettings.data.bodyTypePreferences[1], 2f) * 0.7f, Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f) * 0.5f + Mathf.Pow(UserSettings.data.bodyTypePreferences[1], 2f) * 0.5f, Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f) * 0.6f + Mathf.Pow(UserSettings.data.bodyTypePreferences[3], 2f) * 0.2f, Mathf.Pow(UserSettings.data.bodyTypePreferences[0], 2f) * 0.8f);
		switch ((int)UserSettings.data.stylePreference)
		{
		case 0:
			RandomCharacterGenerator.setColorWeights(100f, 10f, 0f, 0f);
			break;
		case 1:
			RandomCharacterGenerator.setColorWeights(100f, 40f, 0f, 0f);
			break;
		case 2:
			RandomCharacterGenerator.setColorWeights(90f, 60f, 10f, 0f);
			break;
		case 3:
			RandomCharacterGenerator.setColorWeights(50f, 80f, 40f, 5f);
			break;
		case 4:
			RandomCharacterGenerator.setColorWeights(20f, 80f, 50f, 5f);
			break;
		case 5:
			RandomCharacterGenerator.setColorWeights(10f, 40f, 60f, 30f);
			break;
		case 6:
			RandomCharacterGenerator.setColorWeights(5f, 30f, 80f, 70f);
			break;
		}
		for (int i = 0; i < CharacterManager.species.Count; i++)
		{
			if (RandomCharacterGenerator.forceSpecies == string.Empty)
			{
				RandomCharacterGenerator.setWeight("spe_" + CharacterManager.species[i], Mathf.Pow(2f, UserSettings.getSpeciesPreference(CharacterManager.species[i])) - 1f);
			}
			else if (CharacterManager.species[i].ToLower() == RandomCharacterGenerator.forceSpecies)
			{
				RandomCharacterGenerator.setWeight("spe_" + CharacterManager.species[i], 100f);
			}
			else
			{
				RandomCharacterGenerator.setWeight("spe_" + CharacterManager.species[i], 0f);
			}
		}
	}

	public static void addSpecies(string species, float val)
	{
		RandomCharacterGenerator.setWeight("spe_" + species, val);
	}

	public static void setWeight(string id, float val)
	{
		for (int i = 0; i < RandomCharacterGenerator.weights.Count; i++)
		{
			if (RandomCharacterGenerator.weights[i].id == id)
			{
				RandomCharacterGenerator.weights[i].weight = val;
				return;
			}
		}
		RandomCharacterGenerator.weights.Add(new GeneratorWeight());
		RandomCharacterGenerator.weights[RandomCharacterGenerator.weights.Count - 1].id = id;
		RandomCharacterGenerator.weights[RandomCharacterGenerator.weights.Count - 1].weight = val;
	}

	public static void adjustWeight(string id, float val)
	{
		for (int i = 0; i < RandomCharacterGenerator.weights.Count; i++)
		{
			if (RandomCharacterGenerator.weights[i].id == id)
			{
				RandomCharacterGenerator.weights[i].weight += val;
				return;
			}
		}
		RandomCharacterGenerator.weights.Add(new GeneratorWeight());
		RandomCharacterGenerator.weights[RandomCharacterGenerator.weights.Count - 1].id = id;
		RandomCharacterGenerator.weights[RandomCharacterGenerator.weights.Count - 1].weight = val;
	}

	public static void setGenderWeights(float male, float female, float cboy, float dgirl, float mherm, float fherm, float eun)
	{
		RandomCharacterGenerator.setWeight("gen_male", male);
		RandomCharacterGenerator.setWeight("gen_female", female);
		RandomCharacterGenerator.setWeight("gen_cboy", cboy);
		RandomCharacterGenerator.setWeight("gen_dgirl", dgirl);
		RandomCharacterGenerator.setWeight("gen_mherm", mherm);
		RandomCharacterGenerator.setWeight("gen_fherm", fherm);
		RandomCharacterGenerator.setWeight("gen_eun", eun);
	}

	public static void addBodyTypeWeights(float average, float athlete, float bodybuilder, float swimmer, float bear, float sturdy, float chubby, float obese, float scrawny, float lanky, float amazonian, float hourglass)
	{
		RandomCharacterGenerator.adjustWeight("bod_average", average);
		RandomCharacterGenerator.adjustWeight("bod_athlete", athlete);
		RandomCharacterGenerator.adjustWeight("bod_bodybuilder", bodybuilder);
		RandomCharacterGenerator.adjustWeight("bod_swimmer", swimmer);
		RandomCharacterGenerator.adjustWeight("bod_bear", bear);
		RandomCharacterGenerator.adjustWeight("bod_sturdy", sturdy);
		RandomCharacterGenerator.adjustWeight("bod_chubby", chubby);
		RandomCharacterGenerator.adjustWeight("bod_obese", obese);
		RandomCharacterGenerator.adjustWeight("bod_scrawny", scrawny);
		RandomCharacterGenerator.adjustWeight("bod_lanky", lanky);
		RandomCharacterGenerator.adjustWeight("bod_amazonian", amazonian);
		RandomCharacterGenerator.adjustWeight("bod_hourglass", hourglass);
	}

	public static void setBodyTypeWeights(float average, float athlete, float bodybuilder, float swimmer, float bear, float sturdy, float chubby, float obese, float scrawny, float lanky, float amazonian, float hourglass)
	{
		RandomCharacterGenerator.setWeight("bod_average", average);
		RandomCharacterGenerator.setWeight("bod_athlete", athlete);
		RandomCharacterGenerator.setWeight("bod_bodybuilder", bodybuilder);
		RandomCharacterGenerator.setWeight("bod_swimmer", swimmer);
		RandomCharacterGenerator.setWeight("bod_bear", bear);
		RandomCharacterGenerator.setWeight("bod_sturdy", sturdy);
		RandomCharacterGenerator.setWeight("bod_chubby", chubby);
		RandomCharacterGenerator.setWeight("bod_obese", obese);
		RandomCharacterGenerator.setWeight("bod_scrawny", scrawny);
		RandomCharacterGenerator.setWeight("bod_lanky", lanky);
		RandomCharacterGenerator.setWeight("bod_amazonian", amazonian);
		RandomCharacterGenerator.setWeight("bod_hourglass", hourglass);
	}

	public static void setColorWeights(float natural, float rareButBelievable, float fantastic, float sparklefurs)
	{
		RandomCharacterGenerator.setWeight("col_natural", natural);
		RandomCharacterGenerator.setWeight("col_rare", rareButBelievable);
		RandomCharacterGenerator.setWeight("col_fantastic", fantastic);
		RandomCharacterGenerator.setWeight("col_sparkle", sparklefurs);
	}

	public static RackCharacter createRandomCharacter(bool wipeFirst = true)
	{
		if (wipeFirst)
		{
			RandomCharacterGenerator.wipe();
		}
		RackCharacter rackCharacter = new RackCharacter(Game.gameInstance, RandomCharacterGenerator.randomData(), false, null, 0f, string.Empty);
		RandomCharacterGenerator.randomizeColors(rackCharacter, -1);
		rackCharacter.buildTexture();
		return rackCharacter;
	}

	public static CharacterData randomData()
	{
		bool flag = false;
		int genitalType = 0;
		string bodyType = RandomCharacterGenerator.rollWeights("bod");
		string species = RandomCharacterGenerator.rollWeights("spe");
		string text = RandomCharacterGenerator.rollWeights("gen");
		if (UserSettings.needTutorial("NPT_DISMISS_THE_SUBJECT") && text == "eun")
		{
			text = "cboy";
		}
		RandomCharacterGenerator.randColClass = RandomCharacterGenerator.rollWeights("col");
		bool identifiesMale = false;
		if (text != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
			dictionary.Add("male", 0);
			dictionary.Add("female", 1);
			dictionary.Add("cboy", 2);
			dictionary.Add("dgirl", 3);
			dictionary.Add("fherm", 4);
			dictionary.Add("mherm", 5);
			dictionary.Add("eun", 6);
			int num = default(int);
			if (dictionary.TryGetValue(text, out num))
			{
				switch (num)
				{
				case 0:
					flag = false;
					genitalType = 0;
					identifiesMale = true;
					break;
				case 1:
					flag = true;
					genitalType = 1;
					break;
				case 2:
					flag = false;
					genitalType = 1;
					identifiesMale = true;
					break;
				case 3:
					flag = true;
					genitalType = 0;
					break;
				case 4:
					flag = true;
					genitalType = 3;
					break;
				case 5:
					flag = false;
					genitalType = 3;
					identifiesMale = true;
					break;
				case 6:
					flag = false;
					genitalType = 2;
					break;
				}
			}
		}
		CharacterData characterData = Game.buildCharacterDataFromParameters(species, flag, genitalType, bodyType);
		characterData.genResult = text;
		for (int num2 = characterData.embellishmentLayers.Count - 1; num2 >= 0; num2--)
		{
			if (characterData.embellishmentLayers[num2].genderRequirement != 0)
			{
				if (characterData.embellishmentLayers[num2].genderRequirement == 1 && flag)
				{
					characterData.embellishmentLayers.RemoveAt(num2);
				}
				else if (characterData.embellishmentLayers[num2].genderRequirement == -1 && !flag)
				{
					characterData.embellishmentLayers.RemoveAt(num2);
				}
			}
		}
		characterData.identifiesMale = identifiesMale;
		characterData.aggression = -0.9f + UnityEngine.Random.value * 1.6f;
		if (UserSettings.needTutorial("NPT_DISMISS_THE_SUBJECT"))
		{
			characterData.aggression = -0.3f;
		}
		characterData.name = RandomCharacterGenerator.determineRandomName(characterData);
		RandomCharacterGenerator.determineSexualCharacteristics(characterData);
		RandomCharacterGenerator.determinePreferences(characterData);
		RandomCharacterGenerator.determineConfidences(characterData);
		return characterData;
	}

	public static void randomize(RackCharacter character)
	{
		character.data = RandomCharacterGenerator.randomData();
		character.rebuildCharacter();
		RandomCharacterGenerator.randomizeColors(character, -1);
		character.buildTexture();
	}

	public static int randDir()
	{
		if (UnityEngine.Random.value > 0.5f)
		{
			return 1;
		}
		return -1;
	}

	public static void determineConfidences(CharacterData cData)
	{
		foreach (string key in SexualPreferences.confidences.Keys)
		{
			bool flag = false;
			int num = 0;
			while (num < cData.confidences.Count)
			{
				if (!(cData.confidences[num].attribute == key))
				{
					num++;
					continue;
				}
				flag = true;
				break;
			}
			if (!flag)
			{
				ConfidenceValue confidenceValue = new ConfidenceValue();
				confidenceValue.attribute = key;
				confidenceValue.value = SexualPreferences.confidences[key].defaultValue;
				if (SexualPreferences.confidences[key].invertIfFemale && !cData.identifiesMale)
				{
					confidenceValue.value *= -1f;
				}
				cData.confidences.Add(confidenceValue);
			}
		}
		for (int i = 0; i < cData.confidences.Count; i++)
		{
			float num2 = Mathf.Pow(UnityEngine.Random.value, 2f);
			cData.confidences[i].value += num2;
		}
		for (int j = 0; j < cData.preferences.Count; j++)
		{
			if (cData.preferences[j].preference == "analfucking_receiving" && cData.preferences[j].value < 0.4f)
			{
				for (int k = 0; k < cData.confidences.Count; k++)
				{
					if (cData.confidences[k].attribute == "ass_cuteness" && cData.confidences[k].value > 0f)
					{
						cData.confidences[k].value *= -1f;
					}
				}
			}
		}
	}

	public static void determinePreferences(CharacterData cData)
	{
		foreach (string key in SexualPreferences.preferences.Keys)
		{
			bool flag = false;
			int num = 0;
			while (num < cData.preferences.Count)
			{
				if (!(cData.preferences[num].preference == key))
				{
					num++;
					continue;
				}
				flag = true;
				break;
			}
			if (!flag)
			{
				SexualPreferenceValue sexualPreferenceValue = new SexualPreferenceValue();
				sexualPreferenceValue.preference = key;
				sexualPreferenceValue.value = SexualPreferences.preferences[key].defaultValue;
				cData.preferences.Add(sexualPreferenceValue);
			}
		}
		for (int i = 0; i < cData.preferences.Count; i++)
		{
			float num2 = Mathf.Pow(UnityEngine.Random.value, 3f) * 3f;
			if (UnityEngine.Random.value > 0.6f)
			{
				num2 *= -1f;
			}
			cData.preferences[i].value += num2;
			if (cData.preferences[i].value > 0.6f)
			{
				switch (SexualPreferences.preferences[cData.preferences[i].preference].keepSecret)
				{
				case 0:
					cData.preferences[i].knowledge = 2;
					break;
				case 1:
					cData.preferences[i].knowledge = 1;
					break;
				case 2:
					cData.preferences[i].knowledge = 1;
					if (cData.genitalType != 1 && cData.genitalType != 3)
					{
						break;
					}
					cData.preferences[i].knowledge = 2;
					break;
				case 3:
					cData.preferences[i].knowledge = 2;
					if (cData.genitalType != 1 && cData.genitalType != 3)
					{
						break;
					}
					cData.preferences[i].knowledge = 1;
					break;
				}
				if (cData.preferences[i].knowledge == 1)
				{
					float value = UnityEngine.Random.value;
					if (value > 0.7f)
					{
						cData.preferences[i].knowledge = 2;
					}
					if (value < 0.3f)
					{
						cData.preferences[i].knowledge = 0;
					}
				}
			}
			else
			{
				cData.preferences[i].knowledge = 2;
			}
			if (cData.preferences[i].preference == "category_sensations" || cData.preferences[i].preference == "category_experiences" || cData.preferences[i].preference == "category_attraction")
			{
				cData.preferences[i].value = Mathf.Abs(cData.preferences[i].value);
			}
		}
	}

	public static void initSexualCharactertistics()
	{
	}

	public static void determineSexualCharacteristics(CharacterData cData)
	{
		string genResult = cData.genResult;
		UserSettings.data.mod_rngRanges.negativeExperienceModifier.getValue(ref cData.negativeExperienceModifier, -4f);
		UserSettings.data.mod_rngRanges.height.getValue(ref cData.height, 0f);
		UserSettings.data.mod_rngRanges.breastSize.getValue(ref cData.breastSize, 0f);
		UserSettings.data.mod_rngRanges.growerShower.getValue(ref cData.growerShower, -0.1f);
		UserSettings.data.mod_rngRanges.penisCurveX.getValue(ref cData.penisCurveX, -0.5f);
		UserSettings.data.mod_rngRanges.penisCurveY.getValue(ref cData.penisCurveY, -0.5f);
		UserSettings.data.mod_rngRanges.penisLength.getValue(ref cData.penisLength, 0f);
		UserSettings.data.mod_rngRanges.penisGirth.getValue(ref cData.penisGirth, -0.5f);
		UserSettings.data.mod_rngRanges.penisSize.getValue(ref cData.penisSize, -0.5f);
		UserSettings.data.mod_rngRanges.ballSize.getValue(ref cData.ballSize, -0.5f);
		UserSettings.data.mod_rngRanges.tailholeTightness.getValue(ref cData.tailholeTightness, -0.5f);
		UserSettings.data.mod_rngRanges.vaginalTightness.getValue(ref cData.vaginalTightness, -0.3f);
		UserSettings.data.mod_rngRanges.scrotumLength.getValue(ref cData.scrotumLength, -0.5f);
		UserSettings.data.mod_rngRanges.breastPerk.getValue(ref cData.breastPerk, -0.1f);
		UserSettings.data.mod_rngRanges.nippleSize.getValue(ref cData.nippleSize, -0.1f);
		UserSettings.data.mod_rngRanges.vaginaPlumpness.getValue(ref cData.vaginaPlumpness, -0.3f);
		UserSettings.data.mod_rngRanges.vaginaShape.getValue(ref cData.vaginaShape, -0.2f);
		UserSettings.data.mod_rngRanges.wetnessThreshold.getValue(ref cData.wetnessThreshold, -0.4f);
		UserSettings.data.mod_rngRanges.squirtAmount.getValue(ref cData.squirtAmount, -0.2f);
		UserSettings.data.mod_rngRanges.clitSize.getValue(ref cData.clitSize, -0.2f);
		UserSettings.data.mod_rngRanges.cumVolume.getValue(ref cData.cumVolume, -0.9f);
		UserSettings.data.mod_rngRanges.cumSpurtStrength.getValue(ref cData.cumSpurtStrength, -1.2f);
		UserSettings.data.mod_rngRanges.cumSpurtRatio.getValue(ref cData.mod_cumSpurtRatio, -0.5f);
		UserSettings.data.mod_rngRanges.cumSpurtFrequency.getValue(ref cData.cumSpurtFrequency, -1f);
		UserSettings.data.mod_rngRanges.precumThreshold.getValue(ref cData.precumThreshold, -0.8f);
		UserSettings.data.mod_rngRanges.stamina.getValue(ref cData.stamina, -1f);
		UserSettings.data.mod_rngRanges.orgasmDuration.getValue(ref cData.orgasmDuration, -1f);
		UserSettings.data.mod_rngRanges.orgasmAnticipationFactor.getValue(ref cData.orgasmAnticipationFactor, -0.2f);
		UserSettings.data.mod_rngRanges.refractoryDuration.getValue(ref cData.refractoryDuration, -1f);
		UserSettings.data.mod_rngRanges.orgasmSensitivity.getValue(ref cData.orgasmSensitivity, -0.2f);
		UserSettings.data.mod_rngRanges.refractorySensitivity.getValue(ref cData.refractorySensitivity, -3f);
		UserSettings.data.mod_rngRanges.sensitivity.getValue(ref cData.sensitivity, -1f);
		UserSettings.data.mod_rngRanges.proximitySensitivity.getValue(ref cData.proximitySensitivity, -0.1f);
		UserSettings.data.mod_rngRanges.analPleasure.getValue(ref cData.analPleasure, -1f);
		if (!(genResult == "female"))
		{
			if (genResult == "cboy")
			{
				cData.refractoryDuration *= 0.1f;
				cData.analPleasure *= 0.1f;
			}
		}
		else
		{
			cData.refractoryDuration *= 0.1f;
			cData.analPleasure *= 0.1f;
		}
	}

	public static string determineRandomName(CharacterData cData)
	{
		StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/names/firstnames.txt");
		string[] array = streamReader.ReadToEnd().Split(';');
		streamReader.Close();
		int num = 0;
		int num2 = 18993;
		if (cData.identifiesMale)
		{
			num = num2;
			num2 = array.Length - num2;
		}
		int num3 = num + Mathf.FloorToInt(Mathf.Pow(UnityEngine.Random.value, 4f) * (float)num2);
		string str = array[num3];
		StreamReader streamReader2 = new StreamReader(Application.persistentDataPath + "/names/lastnames.txt");
		string[] array2 = streamReader2.ReadToEnd().Split(';');
		streamReader2.Close();
		streamReader = null;
		streamReader2 = null;
		string str2 = array2[Mathf.FloorToInt(UnityEngine.Random.value * (float)array2.Length) % array2.Length];
		return str + " " + str2;
	}

	public static void determineColorReferencesAndDefinitions(RackCharacter character)
	{
		CharacterData characterData = new CharacterData();
		RackCharacter.createDefaultEmbellishmentColors(characterData);
		Game.speciesDefinitionColors = new List<UnityEngine.Color>();
		Game.speciesDefinitionColorNames = new List<string>();
		Game.speciesDefinitionColorReplacements = new List<UnityEngine.Color>();
		Game.speciesDefinitionColorRefs = new List<int>();
		Game.speciesDefinitionColorRefOffsets_hue = new List<float>();
		Game.speciesDefinitionColorRefOffsets_sat = new List<float>();
		Game.speciesDefinitionColorRefOffsets_val = new List<float>();
		for (int i = 0; i < character.data.textureLayers.Count; i++)
		{
			if (!Game.containsColor(Game.speciesDefinitionColors, character.data.textureLayers[i].color))
			{
				Game.speciesDefinitionColors.Add(character.data.textureLayers[i].color);
				Game.speciesDefinitionColorReplacements.Add(character.data.textureLayers[i].color);
			}
		}
		if (!Game.containsColor(Game.speciesDefinitionColors, character.data.baseColor))
		{
			Game.speciesDefinitionColors.Add(character.data.baseColor);
			Game.speciesDefinitionColorReplacements.Add(character.data.baseColor);
		}
		for (int j = 0; j < character.data.embellishmentColors.Count; j++)
		{
			if (!(character.data.embellishmentColors[j] == characterData.embellishmentColors[j]) && !Game.containsColor(Game.speciesDefinitionColors, character.data.embellishmentColors[j] / 255f))
			{
				Game.speciesDefinitionColors.Add(character.data.embellishmentColors[j] / 255f);
				Game.speciesDefinitionColorReplacements.Add(character.data.embellishmentColors[j] / 255f);
			}
		}
		for (int k = 0; k < character.data.embellishmentColorGradientPoints.Count; k++)
		{
			bool flag = false;
			for (int l = 0; l < characterData.embellishmentColorGradientPoints.Count; l++)
			{
				flag = (flag || character.data.embellishmentColorGradientPoints[k].color == characterData.embellishmentColorGradientPoints[l].color);
			}
			if (!flag && !Game.containsColor(Game.speciesDefinitionColors, character.data.embellishmentColorGradientPoints[k].color / 255f))
			{
				Game.speciesDefinitionColors.Add(character.data.embellishmentColorGradientPoints[k].color / 255f);
				Game.speciesDefinitionColorReplacements.Add(character.data.embellishmentColorGradientPoints[k].color / 255f);
			}
		}
		for (int m = 0; m < Game.speciesDefinitionColorReplacements.Count; m++)
		{
			Game.speciesDefinitionColorRefs.Add(-1);
			Game.speciesDefinitionColorNames.Add(string.Empty);
			Game.speciesDefinitionColorRefOffsets_hue.Add(0f);
			Game.speciesDefinitionColorRefOffsets_sat.Add(0f);
			Game.speciesDefinitionColorRefOffsets_val.Add(1f);
		}
		for (int n = 0; n < Game.speciesDefinitionColors.Count; n++)
		{
			for (int num = 0; num < character.data.colorDefinitions.Count; num++)
			{
				UnityEngine.Color color = Game.speciesDefinitionColors[n];
				if (Mathf.Abs(color.r - character.data.colorDefinitions[num].r) < 0.001f)
				{
					UnityEngine.Color color2 = Game.speciesDefinitionColors[n];
					if (Mathf.Abs(color2.g - character.data.colorDefinitions[num].g) < 0.001f)
					{
						UnityEngine.Color color3 = Game.speciesDefinitionColors[n];
						if (Mathf.Abs(color3.b - character.data.colorDefinitions[num].b) < 0.001f)
						{
							Game.speciesDefinitionColorNames[n] = character.data.colorDefinitions[num].name;
							if (character.data.colorDefinitions[num].autoBase != string.Empty)
							{
								for (int num2 = 0; num2 < character.data.colorDefinitions.Count; num2++)
								{
									if (character.data.colorDefinitions[num2].name == character.data.colorDefinitions[num].autoBase)
									{
										Game.speciesDefinitionColorRefs[n] = num2;
										UnityEngine.Color white = UnityEngine.Color.white;
										white.r = character.data.colorDefinitions[num].r;
										white.g = character.data.colorDefinitions[num].g;
										white.b = character.data.colorDefinitions[num].b;
										float num3 = default(float);
										float num4 = default(float);
										float num5 = default(float);
										ColorPicker.ColorToHSV(white, out num3, out num4, out num5);
										UnityEngine.Color white2 = UnityEngine.Color.white;
										white2.r = character.data.colorDefinitions[num2].r;
										white2.g = character.data.colorDefinitions[num2].g;
										white2.b = character.data.colorDefinitions[num2].b;
										float num6 = default(float);
										float num7 = default(float);
										float num8 = default(float);
										ColorPicker.ColorToHSV(white2, out num6, out num7, out num8);
										Game.speciesDefinitionColorRefOffsets_hue[n] = num6 - num3;
										Game.speciesDefinitionColorRefOffsets_sat[n] = num7 - num4;
										Game.speciesDefinitionColorRefOffsets_val[n] = num8 / num5;
									}
								}
								break;
							}
						}
					}
				}
			}
		}
	}

	public static UnityEngine.Color randomEyeColor(int oddity)
	{
		if (RandomCharacterGenerator.naturalEyeColors.Count < 1)
		{
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.2786828f, 0.345393956f, 0.719495833f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.340063959f, 0.5279944f, 0.5892602f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.2688667f, 0.5967017f, 0.318655878f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.14553988f, 1f, 0.4084506f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.46974954f, 0.7567058f, 0.273385346f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.377161235f, 0.229756758f, 0.136262238f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.5148393f, 0.3696336f, 0.300946772f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.369719565f, 0.352674037f, 0.302789181f));
			RandomCharacterGenerator.naturalEyeColors.Add(new UnityEngine.Color(0.2780084f, 0.299655348f, 0.377161473f));
		}
		UnityEngine.Color a = RandomCharacterGenerator.naturalEyeColors[Mathf.FloorToInt(UnityEngine.Random.value * (float)RandomCharacterGenerator.naturalEyeColors.Count) % RandomCharacterGenerator.naturalEyeColors.Count];
		UnityEngine.Color a2 = RandomCharacterGenerator.naturalEyeColors[Mathf.FloorToInt(UnityEngine.Random.value * (float)RandomCharacterGenerator.naturalEyeColors.Count) % RandomCharacterGenerator.naturalEyeColors.Count];
		float value = UnityEngine.Random.value;
		return a * value + a2 * (1f - value);
	}

	public static void pickRandomColorAndApplyItToMainReplacements(RackCharacter character, int forcedOddity = -1)
	{
		RandomCharacterGenerator.glowAccents = false;
		List<int> list = new List<int>();
		RandomCharacterGenerator.selectedOddity = 1;
		switch (RandomCharacterGenerator.randColClass)
		{
		case "natural":
			RandomCharacterGenerator.selectedOddity = 0;
			break;
		case "rare":
			RandomCharacterGenerator.selectedOddity = 1;
			break;
		case "fantastic":
			RandomCharacterGenerator.selectedOddity = 2;
			break;
		case "sparkle":
			RandomCharacterGenerator.selectedOddity = 3;
			break;
		}
		if (forcedOddity != -1)
		{
			RandomCharacterGenerator.selectedOddity = forcedOddity;
		}
		list.Add(-1);
		for (int i = 0; i < character.data.colorDefinitionAlts.Count; i++)
		{
			if (character.data.colorDefinitionAlts[i].oddity <= RandomCharacterGenerator.selectedOddity && !list.Contains(character.data.colorDefinitionAlts[i].alt))
			{
				list.Add(character.data.colorDefinitionAlts[i].alt);
			}
		}
		int index = Mathf.FloorToInt(UnityEngine.Random.value * (float)list.Count) % list.Count;
		int num = list[index];
		for (int j = 0; j < Game.speciesDefinitionColorReplacements.Count; j++)
		{
			if (Game.speciesDefinitionColorNames[j] == "Iris")
			{
				Game.speciesDefinitionColorReplacements[j] = RandomCharacterGenerator.randomEyeColor(RandomCharacterGenerator.selectedOddity);
			}
		}
		if (num == -1 && RandomCharacterGenerator.selectedOddity < 2)
		{
			return;
		}
		float num2 = Game.cap((float)RandomCharacterGenerator.selectedOddity * 0.1f + UnityEngine.Random.value, 0f, 1f);
		float b = 1f - Mathf.Pow(1f - num2, (float)(2 + RandomCharacterGenerator.selectedOddity));
		for (int k = 0; k < character.data.colorDefinitionAlts.Count; k++)
		{
			if (character.data.colorDefinitionAlts[k].alt == num)
			{
				for (int l = 0; l < Game.speciesDefinitionColorReplacements.Count; l++)
				{
					if (Game.speciesDefinitionColorNames[l] == character.data.colorDefinitionAlts[k].name)
					{
						UnityEngine.Color white = UnityEngine.Color.white;
						white.r = character.data.colorDefinitionAlts[k].r;
						white.g = character.data.colorDefinitionAlts[k].g;
						white.b = character.data.colorDefinitionAlts[k].b;
						List<UnityEngine.Color> speciesDefinitionColorReplacements;
						int index2;
						(speciesDefinitionColorReplacements = Game.speciesDefinitionColorReplacements)[index2 = l] = speciesDefinitionColorReplacements[index2] + (white - Game.speciesDefinitionColorReplacements[l]) * b;
					}
				}
			}
		}
		if (RandomCharacterGenerator.accentLayers.Count < 1)
		{
			RandomCharacterGenerator.accentLayers.Add("Accent");
			RandomCharacterGenerator.accentLayers.Add("Accents");
			RandomCharacterGenerator.accentLayers.Add("Iris");
			RandomCharacterGenerator.baseLayers.Add("Fur");
			RandomCharacterGenerator.baseLayers.Add("Scales");
			RandomCharacterGenerator.baseLayers.Add("Feathers");
			RandomCharacterGenerator.darkLayers.Add("Dark Fur");
			RandomCharacterGenerator.accentLayers.Add("Spots");
			RandomCharacterGenerator.lightLayers.Add("Underbelly");
			RandomCharacterGenerator.accentLayers.Add("Extremities");
			RandomCharacterGenerator.accentLayers.Add("Pawpads");
			RandomCharacterGenerator.accentLayers.Add("Tongue");
			RandomCharacterGenerator.accentLayers.Add("Stripes");
			RandomCharacterGenerator.accentLayers.Add("Spikes");
			RandomCharacterGenerator.accentLayers.Add("Flesh");
			RandomCharacterGenerator.accentTextures.Add("cheststripe_0");
			RandomCharacterGenerator.accentTextures.Add("eyecorner_0");
			RandomCharacterGenerator.accentTextures.Add("cheeks_1");
			RandomCharacterGenerator.accentTextures.Add("eyebrowmarkings_7");
			RandomCharacterGenerator.accentTextures.Add("eyesockets_5");
			RandomCharacterGenerator.accentTextures.Add("forehead_0");
			RandomCharacterGenerator.accentTextures.Add("lowerback_0");
			RandomCharacterGenerator.accentTextures.Add("lowereyelid_3");
			RandomCharacterGenerator.accentTextures.Add("scalp_0");
			RandomCharacterGenerator.accentTextures.Add("nosebridge_5");
			RandomCharacterGenerator.accentTextures.Add("scalp_4");
			RandomCharacterGenerator.accentTextures.Add("snoutmarking_5");
			RandomCharacterGenerator.accentTextures.Add("snoutmarking_1");
			RandomCharacterGenerator.accentTextures.Add("tailtip_3");
			RandomCharacterGenerator.accentTextures.Add("toetips_1");
			RandomCharacterGenerator.accentTextures.Add("uppereyelid_2");
		}
		bool flag = false;
		float num3 = UnityEngine.Random.value * 360f;
		float num4 = 0.4f + UnityEngine.Random.value * 0.15f + (float)(RandomCharacterGenerator.selectedOddity - 2) * 0.2f;
		float num5 = 0.99f;
		bool flag2 = false;
		if (RandomCharacterGenerator.selectedOddity >= 2)
		{
			RandomCharacterGenerator.glowAccents = (RandomCharacterGenerator.glowAccents || UnityEngine.Random.value > 0.95f);
			if (UnityEngine.Random.value > 0.5f)
			{
				num5 -= 0.4f;
				flag2 = true;
			}
			RandomCharacterGenerator.accentColor = ColorPicker.HsvToColor(num3, num4, num5);
			for (int m = 0; m < Game.speciesDefinitionColorReplacements.Count; m++)
			{
				if (RandomCharacterGenerator.accentLayers.Contains(Game.speciesDefinitionColorNames[m]))
				{
					if (Game.speciesDefinitionColorNames[m] == "Iris")
					{
						Game.speciesDefinitionColorReplacements[m] = RandomCharacterGenerator.accentColor * 0.75f;
					}
					else if (RandomCharacterGenerator.selectedOddity == 3)
					{
						if (UnityEngine.Random.value > 0.75f)
						{
							Game.speciesDefinitionColorReplacements[m] = RandomCharacterGenerator.accentColor * 0.2f;
						}
						else if (UnityEngine.Random.value > 0.75f)
						{
							Game.speciesDefinitionColorReplacements[m] = UnityEngine.Color.white * 0.9f;
						}
						else
						{
							Game.speciesDefinitionColorReplacements[m] = (UnityEngine.Color.white + RandomCharacterGenerator.accentColor) / 2f;
						}
					}
					else
					{
						Game.speciesDefinitionColorReplacements[m] = RandomCharacterGenerator.accentColor;
					}
				}
			}
			int num6 = -1 + Mathf.FloorToInt(UnityEngine.Random.value * 6f);
			if (num6 >= 1)
			{
				List<string> list2 = new List<string>();
				for (int n = 0; n < num6; n++)
				{
					string text = string.Empty;
					while (true)
					{
						if (!(text == string.Empty) && !list2.Contains(text))
						{
							break;
						}
						text = Game.randPick(RandomCharacterGenerator.accentTextures.ToArray());
					}
					list2.Add(text);
				}
				for (int num7 = 0; num7 < list2.Count; num7++)
				{
					character.data.textureLayers.Add(new TextureLayer());
					character.data.textureLayers[character.data.textureLayers.Count - 1].texture = list2[num7];
					character.data.textureLayers[character.data.textureLayers.Count - 1].color = RandomCharacterGenerator.accentColor;
					character.data.textureLayers[character.data.textureLayers.Count - 1].glow = RandomCharacterGenerator.glowAccents;
				}
				flag = true;
			}
		}
		if (RandomCharacterGenerator.selectedOddity >= 3)
		{
			if (!flag2)
			{
				num3 = ((!(UnityEngine.Random.value > 0.5f)) ? (num3 + 120f) : (num3 - 120f));
			}
			UnityEngine.Color value = ColorPicker.HsvToColor(num3, num4, num5);
			UnityEngine.Color value2 = ColorPicker.HsvToColor(num3, num4 * 0.4f, (1f + num5) * 0.5f);
			UnityEngine.Color value3 = ColorPicker.HsvToColor(num3, num4, 0.5f * num5);
			for (int num8 = 0; num8 < Game.speciesDefinitionColorReplacements.Count; num8++)
			{
				if (RandomCharacterGenerator.darkLayers.Contains(Game.speciesDefinitionColorNames[num8]))
				{
					Game.speciesDefinitionColorReplacements[num8] = value3;
				}
				else if (RandomCharacterGenerator.lightLayers.Contains(Game.speciesDefinitionColorNames[num8]))
				{
					Game.speciesDefinitionColorReplacements[num8] = value2;
				}
				else if (RandomCharacterGenerator.baseLayers.Contains(Game.speciesDefinitionColorNames[num8]))
				{
					Game.speciesDefinitionColorReplacements[num8] = value;
				}
			}
		}
		if (flag)
		{
			character.buildTexture();
		}
	}

	public static void applyRandomColorsToAllSubcolors(RackCharacter character)
	{
		for (int i = 0; i < Game.speciesDefinitionColorRefs.Count; i++)
		{
			if (Game.speciesDefinitionColorRefs[i] != -1)
			{
				float num = default(float);
				float num2 = default(float);
				float num3 = default(float);
				ColorPicker.ColorToHSV(Game.speciesDefinitionColorReplacements[Game.speciesDefinitionColorRefs[i]], out num, out num2, out num3);
				num = (num + Game.speciesDefinitionColorRefOffsets_hue[i]) % 360f;
				num2 = Game.cap(num2 - Game.speciesDefinitionColorRefOffsets_sat[i], 0f, 1f);
				num3 = Game.cap(num3 / Game.speciesDefinitionColorRefOffsets_val[i], 0f, 1f);
				float r = default(float);
				float g = default(float);
				float b = default(float);
				ColorPicker.HsvToRgb(num, num2, num3, out r, out g, out b);
				UnityEngine.Color white = UnityEngine.Color.white;
				white.r = r;
				white.g = g;
				white.b = b;
				Game.speciesDefinitionColorReplacements[i] = white;
			}
		}
	}

	public static void applyColorsToCharacter(RackCharacter character)
	{
		for (int i = 0; i < Game.speciesDefinitionColors.Count; i++)
		{
			if (Game.basicallyTheSameColor(character.data.baseColor, Game.speciesDefinitionColors[i]))
			{
				character.data.baseColor = Game.speciesDefinitionColorReplacements[i];
			}
			for (int j = 0; j < character.data.textureLayers.Count; j++)
			{
				if (Game.basicallyTheSameColor(character.data.textureLayers[j].color, Game.speciesDefinitionColors[i]))
				{
					character.data.textureLayers[j].color = Game.speciesDefinitionColorReplacements[i];
				}
			}
			for (int k = 0; k < character.data.embellishmentColors.Count; k++)
			{
				if (Game.basicallyTheSameColor(character.data.embellishmentColors[k], Game.speciesDefinitionColors[i] * 255f))
				{
					character.data.embellishmentColors[k] = Game.speciesDefinitionColorReplacements[i] * 255f;
				}
			}
			for (int l = 0; l < character.data.embellishmentColorGradientPoints.Count; l++)
			{
				if (Game.basicallyTheSameColor(character.data.embellishmentColorGradientPoints[l].color, Game.speciesDefinitionColors[i] * 255f))
				{
					character.data.embellishmentColorGradientPoints[l].color = Game.speciesDefinitionColorReplacements[i] * 255f;
				}
			}
		}
	}

	public static void drawDecalsOnCharacter(RackCharacter character)
	{
		int num = 0;
		string text = string.Empty;
		while (true)
		{
			if (!(text == string.Empty))
			{
				if (text.IndexOf("scar") == -1)
				{
					break;
				}
				if (character.data.identifiesMale)
				{
					break;
				}
			}
			switch (RandomCharacterGenerator.selectedOddity)
			{
			case 0:
				if (UnityEngine.Random.value > 0.96f)
				{
					num = 1;
				}
				text = Game.randPick(new string[4]
				{
					"scar",
					"scar2",
					"circle",
					"ring"
				});
				break;
			case 1:
				if (UnityEngine.Random.value > 0.9f)
				{
					num = 1;
				}
				text = Game.randPick(new string[4]
				{
					"scar",
					"scar2",
					"circle",
					"ring"
				});
				break;
			case 2:
				if (UnityEngine.Random.value > 0.6f)
				{
					num = 1;
				}
				if (UnityEngine.Random.value > 0.6f)
				{
					num++;
				}
				text = Game.randPick(new string[14]
				{
					"scar",
					"scar2",
					"scar3",
					"scar4",
					"anhk",
					"ankh1",
					"heart",
					"heart1",
					"hex",
					"hex1",
					"moon1",
					"ring",
					"ring1",
					"star"
				});
				break;
			case 3:
				if (UnityEngine.Random.value > 0.2f)
				{
					num = 1;
				}
				if (UnityEngine.Random.value > 0.2f)
				{
					num++;
				}
				if (UnityEngine.Random.value > 0.4f)
				{
					num++;
				}
				if (UnityEngine.Random.value > 0.6f)
				{
					num++;
				}
				text = Game.randPick(new string[25]
				{
					"scar",
					"scar2",
					"scar3",
					"scar4",
					"anhk",
					"ankh1",
					"eye",
					"eye1",
					"gear",
					"heart",
					"heart1",
					"infinity1",
					"moon",
					"moon1",
					"nuke",
					"nuke1",
					"pawprint",
					"pawprint1",
					"ring",
					"ring1",
					"spade",
					"star",
					"star1",
					"skull",
					"sun1"
				});
				break;
			}
		}
		Game.gameInstance.processDecalPainting();
		List<int> list = new List<int>();
		if (text.IndexOf("scar") != -1 && num > 1)
		{
			num = 1;
		}
		int num2 = 12;
		for (int i = 0; i < num2; i++)
		{
			list.Add(i);
		}
		if (num > 0)
		{
			Texture2D texture2D = new Texture2D(256, 256);
			Texture2D texture2D2 = null;
			if (File.Exists(Application.persistentDataPath + "/decals/" + text + ".png"))
			{
				byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/decals/" + text + ".png");
				texture2D.LoadImage(data);
				if (texture2D.width != 256)
				{
					TextureScale.Bilinear(texture2D, 256, 256);
				}
				texture2D.wrapMode = TextureWrapMode.Clamp;
			}
			if (File.Exists(Application.persistentDataPath + "/decals/" + text + "_FX.png"))
			{
				byte[] data2 = File.ReadAllBytes(Application.persistentDataPath + "/decals/" + text + "_FX.png");
				texture2D2 = new Texture2D(256, 256);
				texture2D2.LoadImage(data2);
				if (texture2D2.width != 256)
				{
					TextureScale.Bilinear(texture2D2, 256, 256);
				}
				texture2D2.wrapMode = TextureWrapMode.Clamp;
			}
			while (num > 0)
			{
				int index = Mathf.FloorToInt(UnityEngine.Random.value * (float)list.Count) % list.Count;
				int num3 = list[index];
				list.RemoveAt(index);
				string empty = string.Empty;
				switch (num3)
				{
				default:
					empty = "back";
					break;
				case 1:
					empty = "buttleft";
					break;
				case 2:
					empty = "buttright";
					break;
				case 3:
					empty = "chest";
					break;
				case 4:
					empty = "eyeleft";
					break;
				case 5:
					empty = "eyeright";
					break;
				case 6:
					empty = "forehead";
					break;
				case 7:
					empty = "handleft";
					break;
				case 8:
					empty = "handright";
					break;
				case 9:
					empty = "shoulderleft";
					break;
				case 10:
					empty = "shoulderright";
					break;
				case 11:
					empty = "trampstamp";
					break;
				}
				string text2 = "decal-" + Guid.NewGuid();
				new FileInfo(Application.persistentDataPath + "/characterTextures/decal_cache/").Directory.Create();
				for (int j = 0; j < 2; j++)
				{
					string text3 = "_body";
					if (j == 1)
					{
						text3 = "_head";
					}
					try
					{
						RandomCharacterGenerator.image = new Bitmap(Application.persistentDataPath + "/decals/templates/" + empty + text3 + ".png");
						RandomCharacterGenerator.bitmapData = RandomCharacterGenerator.image.LockBits(new Rectangle(0, 0, RandomCharacterGenerator.image.Width, RandomCharacterGenerator.image.Height), ImageLockMode.ReadWrite, RandomCharacterGenerator.image.PixelFormat);
						RandomCharacterGenerator.bytesPerPixel = Image.GetPixelFormatSize(RandomCharacterGenerator.image.PixelFormat) / 8;
						RandomCharacterGenerator.byteCount = RandomCharacterGenerator.bitmapData.Stride * RandomCharacterGenerator.image.Height;
						RandomCharacterGenerator.pixels = new byte[RandomCharacterGenerator.byteCount];
						RandomCharacterGenerator.ptrFirstPixel = RandomCharacterGenerator.bitmapData.Scan0;
						Marshal.Copy(RandomCharacterGenerator.ptrFirstPixel, RandomCharacterGenerator.pixels, 0, RandomCharacterGenerator.pixels.Length);
						RandomCharacterGenerator.heightInPixels = RandomCharacterGenerator.bitmapData.Height;
						RandomCharacterGenerator.widthInBytes = RandomCharacterGenerator.bitmapData.Width * RandomCharacterGenerator.bytesPerPixel;
						RandomCharacterGenerator.threadedImagePixels = new Vector4[RandomCharacterGenerator.image.Width * RandomCharacterGenerator.image.Height];
						RandomCharacterGenerator.pix = new UnityEngine.Color[RandomCharacterGenerator.image.Width * RandomCharacterGenerator.image.Height];
						int num4 = 0;
						for (int num5 = RandomCharacterGenerator.heightInPixels - 1; num5 >= 0; num5--)
						{
							int num6 = num5 * RandomCharacterGenerator.bitmapData.Stride;
							for (int k = 0; k < RandomCharacterGenerator.widthInBytes; k += RandomCharacterGenerator.bytesPerPixel)
							{
								RandomCharacterGenerator.threadedImagePixels[num4].x = (float)(int)RandomCharacterGenerator.pixels[num6 + k + 2] / 255f;
								RandomCharacterGenerator.threadedImagePixels[num4].y = (float)(int)RandomCharacterGenerator.pixels[num6 + k + 1] / 255f;
								RandomCharacterGenerator.threadedImagePixels[num4].z = (float)(int)RandomCharacterGenerator.pixels[num6 + k] / 255f;
								RandomCharacterGenerator.threadedImagePixels[num4].w = (float)(int)RandomCharacterGenerator.pixels[num6 + k + 3] / 255f;
								int num7 = Mathf.FloorToInt(RandomCharacterGenerator.threadedImagePixels[num4].x * 255f);
								int num8 = 255 - Mathf.FloorToInt(RandomCharacterGenerator.threadedImagePixels[num4].y * 255f);
								if (empty == "eyeleft")
								{
									num7 = 255 - num7;
									num8 = 255 - num8;
								}
								UnityEngine.Color color = texture2D.GetPixel(num7, num8);
								color.a *= (float)(int)RandomCharacterGenerator.pixels[num6 + k + 3] / 255f;
								color *= RandomCharacterGenerator.accentColor;
								RandomCharacterGenerator.pix[num4] = color;
								num4++;
							}
						}
						RandomCharacterGenerator.image.Dispose();
						RandomCharacterGenerator.image = null;
						RandomCharacterGenerator.bitmapData = null;
						Texture2D texture2D3 = (j != 0) ? new Texture2D(1024, 1024, TextureFormat.ARGB32, false) : new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
						texture2D3.SetPixels(RandomCharacterGenerator.pix);
						texture2D3.Apply();
						Game.saveTexToFile(texture2D3, Application.persistentDataPath + "/characterTextures/decal_cache/" + text2 + text3 + ".png");
						TextureScale.Bilinear(texture2D3, Mathf.RoundToInt((float)texture2D3.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D3.height * UserSettings.data.characterTextureQuality));
						Game.saveTexToFile(texture2D3, Application.persistentDataPath + "/characterTextures/" + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + "/decal_cache/" + text2 + text3 + ".png");
					}
					catch
					{
					}
				}
				character.data.textureLayers.Add(new TextureLayer());
				character.data.textureLayers[character.data.textureLayers.Count - 1].texture = "decal_cache/" + text2;
				character.data.textureLayers[character.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				character.data.textureLayers[character.data.textureLayers.Count - 1].color.r = 0.992156863f;
				character.data.textureLayers[character.data.textureLayers.Count - 1].color.g = 0.992156863f;
				character.data.textureLayers[character.data.textureLayers.Count - 1].color.b = 0.992156863f;
				character.data.textureLayers[character.data.textureLayers.Count - 1].isDecal = true;
				num--;
			}
		}
		Game.gameInstance.stillSafeToUseDecalRaytracingData = false;
	}

	public static void randomizeColors(RackCharacter character, int forcedOddity = -1)
	{
		RandomCharacterGenerator.determineColorReferencesAndDefinitions(character);
		RandomCharacterGenerator.pickRandomColorAndApplyItToMainReplacements(character, forcedOddity);
		RandomCharacterGenerator.applyRandomColorsToAllSubcolors(character);
		RandomCharacterGenerator.applyColorsToCharacter(character);
		RandomCharacterGenerator.drawDecalsOnCharacter(character);
	}

	public static string rollWeights(string key)
	{
		float num = 0f;
		for (int i = 0; i < RandomCharacterGenerator.weights.Count; i++)
		{
			if (RandomCharacterGenerator.weights[i].id.Split('_')[0] == key)
			{
				num += RandomCharacterGenerator.weights[i].weight;
			}
		}
		float num2 = UnityEngine.Random.value * num;
		int num3 = 0;
		while (RandomCharacterGenerator.weights[num3].id.Split('_')[0] != key)
		{
			num3++;
		}
		while (num2 > RandomCharacterGenerator.weights[num3].weight)
		{
			num2 -= RandomCharacterGenerator.weights[num3].weight;
			num3++;
			while (RandomCharacterGenerator.weights[num3].id.Split('_')[0] != key)
			{
				num3++;
			}
		}
		return RandomCharacterGenerator.weights[num3].id.Split('_')[1];
	}

	static RandomCharacterGenerator()
	{
		RandomCharacterGenerator.weights = new List<GeneratorWeight>();
		RandomCharacterGenerator.forceSpecies = string.Empty;
		RandomCharacterGenerator.randColClass = string.Empty;
		RandomCharacterGenerator.naturalEyeColors = new List<UnityEngine.Color>();
		RandomCharacterGenerator.baseLayers = new List<string>();
		RandomCharacterGenerator.lightLayers = new List<string>();
		RandomCharacterGenerator.darkLayers = new List<string>();
		RandomCharacterGenerator.accentLayers = new List<string>();
		RandomCharacterGenerator.accentTextures = new List<string>();
		RandomCharacterGenerator.glowAccents = false;
		RandomCharacterGenerator.image = null;
	}

	public static string buildStatString(RackCharacter character)
	{
		string empty = string.Empty;
		string text = empty;
		empty = text + "_sensitivity: " + Mathf.Round(character.data.sensitivity * 100f) + "\r\n";
		text = empty;
		empty = text + "_stamina: " + Mathf.Round(character.data.stamina * 100f) + "\r\n";
		empty += "\r\n";
		text = empty;
		empty = text + "_proximitySensitivity: " + Mathf.Round(character.data.proximitySensitivity * 100f) + "\r\n";
		text = empty;
		empty = text + "_orgasmDuration: " + Mathf.Round(character.data.orgasmDuration * 100f) + "\r\n";
		text = empty;
		empty = text + "_orgasmAnticipationFactor: " + Mathf.Round(character.data.orgasmAnticipationFactor * 100f) + "\r\n";
		text = empty;
		empty = text + "_orgasmSensitivity: " + Mathf.Round(character.data.orgasmSensitivity * 100f) + "\r\n";
		empty += "\r\n";
		if (character.data.genitalType == 0 || character.data.genitalType == 3)
		{
			text = empty;
			empty = text + "_refractoryDuration: " + Mathf.Round(character.data.refractoryDuration * 100f) + "\r\n";
		}
		if (character.data.genitalType == 0 || character.data.genitalType == 3)
		{
			text = empty;
			empty = text + "_refractorySensitivity: " + Mathf.Round(character.data.refractorySensitivity * 100f) + "\r\n";
		}
		empty += "\r\n";
		if (character.data.breastSize > RackCharacter.breastThreshhold)
		{
			text = empty;
			empty = text + "_breastPerk: " + Mathf.Round(character.data.breastPerk * 100f) + "\r\n";
			text = empty;
			empty = text + "_nippleSize: " + Mathf.Round(character.data.nippleSize * 100f) + "\r\n";
			empty += "\r\n";
		}
		if (character.data.genitalType == 0 || character.data.genitalType == 3)
		{
			text = empty;
			empty = text + "_penisSize: " + Mathf.Round(character.data.penisSize * 100f) + "\r\n";
			text = empty;
			empty = text + "_penisLength: " + Mathf.Round(character.data.penisLength * 100f) + "\r\n";
			text = empty;
			empty = text + "_penisGirth: " + Mathf.Round(character.data.penisGirth * 100f) + "\r\n";
			text = empty;
			empty = text + "_growerShower: " + Mathf.Round(character.data.growerShower * 100f) + "\r\n";
			text = empty;
			empty = text + "_penisCurveX: " + Mathf.Round(character.data.penisCurveX * 100f) + "\r\n";
			text = empty;
			empty = text + "_penisCurveY: " + Mathf.Round(character.data.penisCurveY * 100f) + "\r\n";
			empty += "\r\n";
			text = empty;
			empty = text + "_ballSize: " + Mathf.Round(character.data.ballSize * 100f) + "\r\n";
			text = empty;
			empty = text + "_scrotumLength: " + Mathf.Round(character.data.scrotumLength * 100f) + "\r\n";
			empty += "\r\n";
			text = empty;
			empty = text + "_cumVolume: " + Mathf.Round(character.data.cumVolume * 100f) + "\r\n";
			text = empty;
			empty = text + "_cumSpurtStrength: " + Mathf.Round(character.data.cumSpurtStrength * 100f) + "\r\n";
			text = empty;
			empty = text + "_cumSpurtFrequency: " + Mathf.Round(character.data.cumSpurtFrequency * 100f) + "\r\n";
			text = empty;
			if (UserSettings.data.mod_altCumStyle)
			{
				empty = text + "mod_cumSpurtRatio: " + Mathf.Round(character.data.mod_cumSpurtRatio * 100f) + "\r\n";
				text = empty;
			}
			empty = text + "_precumThreshold: " + Mathf.Round(character.data.precumThreshold * 100f) + "\r\n";
			empty += "\r\n";
		}
		if (character.data.genitalType == 1 || character.data.genitalType == 3)
		{
			text = empty;
			empty = text + "_vaginalTightness: " + Mathf.Round(character.data.vaginalTightness * 100f) + "\r\n";
			text = empty;
			empty = text + "_vaginaPlumpness: " + Mathf.Round(character.data.vaginaPlumpness * 100f) + "\r\n";
			text = empty;
			empty = text + "_vaginaShape: " + Mathf.Round(character.data.vaginaShape * 100f) + "\r\n";
			text = empty;
			empty = text + "_clitSize: " + Mathf.Round(character.data.clitSize * 100f) + "\r\n";
			empty += "\r\n";
		}
		text = empty;
		empty = text + "_tailholeTightness: " + Mathf.Round(character.data.tailholeTightness * 100f) + "\r\n";
		text = empty;
		empty = text + "_analPleasure: " + Mathf.Round(character.data.analPleasure * 100f) + "\r\n";
		return empty + "\r\n";
	}
}
