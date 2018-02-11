using System.Collections.Generic;
using UnityEngine;

public class SexualPreferences
{
	public static string category;

	public static Dictionary<string, SexualPreference> preferences = new Dictionary<string, SexualPreference>();

	public static Dictionary<string, Confidence> confidences = new Dictionary<string, Confidence>();

	public static bool initted = false;

	public static float preferenceIndifferenceRange = 0.45f;

	public static SexualPreference getPreference(string name)
	{
		return SexualPreferences.preferences[name];
	}

	public static void init()
	{
		if (!SexualPreferences.initted)
		{
			SexualPreferences.initted = true;
			SexualPreferences.addConfidence(0.7f, "penis_size", false);
			SexualPreferences.addConfidence(0.5f, "ball_size", false);
			SexualPreferences.addConfidence(0.7f, "vagina_tightness", false);
			SexualPreferences.addConfidence(0.7f, "ass_cuteness", false);
			SexualPreferences.addConfidence(0.6f, "masculinity", true);
			SexualPreferences.addConfidence(-0.5f, "adiposity", false);
			SexualPreferences.addConfidence(0.2f, "muscularity", false);
			SexualPreferences.addConfidence(-0.3f, "breast_size", true);
			SexualPreferences.addConfidence(0.3f, "height", true);
			SexualPreferences.category = "PREFERENCE_CATEGORY_ATTRACTION";
			SexualPreferences.addPreference(0, 0.5f, "category_attraction", 1);
			SexualPreferences.addPreference(3, 1f, "men", 0);
			SexualPreferences.addPreference(2, 1f, "women", 0);
			SexualPreferences.addPreference(1, 0.7f, "herms", 0);
			SexualPreferences.addPreference(1, 0.5f, "cboys", 0);
			SexualPreferences.addPreference(1, 0.5f, "dgirls", 0);
			SexualPreferences.addPreference(1, 0.2f, "nulls", 1);
			SexualPreferences.addPreference(0, 0.8f, "exaggerated_masculinity", 0);
			SexualPreferences.addPreference(1, 0.5f, "countermasculinity", 0);
			SexualPreferences.addPreference(0, 0.9f, "larger_partners", 0);
			SexualPreferences.addPreference(0, 0.9f, "smaller_partners", 0);
			SexualPreferences.category = "PREFERENCE_CATEGORY_EXPERIENCE";
			SexualPreferences.addPreference(0, 0.5f, "category_experiences", 1);
			SexualPreferences.addPreference(0, 0.5f, "edging", 0);
			SexualPreferences.addPreference(0, 0.7f, "anticipation", 0);
			SexualPreferences.addPreference(0, 0.8f, "partner_satisfaction", 0);
			SexualPreferences.addPreference(1, 0.1f, "denial", 0);
			SexualPreferences.addPreference(0, 0.9f, "multiple_orgasms", 0);
			SexualPreferences.addPreference(1, 0.4f, "automated_stimulation", 0);
			SexualPreferences.addPreference(1, 0.2f, "overstimulation", 0);
			SexualPreferences.addPreference(1, 0f, "pain", 0);
			SexualPreferences.addPreference(0, 0.5f, "group_sex", 0);
			SexualPreferences.addPreference(1, 0.3f, "exhibitionism", 0);
			SexualPreferences.addPreference(1, 0f, "degradation", 0);
			SexualPreferences.addPreference(1, 0f, "submission", 0);
			SexualPreferences.addPreference(1, 0.5f, "mad_science", 0);
			SexualPreferences.category = "PREFERENCE_CATEGORY_INTERACTIONS";
			SexualPreferences.addPreference(0, 0.5f, "category_sensations", 1);
			SexualPreferences.addPreference(3, 0.9f, "handjob_giving", 1);
			SexualPreferences.addPreference(0, 1f, "handjob_receiving", 2);
			SexualPreferences.addPreference(2, 0.9f, "clitrub_giving", 1);
			SexualPreferences.addPreference(0, 1f, "clitrub_receiving", 3);
			SexualPreferences.addPreference(2, 0.9f, "vaginalfingering_giving", 1);
			SexualPreferences.addPreference(0, 1f, "vaginalfingering_receiving", 3);
			SexualPreferences.addPreference(1, 0.8f, "analfingering_giving", 1);
			SexualPreferences.addPreference(0, 0.7f, "analfingering_receiving", 0);
			SexualPreferences.addPreference(2, 1f, "vaginalfucking_giving", 2);
			SexualPreferences.addPreference(0, 0.9f, "vaginalfucking_receiving", 3);
			SexualPreferences.addPreference(2, 0.9f, "analfucking_giving", 2);
			SexualPreferences.addPreference(0, 0.7f, "analfucking_receiving", 0);
			SexualPreferences.addPreference(3, 0.8f, "blowjob_giving", 0);
			SexualPreferences.addPreference(0, 1f, "blowjob_receiving", 2);
			SexualPreferences.addPreference(3, 0.8f, "cunnilingus_giving", 0);
			SexualPreferences.addPreference(0, 1f, "cunnilingus_receiving", 2);
			SexualPreferences.addPreference(1, 0.1f, "rimming_giving", 0);
			SexualPreferences.addPreference(1, 0.5f, "rimming_receiving", 0);
			SexualPreferences.addPreference(0, 0.7f, "sextoy_general", 1);
			SexualPreferences.addPreference(0, 0.7f, "sextoy_vaginal", 3);
			SexualPreferences.addPreference(0, 0.7f, "sextoy_anal", 1);
			SexualPreferences.addPreference(0, 1f, "sextoy_penis", 2);
			SexualPreferences.addPreference(0, 1f, "sextoy_clitoris", 3);
			SexualPreferences.addPreference(0, 0.9f, "vibration", 0);
		}
	}

