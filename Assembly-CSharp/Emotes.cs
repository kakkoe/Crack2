using UnityEngine;

public class Emotes
{
	public static RackCharacter dCharacter;

	public static RackCharacter talkingToCharacter;

	public static int insertIndex = 0;

	public static string insertString = string.Empty;

	public static char[] interjectionPunc = new char[8]
	{
		'.',
		'!',
		'?',
		'~',
		'`',
		'，',
		'。',
		'、'
	};

	public static bool useDomTitle = false;

	public static bool insertingATitle = false;

	public static bool titlesInitialized = false;

	public static string[] respectful_name_for_female;

	public static string[] respectful_name_for_male;

	public static string[] affectionate_name_for_female;

	public static string[] affectionate_name_for_effeminate_male;

	public static string[] affectionate_name_for_masculine_male;

	public static string[] derogatory_slur_prefix_phrases;

	public static string[] derogatory_slurs_for_female;

	public static string[] derogatory_slurs_for_effeminate_male;

	public static string[] derogatory_slurs_for_masculine_male;

	public static string formEmoteFromThought(string thought, RackCharacter character)
	{
		if (character.effectivePersonality == string.Empty)
		{
			character.effectivePersonality = "shy";
		}
		string phrase;
		if (character.controlledByPlayer)
		{
			phrase = Localization.getPhrase("EMOTES_" + thought + "." + character.characterVoice, string.Empty);
			Emotes.dCharacter = character.interactionSubject;
			Emotes.talkingToCharacter = character.interactionSubject;
		}
		else
		{
			phrase = Localization.getPhrase("EMOTES_" + thought + "." + character.effectivePersonality, string.Empty);
			Emotes.dCharacter = character;
			Emotes.talkingToCharacter = Game.gameInstance.PC();
		}
		string[] array = phrase.Split('\n');
		int num = 1 + Mathf.FloorToInt(Random.value * 999f) % (array.Length - 2);
		if (array.Length < 4)
		{
			return "NOT_ENOUGH_EMOTE_OPTIONS FOR: " + thought;
		}
		int num2 = 0;
		while (true)
		{
			if (!(array[num] == character.emoteString) && !(array[num].Trim() == string.Empty))
			{
				break;
			}
			if (num2 < array[num].Length)
			{
				num = (num + 1) % array.Length;
				num2++;
				continue;
			}
			break;
		}
		if (array[num].Contains("[notitle]"))
		{
			return Game.animateDialogue(Game.dialogueFormat(array[num]), 10000f, Emotes.dCharacter);
		}
		return Emotes.interjectTitles(Game.animateDialogue(Game.dialogueFormat(array[num]), 10000f, Emotes.dCharacter), character, Emotes.talkingToCharacter);
	}

