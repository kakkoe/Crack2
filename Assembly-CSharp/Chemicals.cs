using System.Collections.Generic;
using UnityEngine;

public class Chemicals
{
	public static float doseDuration = 30f;

	public static void processChemical(RackCharacter character, string chemical, float amountInSystem)
	{
		Chemicals.doseDuration = 30f;
		if (chemical != null)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
			dictionary.Add("Chemical.ChemicalNeutralizer", 1);
			dictionary.Add("Chemical.OrgasmInhibitor", 2);
			dictionary.Add("Chemical.OrgasmInhibitor!", 3);
			dictionary.Add("Chemical.AutomaticPleasure", 4);
			dictionary.Add("Chemical.AutomaticPleasure!", 5);
			dictionary.Add("Chemical.ArousalUp", 6);
			dictionary.Add("Chemical.ArousalUp!", 7);
			dictionary.Add("Chemical.SizeIncreaser", 8);
			dictionary.Add("Chemical.SizeIncreaser!", 9);
			int num = default(int);
			if (dictionary.TryGetValue(chemical, out num))
			{
				switch (num)
				{
				case 1:
					for (int i = 0; i < character.chemicalsInSystem.Count; i++)
					{
						if (character.chemicalsInSystem[i].name != chemical)
						{
							Chemicals.doseDuration = 1f;
							character.chemicalsInSystem[i].amountOwned -= Time.deltaTime * amountInSystem * 25f;
							if (character.chemicalsInSystem[i].amountOwned < 0f)
							{
								character.chemicalsInSystem[i].amountOwned = 0f;
							}
						}
					}
					break;
				case 2:
					Chemicals.doseDuration = 240f;
					character.orgasmPrevention = amountInSystem;
					break;
				case 3:
					Chemicals.doseDuration = 10f;
					character.artificialOrgasm = amountInSystem * 2f;
					break;
				case 4:
					Chemicals.doseDuration = 240f;
					character.stimulate(Time.deltaTime * (0.2f + amountInSystem) * 100f);
					break;
				case 5:
					Chemicals.doseDuration = 10f;
					character.hurt(Time.deltaTime * (0.2f + amountInSystem) * 300f, false);
					break;
				case 6:
					if (character.arousal < 1f)
					{
						character.arousal += Time.deltaTime * 0.03f;
						Chemicals.doseDuration = 120f;
					}
					break;
				case 7:
					if (character.arousal > 0f)
					{
						character.arousal -= Time.deltaTime * 0.03f;
						Chemicals.doseDuration = 120f;
					}
					break;
				case 8:
					character.artificialBigness = amountInSystem;
					Chemicals.doseDuration = 10000f;
					break;
				case 9:
					character.artificialSmallness = amountInSystem;
					Chemicals.doseDuration = 10000f;
					break;
				}
			}
		}
		character.addChemicalCompound(chemical, (0f - Time.deltaTime) / Chemicals.doseDuration);
	}
}