	public static void createObjectivesForTestSubject(RackCharacter subject)
	{
		if (UserSettings.needTutorial("NPT_SAVE_MONEY_FOR_MSS"))
		{
			Objectives.addObjective(0, "attention", Localization.getSubPhrase("GIVE_THE_SUBJECT_X_SECONDS_OF_ATTENTION", subject.data.name, 45.ToString(), string.Empty), 45f, subject, false, false, null);
			Objectives.addObjective(0, "edging", Localization.getSubPhrase("EDGE_THE_SUBJECT_FOR_X_SECONDS", subject.data.name, 15.ToString(), string.Empty), 15f, subject, false, false, null);
			Objectives.addObjective(0, "orgasm", Localization.getSubPhrase("BRING_THE_SUBJECT_TO_ORGASM", subject.data.name, string.Empty, string.Empty), 1f, subject, false, false, null);
			Objectives.addObjective(0, "pain", Localization.getSubPhrase("DONT_HURT_THE_SUBJECT", subject.data.name, string.Empty, string.Empty), 0.5f + Game.cap(subject.preferences["pain"] + 0.5f, 0f, 1f), subject, true, true, null);
		}
		else
		{
			if (subject.preferences["anticipation"] > 1.5f)
			{
				Objectives.addObjective(0, "attention", Localization.getSubPhrase("GIVE_THE_SUBJECT_X_SECONDS_OF_ATTENTION", subject.data.name, 120.ToString(), string.Empty), 120f, subject, false, false, null);
			}
			else if (subject.preferences["anticipation"] > 1f)
			{
				Objectives.addObjective(0, "attention", Localization.getSubPhrase("GIVE_THE_SUBJECT_X_SECONDS_OF_ATTENTION", subject.data.name, 90.ToString(), string.Empty), 90f, subject, false, false, null);
			}
			else if (subject.preferences["anticipation"] > 0.75f)
			{
				Objectives.addObjective(0, "attention", Localization.getSubPhrase("GIVE_THE_SUBJECT_X_SECONDS_OF_ATTENTION", subject.data.name, 60.ToString(), string.Empty), 60f, subject, false, false, null);
			}
			else if (subject.preferences["anticipation"] > 0.5f)
			{
				Objectives.addObjective(0, "attention", Localization.getSubPhrase("GIVE_THE_SUBJECT_X_SECONDS_OF_ATTENTION", subject.data.name, 45.ToString(), string.Empty), 45f, subject, false, false, null);
			}
			else
			{
				Objectives.addObjective(0, "attention", Localization.getSubPhrase("GIVE_THE_SUBJECT_X_SECONDS_OF_ATTENTION", subject.data.name, 30.ToString(), string.Empty), 30f, subject, false, false, null);
			}
			if (Random.value > subject.preferences["denial"])
			{
				if (subject.preferences["multiple_orgasms"] > 1.5f && Random.value > 0.7f)
				{
					Objectives.addObjective(0, "orgasm", Localization.getSubPhrase("BRING_THE_SUBJECT_TO_ORGASM_X_TIMES", subject.data.name, 4.ToString(), string.Empty), 4f, subject, false, false, null);
				}
				else if (subject.preferences["multiple_orgasms"] > 1f && Random.value > 0.6f)
				{
					Objectives.addObjective(0, "orgasm", Localization.getSubPhrase("BRING_THE_SUBJECT_TO_ORGASM_X_TIMES", subject.data.name, 3.ToString(), string.Empty), 3f, subject, false, false, null);
				}
				else if (subject.preferences["multiple_orgasms"] > 0.75f && Random.value > 0.5f)
				{
					Objectives.addObjective(0, "orgasm", Localization.getSubPhrase("BRING_THE_SUBJECT_TO_ORGASM_X_TIMES", subject.data.name, 2.ToString(), string.Empty), 2f, subject, false, false, null);
				}
				else
				{
					Objectives.addObjective(0, "orgasm", Localization.getSubPhrase("BRING_THE_SUBJECT_TO_ORGASM", subject.data.name, string.Empty, string.Empty), 1f, subject, false, false, null);
				}
			}
			if (subject.preferences["sextoy_general"] > 0.5f)
			{
				Objectives.addObjective(0, "sextoy_orgasm", Localization.getSubPhrase("BRING_THE_SUBJECT_TO_ORGASM_USING_SEX_TOY", subject.data.name, string.Empty, string.Empty), 1f, subject, false, false, null);
			}
			if (subject.preferences["edging"] > 1.5f)
			{
				Objectives.addObjective(0, "edging", Localization.getSubPhrase("EDGE_THE_SUBJECT_FOR_X_SECONDS", subject.data.name, 120.ToString(), string.Empty), 120f, subject, false, false, null);
			}
			else if (subject.preferences["edging"] > 1f)
			{
				Objectives.addObjective(0, "edging", Localization.getSubPhrase("EDGE_THE_SUBJECT_FOR_X_SECONDS", subject.data.name, 60.ToString(), string.Empty), 60f, subject, false, false, null);
			}
			else if (subject.preferences["edging"] > 0.75f)
			{
				Objectives.addObjective(0, "edging", Localization.getSubPhrase("EDGE_THE_SUBJECT_FOR_X_SECONDS", subject.data.name, 30.ToString(), string.Empty), 30f, subject, false, false, null);
			}
			else if (subject.preferences["edging"] > 0.25f)
			{
				Objectives.addObjective(0, "edging", Localization.getSubPhrase("EDGE_THE_SUBJECT_FOR_X_SECONDS", subject.data.name, 15.ToString(), string.Empty), 15f, subject, false, false, null);
			}
			if (!(subject.preferences["pain"] > 0.5f))
			{
				Objectives.addObjective(0, "pain", Localization.getSubPhrase("DONT_HURT_THE_SUBJECT", subject.data.name, string.Empty, string.Empty), 0.5f + Game.cap(subject.preferences["pain"] + 0.5f, 0f, 1f), subject, true, true, null);
			}
		}
	}

	public static void addConfidence(float defaultValue, string id, bool invertIfFemale = false)
	{
		Confidence confidence = new Confidence();
		confidence.id = id;
		confidence.defaultValue = defaultValue;
		confidence.invertIfFemale = invertIfFemale;
		SexualPreferences.confidences.Add(id, confidence);
	}

	public static void addPreference(int keepSecret, float defaultValue, string id, int hideFromPreview = 0)
	{
		SexualPreference sexualPreference = new SexualPreference();
		sexualPreference.keepSecret = keepSecret;
		sexualPreference.category = SexualPreferences.category;
		sexualPreference.id = id;
		sexualPreference.defaultValue = defaultValue;
		sexualPreference.hideFromPreview = hideFromPreview;
		SexualPreferences.preferences.Add(id, sexualPreference);
	}
}