	public static string interjectTitles(string phrase, RackCharacter character, RackCharacter talkingToCharacter)
	{
		if (!Emotes.titlesInitialized)
		{
			Emotes.respectful_name_for_female = Localization.getPhrase("respectful_name_for_female", string.Empty).Split('#');
			Emotes.respectful_name_for_male = Localization.getPhrase("respectful_name_for_male", string.Empty).Split('#');
			Emotes.affectionate_name_for_female = Localization.getPhrase("affectionate_name_for_female", string.Empty).Split('#');
			Emotes.affectionate_name_for_effeminate_male = Localization.getPhrase("affectionate_name_for_effeminate_male", string.Empty).Split('#');
			Emotes.affectionate_name_for_masculine_male = Localization.getPhrase("affectionate_name_for_masculine_male", string.Empty).Split('#');
			Emotes.derogatory_slur_prefix_phrases = Localization.getPhrase("derogatory_slur_prefix_phrases", string.Empty).Split('#');
			Emotes.derogatory_slurs_for_female = Localization.getPhrase("derogatory_slurs_for_female", string.Empty).Split('#');
			Emotes.derogatory_slurs_for_effeminate_male = Localization.getPhrase("derogatory_slurs_for_effeminate_male", string.Empty).Split('#');
			Emotes.derogatory_slurs_for_masculine_male = Localization.getPhrase("derogatory_slurs_for_masculine_male", string.Empty).Split('#');
			Emotes.titlesInitialized = true;
		}
		Emotes.useDomTitle = false;
		Emotes.insertingATitle = false;
		if (character.controlledByPlayer)
		{
			switch (character.effectivePersonality)
			{
			case "academic":
				if (Random.value > 0.8f)
				{
					Emotes.insertString = talkingToCharacter.interactionSubject.data.name.Split(' ')[0];
					Emotes.insertingATitle = true;
				}
				break;
			case "pleasant":
				if (Random.value > 0.8f)
				{
					if (talkingToCharacter.data.identifiesMale)
					{
						if (talkingToCharacter.totalFemininity > 0.4f)
						{
							Emotes.insertString = Emotes.affectionate_name_for_effeminate_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.affectionate_name_for_effeminate_male.Length];
						}
						else
						{
							Emotes.insertString = Emotes.affectionate_name_for_masculine_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.affectionate_name_for_masculine_male.Length];
						}
					}
					else
					{
						Emotes.insertString = Emotes.affectionate_name_for_female[Mathf.FloorToInt(Random.value * 999f) % Emotes.affectionate_name_for_female.Length];
					}
					Emotes.insertingATitle = true;
				}
				break;
			case "gruff":
				if (Random.value > 0.8f)
				{
					if (talkingToCharacter.data.identifiesMale)
					{
						if (talkingToCharacter.totalFemininity > 0f || talkingToCharacter.dominance < -0.65f)
						{
							Emotes.insertString = Emotes.derogatory_slurs_for_effeminate_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slurs_for_effeminate_male.Length];
						}
						else
						{
							Emotes.insertString = Emotes.derogatory_slurs_for_masculine_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slurs_for_masculine_male.Length];
						}
					}
					else
					{
						Emotes.insertString = Emotes.derogatory_slurs_for_female[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slurs_for_female.Length];
					}
					if (Random.value > 0.5f)
					{
						Emotes.insertString = Emotes.derogatory_slur_prefix_phrases[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slur_prefix_phrases.Length] + " " + Emotes.insertString;
					}
					Emotes.insertingATitle = true;
				}
				break;
			}
		}
		else
		{
			if (character.getCommandStatus("use_dom_name") > 0 || character.getCommandStatus("use_dom_name") == -1)
			{
				if (-1.25f + Random.value * 8f > character.dominance)
				{
					Emotes.useDomTitle = true;
					character.setCommandStatus("use_dom_name", 2);
					character.dominance -= 0.02f;
				}
				else
				{
					character.setCommandStatus("use_dom_name", -1);
				}
			}
			if (Emotes.useDomTitle)
			{
				Emotes.insertString = Inventory.data.domName;
				Emotes.insertingATitle = true;
			}
			else if (character.aggression < -0.5f || character.dominance < -0.8f)
			{
				if (Random.value < 0.3f)
				{
					if (talkingToCharacter.data.identifiesMale)
					{
						Emotes.insertString = Emotes.respectful_name_for_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.respectful_name_for_male.Length];
					}
					else
					{
						Emotes.insertString = Emotes.respectful_name_for_female[Mathf.FloorToInt(Random.value * 999f) % Emotes.respectful_name_for_female.Length];
					}
					Emotes.insertingATitle = true;
				}
			}
			else if (character.aggression > 0.5f)
			{
				if ((character.getCommandStatus("dont_be_rude") <= 0 || !(Random.value > 1f + character.dominance)) && 1f - Mathf.Pow(Random.value, 2f) < character.aggression - 0.5f)
				{
					if (talkingToCharacter.data.identifiesMale)
					{
						if (talkingToCharacter.totalFemininity > 0f || talkingToCharacter.dominance < -0.65f)
						{
							Emotes.insertString = Emotes.derogatory_slurs_for_effeminate_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slurs_for_effeminate_male.Length];
						}
						else
						{
							Emotes.insertString = Emotes.derogatory_slurs_for_masculine_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slurs_for_masculine_male.Length];
						}
					}
					else
					{
						Emotes.insertString = Emotes.derogatory_slurs_for_female[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slurs_for_female.Length];
					}
					if (Random.value > 0.5f)
					{
						Emotes.insertString = Emotes.derogatory_slur_prefix_phrases[Mathf.FloorToInt(Random.value * 999f) % Emotes.derogatory_slur_prefix_phrases.Length] + " " + Emotes.insertString;
					}
					Emotes.insertingATitle = true;
					character.setCommandStatus("dont_be_rude", -1);
				}
			}
			else if (2f - Mathf.Pow(Random.value, 2f) * 2f < character.talkativeness)
			{
				if (talkingToCharacter.data.identifiesMale)
				{
					if (talkingToCharacter.totalFemininity > 0.4f)
					{
						Emotes.insertString = Emotes.affectionate_name_for_effeminate_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.affectionate_name_for_effeminate_male.Length];
					}
					else
					{
						Emotes.insertString = Emotes.affectionate_name_for_masculine_male[Mathf.FloorToInt(Random.value * 999f) % Emotes.affectionate_name_for_masculine_male.Length];
					}
				}
				else
				{
					Emotes.insertString = Emotes.affectionate_name_for_female[Mathf.FloorToInt(Random.value * 999f) % Emotes.affectionate_name_for_female.Length];
				}
				Emotes.insertingATitle = true;
			}
		}
		if (Emotes.insertingATitle && Emotes.insertString.Length > 0)
		{
			phrase = phrase.Replace("...", "`");
			Emotes.insertIndex = phrase.LastIndexOfAny(Emotes.interjectionPunc);
			if (Emotes.insertIndex != -1)
			{
				phrase = phrase.Insert(Emotes.insertIndex, ", " + Emotes.insertString);
				phrase = phrase.Replace("`", "...");
			}
		}
		return phrase;
	}
}
