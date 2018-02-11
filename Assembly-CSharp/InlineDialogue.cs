using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InlineDialogue
{
	public static float randomSwing;

	public static float friendlinessVolatility = 2f;

	public static int rand;

	public static int commentDirection;

	public static List<string> options;

	public static void tellQuipToSubject(string quip, RackCharacter subject, RackCharacter speaker, float staleness)
	{
		InlineDialogue.randomSwing = Random.value * 0.4f - 0.2f;
		if (subject == null)
		{
			subject = speaker;
		}
		subject.helloPhase = 3;
		quip = quip.Replace("_REPEAT", string.Empty);
		quip = quip.Replace("quip.", string.Empty);
		subject.emoteThought = string.Empty;
		subject.emoteTime = 0f;
		subject.emoteString = string.Empty;
		if (quip.Contains("comment_on_"))
		{
			quip = quip.Replace("comment_on_attraction_", string.Empty);
			quip = quip.Replace("comment_on_interaction_", string.Empty);
			quip = quip.Replace("comment_on_experience_", string.Empty);
			quip = quip.Replace("exaggerated_masculinity_male", "exaggerated_masculinity");
			quip = quip.Replace("exaggerated_masculinity_female", "exaggerated_masculinity");
			quip = quip.Replace("countermasculinity_male", "countermasculinity");
			quip = quip.Replace("countermasculinity_female", "countermasculinity");
			float num = 0f;
			if (quip == "submission_sub")
			{
				quip = "submission";
				num = subject.getPreference(quip) - 0.5f;
			}
			else if (quip == "submission_dom")
			{
				quip = "submission";
				num = subject.getPreference(quip) - 0.5f;
				num *= -1f;
			}
			else
			{
				num = subject.getPreference(quip) - 0.5f;
			}
			subject.arouseByThought(Game.cap(num * 0.1f, 0f, 1f), 4f);
			if (subject.rebellious)
			{
				subject.think("rebellious_response", 1f, true, false);
			}
			else if (subject.getPreference(quip) > 1.25f)
			{
				subject.think("i_love_that_comment", 1f, true, false);
				subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.15f;
			}
			else if (subject.getPreference(quip) > 0.5f)
			{
				subject.think("i_like_that_comment", 1f, true, false);
				subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.05f;
			}
			else if (subject.getPreference(quip) > 0.2f)
			{
				subject.think("i_dislike_that_comment", 1f, true, false);
				subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.01f;
			}
			else
			{
				subject.think("i_hate_that_comment", 1f, true, false);
			}
		}
		else if (quip != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(71);
			dictionary.Add("greet", 0);
			dictionary.Add("be_right_back", 1);
			dictionary.Add("goodbye_friendly", 2);
			dictionary.Add("goodbye_unfriendly", 3);
			dictionary.Add("calm_down", 4);
			dictionary.Add("relax", 4);
			dictionary.Add("be_comfortable", 4);
			dictionary.Add("what_do_you_like", 5);
			dictionary.Add("ask_about_secrets", 6);
			dictionary.Add("ask_what_they_dislike", 7);
			dictionary.Add("reprimand_speech", 8);
			dictionary.Add("reprimand_rudeness", 9);
			dictionary.Add("reprimand_failure_to_use_title", 10);
			dictionary.Add("reprimand_cumming", 11);
			dictionary.Add("reprimand_disobedience", 12);
			dictionary.Add("less_talking", 13);
			dictionary.Add("use_your_mouth", 14);
			dictionary.Add("stop_squirming", 15);
			dictionary.Add("beg", 16);
			dictionary.Add("demean_self", 17);
			dictionary.Add("order_to_cum", 18);
			dictionary.Add("order_to_not_cum", 19);
			dictionary.Add("use_dom_name", 20);
			dictionary.Add("demean", 21);
			dictionary.Add("empower", 22);
			dictionary.Add("insult_fetishes", 23);
			dictionary.Add("encourage_obedience", 24);
			dictionary.Add("encourage_fetishes", 25);
			dictionary.Add("compliment_masculinity_male", 26);
			dictionary.Add("compliment_femininity_male", 27);
			dictionary.Add("compliment_masculinity_female", 28);
			dictionary.Add("compliment_femininity_female", 29);
			dictionary.Add("compliment_soft_body", 30);
			dictionary.Add("compliment_thin_body", 31);
			dictionary.Add("compliment_big_muscles", 32);
			dictionary.Add("compliment_small_muscles", 33);
			dictionary.Add("compliment_tall", 34);
			dictionary.Add("compliment_short", 35);
			dictionary.Add("compliment_tight_pecs", 36);
			dictionary.Add("compliment_large_penis", 37);
			dictionary.Add("compliment_small_penis", 38);
			dictionary.Add("compliment_large_balls", 39);
			dictionary.Add("compliment_small_balls", 40);
			dictionary.Add("compliment_tight_vagina", 41);
			dictionary.Add("compliment_soft_vagina", 42);
			dictionary.Add("compliment_cute_ass", 43);
			dictionary.Add("compliment_masculine_ass", 44);
			dictionary.Add("compliment_large_breasts", 45);
			dictionary.Add("compliment_small_breasts", 46);
			dictionary.Add("insult_masculinity_male", 47);
			dictionary.Add("insult_femininity_male", 48);
			dictionary.Add("insult_masculinity_female", 49);
			dictionary.Add("insult_femininity_female", 50);
			dictionary.Add("insult_soft_body", 51);
			dictionary.Add("insult_thin_body", 52);
			dictionary.Add("insult_big_muscles", 53);
			dictionary.Add("insult_small_muscles", 54);
			dictionary.Add("insult_tall", 55);
			dictionary.Add("insult_short", 56);
			dictionary.Add("insult_soft_manboobs", 57);
			dictionary.Add("insult_large_penis", 58);
			dictionary.Add("insult_small_penis", 59);
			dictionary.Add("insult_large_balls", 60);
			dictionary.Add("insult_small_balls", 61);
			dictionary.Add("insult_tight_vagina", 62);
			dictionary.Add("insult_soft_vagina", 63);
			dictionary.Add("insult_cute_ass", 64);
			dictionary.Add("insult_masculine_ass", 65);
			dictionary.Add("insult_large_breasts", 66);
			dictionary.Add("insult_small_breasts", 67);
			dictionary.Add("generic_comment", 68);
			int num2 = default(int);
			if (dictionary.TryGetValue(quip, out num2))
			{
				switch (num2)
				{
				case 0:
					if (staleness > 0f)
					{
						subject.friendlinessToPlayer -= 0.15f;
						subject.think("hello2", 1f, true, false);
					}
					subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.08f;
					break;
				case 1:
					if (staleness > 0f)
					{
						subject.friendlinessToPlayer -= 0.05f;
					}
					if (subject.beingStimulatedAutomatically)
					{
						if (subject.refractory > 0f || subject.proximityToOrgasm > 0.75f)
						{
							if (subject.getPreference("automated_stimulation") > 0.5f)
							{
								subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.15f;
								subject.think("beg_player_not_to_leave_automation_on", 1f, true, false);
							}
							else
							{
								subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.55f;
								subject.think("beg_player_not_to_leave_automation_on", 1f, true, false);
							}
						}
						else if (subject.getPreference("automated_stimulation") > 0.5f)
						{
							subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.05f;
							subject.think("respond_to_brb", 1f, true, false);
						}
						else
						{
							subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
							subject.think("hurry_back", 1f, true, false);
						}
					}
					else if (subject.aggression > 0.4f)
					{
						subject.think("hurry_back", 1f, true, false);
						subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					}
					else
					{
						subject.think("respond_to_brb", 1f, true, false);
						subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.05f;
					}
					break;
				case 2:
					if (staleness > 0f)
					{
						subject.friendlinessToPlayer -= 0.15f;
					}
					subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.05f;
					if (subject.satisfaction > 0.285f)
					{
						subject.think("goodbye_satisfied", 1f, true, false);
					}
					else if (subject.satisfaction > 0.1f)
					{
						subject.think("goodbye_satisfied", 1f, true, false);
					}
					else
					{
						subject.think("goodbye_furious", 1f, true, false);
					}
					break;
				case 3:
					if (staleness > 0f)
					{
						subject.friendlinessToPlayer -= 1f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					if (subject.satisfaction > 0.425f)
					{
						subject.think("goodbye_dissatisfied", 1f, true, false);
					}
					else
					{
						subject.think("goodbye_furious", 1f, true, false);
					}
					break;
				case 4:
				{
					float num4 = 0f - subject.aggression;
					num4 /= 1f + staleness / 10f;
					if (Mathf.Abs(num4) > subject.friendlinessToPlayer * 0.5f)
					{
						num4 = ((!(num4 > 0f)) ? (subject.friendlinessToPlayer * -0.5f) : (subject.friendlinessToPlayer * 0.5f));
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * Mathf.Abs(num4) * 2f;
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.aggression += num4 * 0.2f;
					}
					else
					{
						subject.aggression += num4;
						if (Mathf.Abs(num4) > 0.1f)
						{
							subject.think("calm_down", 1f, true, false);
						}
						else
						{
							subject.think("reluctant_calm_down", 1f, true, false);
						}
					}
					break;
				}
				case 5:
					if (subject.rebellious && !UserSettings.needTutorial("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES"))
					{
						subject.think("rebellious_answer_to_inquisition", 1f, true, false);
					}
					else if (Random.value - 0.5f + subject.dominance * 0.3f < subject.friendlinessToPlayer - staleness / 50f || UserSettings.needTutorial("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES"))
					{
						subject.think("describe_a_preference", 1f, true, false);
						subject.friendlinessToPlayer *= 0.5f;
					}
					else
					{
						subject.think("annoyed_with_questions", 1f, true, false);
						subject.aggression += 0.025f;
						subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.25f;
					}
					break;
				case 6:
					if (subject.rebellious)
					{
						subject.think("rebellious_answer_to_inquisition", 1f, true, false);
					}
					else if (Random.value + 0.5f + subject.dominance * 0.3f < subject.friendlinessToPlayer - staleness / 50f)
					{
						subject.think("describe_a_secret_preference", 1f, true, false);
						subject.friendlinessToPlayer *= 0.5f;
					}
					else
					{
						subject.think("thats_private", 1f, true, false);
						subject.aggression += 0.05f;
						subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.25f;
					}
					break;
				case 7:
					if (subject.rebellious)
					{
						subject.think("rebellious_answer_to_inquisition", 1f, true, false);
					}
					else if (Random.value - 0.5f + subject.dominance * 0.3f < subject.friendlinessToPlayer - staleness / 50f)
					{
						subject.think("describe_a_dislike", 1f, true, false);
						subject.friendlinessToPlayer *= 0.5f;
					}
					else
					{
						subject.think("annoyed_with_questions", 1f, true, false);
						subject.aggression += 0.025f;
						subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.25f;
					}
					break;
				case 8:
					subject.timeSinceDisobedienceOrReprimand = 0f;
					if (subject.getCommandStatus("less_talking") == -1)
					{
						if (subject.rebellious)
						{
							subject.think("rebellious_response_to_command", 1f, true, false);
							subject.setCommandStatus("less_talking", -1);
							subject.dominance += 0.03f;
						}
						else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.5f + InlineDialogue.randomSwing)
						{
							subject.think("refuse_command", 1f, true, false);
							subject.setCommandStatus("less_talking", -1);
							subject.dominance += 0.03f;
						}
						else
						{
							subject.think("apologize_for_disobedience", 1f, true, false);
							subject.setCommandStatus("less_talking", 1);
							subject.talkativeness *= 0.3f;
						}
					}
					else if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("less_talking", -1);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.9f + InlineDialogue.randomSwing)
					{
						subject.think("confused_by_reprimand", 1f, true, false);
						subject.setCommandStatus("less_talking", 1);
					}
					else
					{
						subject.think("apologize_for_disobedience", 1f, true, false);
						subject.setCommandStatus("less_talking", 1);
						subject.talkativeness *= 0.3f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.25f;
					break;
				case 9:
					subject.timeSinceDisobedienceOrReprimand = 0f;
					if (subject.getCommandStatus("dont_be_rude") == -1)
					{
						if (subject.rebellious)
						{
							subject.think("rebellious_response_to_command", 1f, true, false);
							subject.setCommandStatus("dont_be_rude", -1);
							subject.dominance += 0.03f;
						}
						else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > 0.2f + InlineDialogue.randomSwing)
						{
							subject.think("refuse_command", 1f, true, false);
							subject.setCommandStatus("dont_be_rude", -1);
							subject.dominance += 0.03f;
						}
						else
						{
							subject.think("apologize_for_disobedience", 1f, true, false);
							subject.setCommandStatus("dont_be_rude", 1);
						}
					}
					else if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("dont_be_rude", -1);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.8f + InlineDialogue.randomSwing)
					{
						subject.think("confused_by_reprimand", 1f, true, false);
						subject.setCommandStatus("dont_be_rude", 1);
						subject.talkativeness *= 0.9f;
					}
					else
					{
						subject.think("apologize_for_disobedience", 1f, true, false);
						subject.setCommandStatus("dont_be_rude", 1);
						subject.talkativeness *= 0.5f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.25f;
					break;
				case 10:
					subject.timeSinceDisobedienceOrReprimand = 0f;
					if (subject.getCommandStatus("use_dom_name") == -1)
					{
						if (subject.rebellious)
						{
							subject.think("rebellious_response_to_command", 1f, true, false);
						}
						else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.5f + InlineDialogue.randomSwing)
						{
							subject.think("refuse_command", 1f, true, false);
						}
						else
						{
							subject.think("apologize_for_disobedience", 1f, true, false);
						}
					}
					else if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.9f + InlineDialogue.randomSwing)
					{
						subject.think("confused_by_reprimand", 1f, true, false);
					}
					else
					{
						subject.think("apologize_for_disobedience", 1f, true, false);
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					break;
				case 11:
					subject.timeSinceDisobedienceOrReprimand = 0f;
					if (subject.getCommandStatus("do_not_cum") == -1)
					{
						if (subject.rebellious)
						{
							subject.think("rebellious_response_to_command", 1f, true, false);
						}
						else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.5f + InlineDialogue.randomSwing)
						{
							subject.think("refuse_command", 1f, true, false);
						}
						else
						{
							subject.think("apologize_for_disobedience", 1f, true, false);
						}
					}
					else if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.9f + InlineDialogue.randomSwing)
					{
						subject.think("confused_by_reprimand", 1f, true, false);
					}
					else
					{
						subject.think("apologize_for_disobedience", 1f, true, false);
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.05f;
					break;
				case 12:
				{
					subject.timeSinceDisobedienceOrReprimand = 0f;
					bool flag = false;
					foreach (string item in subject.commandStatus.Keys.ToList())
					{
						if (subject.commandStatus[item] == -1)
						{
							flag = true;
						}
					}
					if (flag)
					{
						if (subject.rebellious)
						{
							subject.think("rebellious_response_to_command", 1f, true, false);
						}
						else
						{
							subject.think("apologize_for_disobedience", 1f, true, false);
							subject.agreeToAllBrokenCommands();
						}
					}
					else if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.9f + InlineDialogue.randomSwing)
					{
						subject.think("confused_by_reprimand", 1f, true, false);
					}
					else
					{
						subject.think("apologize_for_disobedience", 1f, true, false);
						subject.agreeToAllBrokenCommands();
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					break;
				}
				case 13:
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("less_talking", -1);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.25f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("accept_command", 1f, true, false);
						subject.setCommandStatus("less_talking", 2);
						subject.talkativeness *= 0.2f;
					}
					else if (subject.getCommandStatus("less_talking") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("less_talking", 1);
						subject.talkativeness *= 0.8f;
					}
					else
					{
						subject.think("refuse_command", 1f, true, false);
						subject.setCommandStatus("less_talking", -1);
						subject.dominance += 0.03f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					break;
				case 14:
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("use_your_mouth", -1);
						subject.dominance += 0.03f;
						subject.timeSinceBrokeMouthHoldCommand = 0f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.45f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("accept_command", 1f, true, false);
						subject.setCommandStatus("use_your_mouth", 2);
						subject.holdMouthOpenTime = 25f;
					}
					else if (subject.getCommandStatus("use_your_mouth") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("use_your_mouth", 1);
					}
					else
					{
						subject.think("refuse_command", 1f, true, false);
						subject.setCommandStatus("use_your_mouth", -1);
						subject.dominance += 0.03f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					break;
				case 15:
					if (subject.higherWritheFactor > 0.3f)
					{
						if (subject.rebellious)
						{
							subject.think("rebellious_response_to_command", 1f, true, false);
							subject.setCommandStatus("stop_squirming", -1);
							subject.dominance += 0.03f;
						}
						else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.85f - staleness / 60f + InlineDialogue.randomSwing)
						{
							subject.think("accept_command", 1f, true, false);
							subject.setCommandStatus("stop_squirming", 1);
						}
						else
						{
							subject.think("im_trying", 1f, true, false);
							subject.setCommandStatus("stop_squirming", -1);
							subject.dominance += 0.03f;
						}
					}
					else if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("stop_squirming", -1);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.45f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("accept_command", 1f, true, false);
						subject.setCommandStatus("stop_squirming", 1);
					}
					else if (subject.getCommandStatus("stop_squirming") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("stop_squirming", 1);
					}
					else
					{
						subject.think("accept_command", 1f, true, false);
						subject.setCommandStatus("stop_squirming", 1);
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f;
					break;
				case 16:
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("beg", -1);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.65f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("beg", 1f, true, false);
						subject.setCommandStatus("beg", 2);
						subject.dominance -= 0.02f;
					}
					else if (subject.getCommandStatus("beg") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("beg", 1);
					}
					else
					{
						subject.think("refuse_command", 1f, true, false);
						subject.setCommandStatus("beg", -1);
						subject.dominance += 0.03f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.05f;
					break;
				case 17:
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("demean_self", -1);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.85f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("demean_myself", 1f, true, false);
						subject.setCommandStatus("demean_self", 2);
						subject.dominance -= 0.05f;
					}
					else if (subject.getCommandStatus("demean_self") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("demean_self", 1);
					}
					else
					{
						subject.think("refuse_command", 1f, true, false);
						subject.setCommandStatus("demean_self", -1);
						subject.dominance += 0.03f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.25f;
					break;
				case 18:
					subject.setCommandStatus("order_to_not_cum", 0);
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("order_to_cum", 1);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.15f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("im_trying", 1f, true, false);
						subject.setCommandStatus("order_to_cum", 1);
					}
					else if (subject.getCommandStatus("order_to_cum") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("order_to_cum", 1);
					}
					else if (subject.proximityToOrgasm > 0.6f)
					{
						subject.think("im_trying", 1f, true, false);
						subject.setCommandStatus("order_to_cum", 1);
					}
					else
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("order_to_cum", 1);
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.02f;
					break;
				case 19:
					subject.setCommandStatus("order_to_cum", 0);
					if (subject.rebellious)
					{
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.setCommandStatus("order_to_not_cum", 1);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.35f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.think("im_trying", 1f, true, false);
						subject.setCommandStatus("order_to_not_cum", 1);
					}
					else if (subject.getCommandStatus("order_to_not_cum") == 0)
					{
						subject.think("confused_by_command", 1f, true, false);
						subject.setCommandStatus("order_to_not_cum", 1);
					}
					else if (subject.proximityToOrgasm > 0.6f)
					{
						subject.think("im_trying", 1f, true, false);
						subject.setCommandStatus("order_to_not_cum", 1);
					}
					else
					{
						subject.think("accept_command", 1f, true, false);
						subject.setCommandStatus("order_to_not_cum", 1);
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.03f;
					break;
				case 20:
					if (subject.rebellious)
					{
						subject.setCommandStatus("use_dom_name", -1);
						subject.think("rebellious_response_to_command", 1f, true, false);
						subject.dominance += 0.03f;
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f < -0.25f - staleness / 60f + InlineDialogue.randomSwing)
					{
						subject.setCommandStatus("use_dom_name", 1);
						subject.think("accept_command", 1f, true, false);
					}
					else if (subject.getCommandStatus("less_talking") == 0)
					{
						subject.setCommandStatus("use_dom_name", 1);
						subject.think("confused_by_command", 1f, true, false);
					}
					else
					{
						subject.setCommandStatus("use_dom_name", -1);
						subject.think("refuse_command", 1f, true, false);
						subject.dominance += 0.03f;
					}
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.05f;
					break;
				case 21:
					subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.1f;
					if (subject.rebellious)
					{
						subject.think("rebellious_response", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f + staleness / 60f > 0.5f + InlineDialogue.randomSwing)
					{
						subject.think("what_did_you_just_say_about_me?_angry", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f + staleness / 60f > InlineDialogue.randomSwing)
					{
						subject.think("what_did_you_just_say_about_me?_confused", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f + staleness / 60f > -0.8f + InlineDialogue.randomSwing)
					{
						subject.think("what_did_you_just_say_about_me?_hurt", 1f, true, false);
					}
					else
					{
						subject.think("demean_myself", 1f, true, false);
						subject.dominance -= 0.05f;
					}
					if (subject.aggression > 0f - staleness / 60f)
					{
						subject.aggression += 0.1f / (1f + staleness / 20f);
					}
					else
					{
						subject.aggression -= 0.1f / (1f + staleness / 20f);
					}
					subject.dominance -= 0.1f / (1f + staleness / 20f);
					break;
				case 22:
					if (subject.rebellious)
					{
						subject.think("rebellious_response", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > 0.5f + InlineDialogue.randomSwing)
					{
						subject.think("confident_acceptance_of_empowerment", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > InlineDialogue.randomSwing)
					{
						subject.think("acceptance_of_empowerment", 1f, true, false);
					}
					else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.8f + InlineDialogue.randomSwing)
					{
						subject.think("reluctant_acceptance_of_empowerment", 1f, true, false);
					}
					else
					{
						subject.think("denial_of_empowerment", 1f, true, false);
					}
					subject.dominance += InlineDialogue.friendlinessVolatility * subject.friendlinessToPlayer / (1f + staleness / 20f) * 0.3f;
					subject.friendlinessToPlayer = 0f;
					break;
				case 23:
					InlineDialogue.reactToInsult(subject, 0.3f, staleness);
					break;
				case 24:
				{
					int num3 = 0;
					foreach (string item2 in subject.commandStatus.Keys.ToList())
					{
						if (subject.getCommandStatus(item2) == 2)
						{
							num3++;
						}
						if (subject.getCommandStatus(item2) == -1)
						{
							num3--;
						}
					}
					if (subject.rebellious)
					{
						subject.think("rebellious_response", 1f, true, false);
						subject.dominance += (InlineDialogue.randomSwing + 0.15f) * 0.2f / (1f + staleness / 20f);
					}
					else if (num3 > 0)
					{
						subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.05f / (1f + staleness / 20f);
						subject.dominance += (InlineDialogue.randomSwing - 0.2f) * 0.25f / (1f + staleness / 20f);
					}
					else
					{
						subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.01f / (1f + staleness / 20f);
						subject.dominance += (InlineDialogue.randomSwing + 0.05f) * 0.2f / (1f + staleness / 20f);
					}
					break;
				}
				case 25:
					InlineDialogue.reactToCompliment(subject, 0.3f, staleness);
					break;
				case 26:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["masculinity"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 27:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["masculinity"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 28:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["masculinity"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 29:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["masculinity"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 30:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["adiposity"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["adiposity"] = 1;
					break;
				case 31:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["adiposity"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["adiposity"] = 1;
					break;
				case 32:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["muscularity"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["muscularity"] = 1;
					break;
				case 33:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["muscularity"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["muscularity"] = 1;
					break;
				case 34:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["height"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["height"] = 1;
					break;
				case 35:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["height"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["height"] = 1;
					break;
				case 36:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["adiposity"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["adiposity"] = 1;
					break;
				case 37:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["penis_size"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["penis_size"] = 1;
					break;
				case 38:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["penis_size"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["penis_size"] = 1;
					break;
				case 39:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["ball_size"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["ball_size"] = 1;
					break;
				case 40:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["ball_size"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["ball_size"] = 1;
					break;
				case 41:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["vagina_tightness"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["vagina_tightness"] = 1;
					break;
				case 42:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["vagina_tightness"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["vagina_tightness"] = 1;
					break;
				case 43:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["ass_cuteness"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["ass_cuteness"] = 1;
					break;
				case 44:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["ass_cuteness"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["ass_cuteness"] = 1;
					break;
				case 45:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["breast_size"] != 1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["breast_size"] = 1;
					break;
				case 46:
					InlineDialogue.reactToCompliment(subject, (subject.confidences["breast_size"] != -1) ? 0.75f : 1.25f, staleness);
					subject.confidencePlayerKnowledge["breast_size"] = 1;
					break;
				case 47:
					InlineDialogue.reactToInsult(subject, (subject.confidences["masculinity"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 48:
					InlineDialogue.reactToInsult(subject, (subject.confidences["masculinity"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 49:
					InlineDialogue.reactToInsult(subject, (subject.confidences["masculinity"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 50:
					InlineDialogue.reactToInsult(subject, (subject.confidences["masculinity"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["masculinity"] = 1;
					break;
				case 51:
					InlineDialogue.reactToInsult(subject, (subject.confidences["adiposity"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["adiposity"] = 1;
					break;
				case 52:
					InlineDialogue.reactToInsult(subject, (subject.confidences["adiposity"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["adiposity"] = 1;
					break;
				case 53:
					InlineDialogue.reactToInsult(subject, (subject.confidences["muscularity"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["muscularity"] = 1;
					break;
				case 54:
					InlineDialogue.reactToInsult(subject, (subject.confidences["muscularity"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["muscularity"] = 1;
					break;
				case 55:
					InlineDialogue.reactToInsult(subject, (subject.confidences["height"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["height"] = 1;
					break;
				case 56:
					InlineDialogue.reactToInsult(subject, (subject.confidences["height"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["height"] = 1;
					break;
				case 57:
					InlineDialogue.reactToInsult(subject, (subject.confidences["adiposity"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["adiposity"] = 1;
					break;
				case 58:
					InlineDialogue.reactToInsult(subject, (subject.confidences["penis_size"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["penis_size"] = 1;
					break;
				case 59:
					InlineDialogue.reactToInsult(subject, (subject.confidences["penis_size"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["penis_size"] = 1;
					break;
				case 60:
					InlineDialogue.reactToInsult(subject, (subject.confidences["ball_size"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["ball_size"] = 1;
					break;
				case 61:
					InlineDialogue.reactToInsult(subject, (subject.confidences["ball_size"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["ball_size"] = 1;
					break;
				case 62:
					InlineDialogue.reactToInsult(subject, (subject.confidences["vagina_tightness"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["vagina_tightness"] = 1;
					break;
				case 63:
					InlineDialogue.reactToInsult(subject, (subject.confidences["vagina_tightness"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["vagina_tightness"] = 1;
					break;
				case 64:
					InlineDialogue.reactToInsult(subject, (subject.confidences["ass_cuteness"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["ass_cuteness"] = 1;
					break;
				case 65:
					InlineDialogue.reactToInsult(subject, (subject.confidences["ass_cuteness"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["ass_cuteness"] = 1;
					break;
				case 66:
					InlineDialogue.reactToInsult(subject, (subject.confidences["breast_size"] != 1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["breast_size"] = 1;
					break;
				case 67:
					InlineDialogue.reactToInsult(subject, (subject.confidences["breast_size"] != -1) ? 1.25f : 0.75f, staleness);
					subject.confidencePlayerKnowledge["breast_size"] = 1;
					break;
				case 68:
					if (subject.rebellious)
					{
						subject.think("rebellious_response_tame", 1f, true, false);
					}
					subject.arouseByThought(0.03f / (1f + staleness / 20f), 4f);
					break;
				}
			}
		}
		subject.aggression = Game.cap(subject.aggression, -1f, 1f);
		subject.dominance = Game.cap(subject.dominance, -1f, 1f);
		subject.friendlinessToPlayer = Game.cap(subject.friendlinessToPlayer, 0f, 2f);
	}

	public static void reactToInsult(RackCharacter subject, float agreementWithQuip, float staleness)
	{
		subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.15f / (1f + staleness / 20f) * agreementWithQuip;
		if (subject.rebellious)
		{
			subject.think("rebellious_response", 1f, true, false);
			subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.1f / (1f + staleness / 20f);
		}
		else if (subject.dominance - subject.friendlinessToPlayer * 1.1f + staleness / 60f > 0.5f + InlineDialogue.randomSwing)
		{
			subject.think("what_did_you_just_say_about_me?_angry", 1f, true, false);
		}
		else if (subject.dominance - subject.friendlinessToPlayer * 1.1f + staleness / 60f > InlineDialogue.randomSwing)
		{
			subject.think("what_did_you_just_say_about_me?_confused", 1f, true, false);
		}
		else if (subject.dominance - subject.friendlinessToPlayer * 1.1f + staleness / 60f > -0.8f + InlineDialogue.randomSwing)
		{
			subject.think("what_did_you_just_say_about_me?_hurt", 1f, true, false);
		}
		else
		{
			subject.think("accept_insult", 1f, true, false);
		}
		if (subject.aggression > 0f)
		{
			subject.aggression += 0.1f / (1f + staleness / 20f) * agreementWithQuip;
		}
		else
		{
			subject.aggression -= 0.1f / (1f + staleness / 20f) * agreementWithQuip;
		}
	}

	public static void reactToCompliment(RackCharacter subject, float agreementWithQuip, float staleness)
	{
		if (UserSettings.needTutorial("NPT_GREET_THE_SUBJECT"))
		{
			subject.friendlinessToPlayer = 0.45f;
		}
		else
		{
			subject.friendlinessToPlayer += InlineDialogue.friendlinessVolatility * 0.15f / (1f + staleness / 30f) * agreementWithQuip;
		}
		if (subject.rebellious && !UserSettings.needTutorial("NPT_GREET_THE_SUBJECT"))
		{
			subject.think("rebellious_response_tame", 1f, true, false);
			subject.friendlinessToPlayer -= InlineDialogue.friendlinessVolatility * 0.1f / (1f + staleness / 30f);
		}
		else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > 0.5f + InlineDialogue.randomSwing && !UserSettings.needTutorial("NPT_GREET_THE_SUBJECT"))
		{
			subject.think("accept_compliment_and_ask_for_more", 1f, true, false);
		}
		else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > InlineDialogue.randomSwing || UserSettings.needTutorial("NPT_GREET_THE_SUBJECT"))
		{
			subject.think("accept_compliment", 1f, true, false);
		}
		else if (subject.dominance - subject.friendlinessToPlayer * 1.1f - staleness / 60f > -0.8f + InlineDialogue.randomSwing)
		{
			subject.think("reluctantly_accept_compliment", 1f, true, false);
		}
		else
		{
			subject.think("reject_compliment", 1f, true, false);
		}
	}

	public static string selectQuipFromSubOption(string subOption, RackCharacter subject, RackCharacter top)
	{
		string text = "NO_QUIP_FOUND";
		string empty = string.Empty;
		if (subject == null)
		{
			subject = top;
		}
		if (subOption != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(33);
			dictionary.Add("inline_arouse.comment_on_interaction", 0);
			dictionary.Add("inline_arouse.comment_on_experience", 1);
			dictionary.Add("inline_arouse.comment_on_attraction", 2);
			dictionary.Add("inline_encourage.compliment_body", 3);
			dictionary.Add("inline_encourage.compliment_genitals", 4);
			dictionary.Add("inline_encourage.encourage_fetishes", 5);
			dictionary.Add("inline_encourage.encourage_obedience", 6);
			dictionary.Add("inline_encourage.empower", 7);
			dictionary.Add("inline_ask.ask_what_they_like", 8);
			dictionary.Add("inline_converse.greet", 9);
			dictionary.Add("inline_converse.be_right_back", 10);
			dictionary.Add("inline_converse.goodbye_friendly", 11);
			dictionary.Add("inline_converse.goodbye_unfriendly", 12);
			dictionary.Add("inline_ask.ask_about_secrets", 13);
			dictionary.Add("inline_ask.ask_what_they_dislike", 14);
			dictionary.Add("inline_encourage.relax", 15);
			dictionary.Add("inline_reprimand.speaking", 16);
			dictionary.Add("inline_reprimand.rudeness", 17);
			dictionary.Add("inline_reprimand.failure_to_use_title", 18);
			dictionary.Add("inline_reprimand.cumming", 19);
			dictionary.Add("inline_reprimand.disobedience", 20);
			dictionary.Add("inline_demean.insult_body", 21);
			dictionary.Add("inline_demean.insult_genitals", 22);
			dictionary.Add("inline_demean.insult_fetishes", 23);
			dictionary.Add("inline_demean.demean", 24);
			dictionary.Add("inline_order.less_talking", 25);
			dictionary.Add("inline_order.use_your_mouth", 26);
			dictionary.Add("inline_order.stop_squirming", 27);
			dictionary.Add("inline_order.beg", 28);
			dictionary.Add("inline_order.demean_self", 29);
			dictionary.Add("inline_order.order_to_cum", 30);
			dictionary.Add("inline_order.order_to_not_cum", 31);
			dictionary.Add("inline_order.use_dom_name", 32);
			int num = default(int);
			if (dictionary.TryGetValue(subOption, out num))
			{
				switch (num)
				{
				case 0:
					empty = InlineDialogue.getInteractionQuip(subject);
					text = ((!(empty == "none")) ? ("quip.comment_on_interaction_" + empty) : ((top.getQuipStaleness("inline_arouse.comment_on_interaction") != -1f) ? "quip.generic_comment_REPEAT" : "quip.generic_comment"));
					break;
				case 1:
					empty = InlineDialogue.getExperienceQuip(subject);
					text = ((!(empty == "none")) ? ((!(empty == "comment_on_experience_submission")) ? ("quip.comment_on_experience_" + empty) : ((!(subject.dominance > 0f)) ? ("quip.comment_on_experience_" + empty + "_sub") : ("quip.comment_on_experience_" + empty + "_dom"))) : ((top.getQuipStaleness("inline_arouse.comment_on_interaction") != -1f) ? "quip.generic_comment_REPEAT" : "quip.generic_comment"));
					break;
				case 2:
					empty = InlineDialogue.getAttractionQuip(subject);
					text = ((!(empty == "none")) ? ((!(empty == "exaggerated_masculinity")) ? ((!(empty == "countermasculinity")) ? ("quip.comment_on_attraction_" + empty) : ((!top.data.identifiesMale) ? ("quip.comment_on_attraction_" + empty + "_female") : ("quip.comment_on_attraction_" + empty + "_male"))) : ((!top.data.identifiesMale) ? ("quip.comment_on_attraction_" + empty + "_female") : ("quip.comment_on_attraction_" + empty + "_male"))) : ((top.getQuipStaleness("inline_arouse.comment_on_interaction") != -1f) ? "quip.generic_comment_REPEAT" : "quip.generic_comment"));
					break;
				case 3:
					if (!subject.data.identifiesMale)
					{
						InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 4;
					}
					else
					{
						InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 5;
					}
					switch (InlineDialogue.rand)
					{
					case 0:
						if (subject.confidencePlayerKnowledge["masculinity"] == 1)
						{
							if (subject.confidences["masculinity"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.totalFemininity < 0.3f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_femininity" : "quip.compliment_masculinity");
						text = ((!subject.data.identifiesMale) ? (text + "_female") : (text + "_male"));
						break;
					case 1:
						if (subject.confidencePlayerKnowledge["adiposity"] == 1)
						{
							if (subject.confidences["adiposity"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.adiposity_act > 0.4f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_thin_body" : "quip.compliment_soft_body");
						break;
					case 2:
						if (subject.confidencePlayerKnowledge["muscularity"] == 1)
						{
							if (subject.confidences["muscularity"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.muscle_act > 0.1f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_small_muscles" : "quip.compliment_big_muscles");
						break;
					case 3:
						if (subject.confidencePlayerKnowledge["height"] == 1)
						{
							if (subject.confidences["height"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.height_act > 0.98f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_short" : "quip.compliment_tall");
						break;
					case 4:
						if (subject.confidencePlayerKnowledge["breast_size"] == 1)
						{
							if (subject.confidences["breast_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.totalFemininity < 0.1f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_tight_pecs" : "quip.compliment_soft_body");
						break;
					}
					break;
				case 4:
					InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 5;
					while (true)
					{
						if (InlineDialogue.rand == 2 && !subject.showVagina)
						{
							goto IL_07a1;
						}
						if (InlineDialogue.rand == 4 && subject.data.identifiesMale)
						{
							goto IL_07a1;
						}
						if (InlineDialogue.rand == 0 && !subject.showPenis)
						{
							goto IL_07a1;
						}
						if (InlineDialogue.rand != 1)
						{
							break;
						}
						if (subject.showBalls && subject.data.genitalType <= 0 && subject.data.ballsType <= 0)
						{
							break;
						}
						goto IL_07a1;
						IL_07a1:
						InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 5;
					}
					switch (InlineDialogue.rand)
					{
					case 0:
						if (subject.confidencePlayerKnowledge["penis_size"] == 1)
						{
							if (subject.confidences["penis_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.totalPenisSize > 1f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_small_penis" : "quip.compliment_large_penis");
						break;
					case 1:
						if (subject.confidencePlayerKnowledge["ball_size"] == 1)
						{
							if (subject.confidences["ball_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.totalBallSize > 1.2f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_small_balls" : "quip.compliment_large_balls");
						break;
					case 2:
						if (subject.confidencePlayerKnowledge["vagina_tightness"] == 1)
						{
							if (subject.confidences["vagina_tightness"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.vaginaShape_act < 0.5f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_soft_vagina" : "quip.compliment_tight_vagina");
						break;
					case 3:
						if (subject.confidencePlayerKnowledge["ass_cuteness"] == 1)
						{
							if (subject.confidences["ass_cuteness"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.totalFemininity > 0.3f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_masculine_ass" : "quip.compliment_cute_ass");
						break;
					case 4:
						if (subject.confidencePlayerKnowledge["breast_size"] == 1)
						{
							if (subject.confidences["breast_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
						}
						else if (subject.breastSize_act > 0.65f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.compliment_small_breasts" : "quip.compliment_large_breasts");
						break;
					}
					break;
				case 5:
					text = "quip.encourage_fetishes";
					break;
				case 6:
					text = "quip.encourage_obedience";
					break;
				case 7:
					text = "quip.empower";
					break;
				case 8:
					text = ((top.getQuipStaleness("inline_ask.ask_what_they_like") != -1f) ? "quip.what_do_you_like_REPEAT" : "quip.what_do_you_like");
					break;
				case 9:
					text = ((top.getQuipStaleness("inline_converse.greet") != -1f) ? "quip.greet_REPEAT" : "quip.greet");
					break;
				case 10:
					text = "quip.be_right_back";
					break;
				case 11:
					text = "quip.goodbye_friendly";
					break;
				case 12:
					text = "quip.goodbye_unfriendly";
					break;
				case 13:
					text = ((top.getQuipStaleness("inline_ask.ask_about_secrets") != -1f) ? "quip.ask_about_secrets_REPEAT" : "quip.ask_about_secrets");
					break;
				case 14:
					text = ((top.getQuipStaleness("inline_ask.ask_what_they_dislike") != -1f) ? "quip.ask_what_they_dislike_REPEAT" : "quip.ask_what_they_dislike");
					break;
				case 15:
					text = ((!(subject.aggression > 0.5f)) ? ((!(subject.aggression < -0.5f)) ? "quip.be_comfortable" : "quip.relax") : "quip.calm_down");
					subject.talkativeness += (1f - subject.talkativeness) * 0.3f;
					break;
				case 16:
					text = ((top.getQuipStaleness("inline_reprimand.speaking") != -1f) ? "quip.reprimand_speech_REPEAT" : "quip.reprimand_speech");
					break;
				case 17:
					text = ((top.getQuipStaleness("inline_reprimand.rudeness") != -1f) ? "quip.reprimand_rudeness_REPEAT" : "quip.reprimand_rudeness");
					break;
				case 18:
					text = ((top.getQuipStaleness("inline_reprimand.failure_to_use_title") != -1f) ? "quip.reprimand_failure_to_use_title_REPEAT" : "quip.reprimand_failure_to_use_title");
					break;
				case 19:
					text = "quip.reprimand_cumming";
					break;
				case 20:
					text = "quip.reprimand_disobedience";
					break;
				case 21:
					if (!subject.data.identifiesMale)
					{
						InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 4;
					}
					else
					{
						InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 5;
					}
					switch (InlineDialogue.rand)
					{
					case 0:
						if (subject.confidencePlayerKnowledge["masculinity"] == 1)
						{
							if (subject.confidences["masculinity"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.totalFemininity < -0.3f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_femininity" : "quip.insult_masculinity");
						text = ((!subject.data.identifiesMale) ? (text + "_female") : (text + "_male"));
						break;
					case 1:
						if (subject.confidencePlayerKnowledge["adiposity"] == 1)
						{
							if (subject.confidences["adiposity"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.adiposity_act > 0.25f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_thin_body" : "quip.insult_soft_body");
						break;
					case 2:
						if (subject.confidencePlayerKnowledge["muscularity"] == 1)
						{
							if (subject.confidences["muscularity"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.muscle_act > 0.5f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_small_muscles" : "quip.insult_big_muscles");
						break;
					case 3:
						if (subject.confidencePlayerKnowledge["height"] == 1)
						{
							if (subject.confidences["height"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.height_act > 1.1f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_short" : "quip.insult_tall");
						break;
					case 4:
						if (subject.confidencePlayerKnowledge["breast_size"] == 1)
						{
							if (subject.confidences["breast_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						if (InlineDialogue.commentDirection == 1)
						{
							text = "quip.insult_soft_manboobs";
						}
						break;
					}
					break;
				case 22:
					InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 5;
					while (true)
					{
						if (InlineDialogue.rand == 2 && !subject.showVagina)
						{
							goto IL_103d;
						}
						if (InlineDialogue.rand == 4 && subject.data.identifiesMale)
						{
							goto IL_103d;
						}
						if (InlineDialogue.rand == 0 && !subject.showPenis)
						{
							goto IL_103d;
						}
						if (InlineDialogue.rand != 1)
						{
							break;
						}
						if (subject.showBalls && subject.data.genitalType != 2 && subject.data.genitalType != 2)
						{
							break;
						}
						goto IL_103d;
						IL_103d:
						InlineDialogue.rand = Mathf.FloorToInt(Random.value * 99f) % 5;
					}
					switch (InlineDialogue.rand)
					{
					case 0:
						if (subject.confidencePlayerKnowledge["penis_size"] == 1)
						{
							if (subject.confidences["penis_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.totalPenisSize > 1.7f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_small_penis" : "quip.insult_large_penis");
						break;
					case 1:
						if (subject.confidencePlayerKnowledge["ball_size"] == 1)
						{
							if (subject.confidences["ball_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.totalBallSize > 1.6f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_small_balls" : "quip.insult_large_balls");
						break;
					case 2:
						if (subject.confidencePlayerKnowledge["vagina_tightness"] == 1)
						{
							if (subject.confidences["vagina_tightness"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.vaginaShape_act < 0.5f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_soft_vagina" : "quip.insult_tight_vagina");
						break;
					case 3:
						if (subject.confidencePlayerKnowledge["ass_cuteness"] == 1)
						{
							if (subject.confidences["ass_cuteness"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.totalFemininity > 0f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_masculine_ass" : "quip.insult_cute_ass");
						break;
					case 4:
						if (subject.confidencePlayerKnowledge["breast_size"] == 1)
						{
							if (subject.confidences["breast_size"] > 0)
							{
								InlineDialogue.commentDirection = 1;
							}
							else
							{
								InlineDialogue.commentDirection = -1;
							}
							InlineDialogue.commentDirection *= -1;
						}
						else if (subject.breastSize_act > 1f)
						{
							InlineDialogue.commentDirection = 1;
						}
						else
						{
							InlineDialogue.commentDirection = -1;
						}
						text = ((InlineDialogue.commentDirection != 1) ? "quip.insult_small_breasts" : "quip.insult_large_breasts");
						break;
					}
					break;
				case 23:
					text = "quip.insult_fetishes";
					break;
				case 24:
					text = "quip.demean";
					break;
				case 25:
					text = ((subject.getCommandStatus("less_talking") != 0) ? "quip.less_talking_REPEAT" : "quip.less_talking");
					break;
				case 26:
					text = ((subject.getCommandStatus("use_your_mouth") != 0) ? "quip.use_your_mouth_REPEAT" : "quip.use_your_mouth");
					break;
				case 27:
					text = ((subject.getCommandStatus("stop_squirming") != 0) ? "quip.stop_squirming_REPEAT" : "quip.stop_squirming");
					break;
				case 28:
					text = ((subject.getCommandStatus("beg") != 0) ? "quip.beg_REPEAT" : "quip.beg");
					break;
				case 29:
					text = ((subject.getCommandStatus("demean_self") != 0) ? "quip.demean_self_REPEAT" : "quip.demean_self");
					break;
				case 30:
					text = ((subject.getCommandStatus("order_to_cum") != 0) ? "quip.order_to_cum_REPEAT" : "quip.order_to_cum");
					break;
				case 31:
					text = ((subject.getCommandStatus("order_to_not_cum") != 0) ? "quip.order_to_not_cum_REPEAT" : "quip.order_to_not_cum");
					break;
				case 32:
					text = ((subject.getCommandStatus("use_dom_name") != 0) ? "quip.use_dom_name_REPEAT" : "quip.use_dom_name");
					break;
				}
			}
		}
		if (text == "NO_QUIP_FOUND")
		{
			Debug.Log("No quip found for: " + subOption);
		}
		return text;
	}

	public static string getInteractionQuip(RackCharacter subject)
	{
		InlineDialogue.options = new List<string>();
		string result = "none";
		foreach (string item in subject.interactionsEnjoymentCauses.Keys.ToList())
		{
			if (subject.interactionsEnjoymentCauses[item] != 0f && (subject.interactionsEnjoymentCauses[item] > 0f || subject.preferencePlayerKnowledge[item] != 1))
			{
				InlineDialogue.options.Add(item);
			}
		}
		if (InlineDialogue.options.Count > 0)
		{
			result = (from item in InlineDialogue.options
			orderby Random.value
			select item).ToList()[0];
		}
		return result;
	}

	public static string getExperienceQuip(RackCharacter subject)
	{
		InlineDialogue.options = new List<string>();
		string result = "none";
		foreach (string item in subject.experienceEnjoymentCauses.Keys.ToList())
		{
			if (subject.experienceEnjoymentCauses[item] != 0f && (subject.experienceEnjoymentCauses[item] > 0f || subject.preferencePlayerKnowledge[item] != 1))
			{
				InlineDialogue.options.Add(item);
			}
		}
		if (InlineDialogue.options.Count > 0)
		{
			result = (from item in InlineDialogue.options
			orderby Random.value
			select item).ToList()[0];
		}
		return result;
	}

	public static string getAttractionQuip(RackCharacter subject)
	{
		InlineDialogue.options = new List<string>();
		string result = "none";
		foreach (string item in subject.attractionEnjoymentCauses.Keys.ToList())
		{
			if (subject.attractionEnjoymentCauses[item] != 0f && (subject.attractionEnjoymentCauses[item] > 0f || subject.preferencePlayerKnowledge[item] != 1))
			{
				InlineDialogue.options.Add(item);
			}
		}
		if (InlineDialogue.options.Count > 0)
		{
			result = (from item in InlineDialogue.options
			orderby Random.value
			select item).ToList()[0];
		}
		return result;
	}
}
