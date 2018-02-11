using System.Collections.Generic;
using UnityEngine;

public class ResearchList
{
	public static List<ResearchTaskDefinition> allTasksAvailable = new List<ResearchTaskDefinition>();

	public static void init()
	{
		ResearchList.addResearchTaskToList("MaterialSynthesisStation", "SCIENCE_EQUIPMENT", 0.08f, string.Empty, string.Empty);
		ResearchList.addResearchTaskToList("Dildo", "SEX_TOYS_AND_TOOLS", 0f, string.Empty, "MaterialSynthesisStation");
		ResearchList.addResearchTaskToList("CockRing", "SEX_TOYS_AND_TOOLS", 0f, string.Empty, "MaterialSynthesisStation");
		ResearchList.addResearchTaskToList("GyroDock", "SCIENCE_EQUIPMENT", 0.3f, string.Empty, "MaterialSynthesisStation");
		ResearchList.addResearchTaskToList("VibratingCockRing", "SEX_TOYS_AND_TOOLS", 0.1f, string.Empty, "CockRing,GyroDock");
		ResearchList.addResearchTaskToList("ChemicalSynthesisBay", "SCIENCE_EQUIPMENT", 0.4f, string.Empty, string.Empty);
		ResearchList.addResearchTaskToList("Chemical.AutomaticPleasure", "CHEMICALS", 0.2f, string.Empty, "ChemicalSynthesisBay");
		ResearchList.addResearchTaskToList("Chemical.ChemicalNeutralizer", "CHEMICALS", 0.5f, string.Empty, "Chemical.AutomaticPleasure");
		ResearchList.addResearchTaskToList("Chemical.ArousalUp", "CHEMICALS", 0.1f, string.Empty, "Chemical.AutomaticPleasure");
		ResearchList.addResearchTaskToList("Chemical.OrgasmInhibitor", "CHEMICALS", 0.4f, string.Empty, "Chemical.ArousalUp");
		ResearchList.addResearchTaskToList("Chemical.SizeIncreaser", "CHEMICALS", 0.2f, "Sizeplay", "ChemicalSynthesisBay");
		ResearchList.addResearchTaskToList("ApparatusManufacturingBay", "SCIENCE_EQUIPMENT", 0.6f, string.Empty, "MaterialSynthesisStation");
		ResearchList.addResearchTaskToList("Stocks", "BONDAGE_APPARATUS", 0.5f, string.Empty, "ApparatusManufacturingBay");
		ResearchList.addResearchTaskToList("RackTable", "BONDAGE_APPARATUS", 0.5f, string.Empty, "Stocks");
		ResearchList.addResearchTaskToList("Inverter", "BONDAGE_APPARATUS", 0.5f, string.Empty, "RackTable");
		ResearchList.addResearchTaskToList("ColoredCeilingLight", "LIGHTING_AND_DECORATION", 0.1f, string.Empty, string.Empty);
		ResearchList.addResearchTaskToList("ColoredSpotlight", "LIGHTING_AND_DECORATION", 0.1f, string.Empty, "ColoredCeilingLight");
	}

	public static bool eligibleSpot(int x, int y, ResearchTask task)
	{
		return task.solutionPoints[x + y * 32] == -1;
	}

	public static ResearchTask createResearchTaskFromDefinition(string id, float difficulty, string fetish, string category, int type = -1, int bannedColor = -2)
	{
		ResearchTask researchTask = new ResearchTask();
		researchTask.id = id;
		researchTask.fetish = fetish;
		researchTask.category = category;
		researchTask.solutionPoints = new List<int>();
		researchTask.guesses = new List<int>();
		for (int i = 0; i < 128; i++)
		{
			researchTask.solutionPoints.Add(-1);
			researchTask.guesses.Add(-1);
		}
		int num = Mathf.FloorToInt(4f + 100f * difficulty);
		if (id == "PaidResearch")
		{
			researchTask.value = Mathf.FloorToInt(Mathf.Pow((float)num * 0.3f, 2f));
		}
		if (id == "ChemicalConversion")
		{
			researchTask.value = num + 4;
			researchTask.type = type;
		}
		int num2 = Mathf.FloorToInt(Random.value * 99f) % 32;
		int num3 = Mathf.FloorToInt(Random.value * 99f) % 4;
		int value = Mathf.FloorToInt(Random.value * 99f) % 6;
		bool flag = UserSettings.needTutorial("NPT_COMPLETE_A_RESEARCH_PROJECT");
		if (UserSettings.needTutorial("NPT_COMPLETE_A_RESEARCH_PROJECT"))
		{
			value = 0;
		}
		for (int j = 0; j < num; j++)
		{
			int num4 = Mathf.FloorToInt(Random.value * 99f) % 4;
			int num5 = 0;
			while (!ResearchList.eligibleSpot(num2, num3, researchTask))
			{
				if (num5 == 6)
				{
					num5 = 0;
					num2 = Mathf.FloorToInt(Random.value * 99f) % 32;
					num3 = Mathf.FloorToInt(Random.value * 99f) % 4;
				}
				switch (num4)
				{
				case 0:
					num3--;
					break;
				case 1:
					num3++;
					break;
				case 2:
					num2--;
					break;
				case 3:
					num2++;
					break;
				}
				if (num2 < 0)
				{
					num2 += 32;
				}
				if (num2 >= 32)
				{
					num2 -= 32;
				}
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num3 >= 4)
				{
					num3 = 3;
				}
				num4 = (num4 + 1) % 6;
				num5++;
			}
			researchTask.solutionPoints[num2 + num3 * 32] = Mathf.FloorToInt(Random.value * 99f) % 6;
			if (Random.value > 0.8f || flag)
			{
				researchTask.solutionPoints[num2 + num3 * 32] = value;
				flag = false;
			}
			if (researchTask.solutionPoints[num2 + num3 * 32] == bannedColor)
			{
				researchTask.solutionPoints[num2 + num3 * 32] = (researchTask.solutionPoints[num2 + num3 * 32] + 1) % 6;
			}
		}
		return researchTask;
	}

	public static void addResearchTaskToList(string id, string category, float difficulty, string fetish = "", string prerequisites = "")
	{
		ResearchTaskDefinition researchTaskDefinition = new ResearchTaskDefinition();
		researchTaskDefinition.id = id;
		researchTaskDefinition.category = category;
		researchTaskDefinition.difficulty = difficulty;
		researchTaskDefinition.fetish = fetish;
		researchTaskDefinition.prerequisite = prerequisites;
		ResearchList.allTasksAvailable.Add(researchTaskDefinition);
	}
}
