using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Objectives
{
	public static Game game;

	public static int life = 0;

	public static List<Objective> objectives;

	public static Thread objectiveTrackingThread;

	public static Objective curObjective;

	public static float deltaTime = 0f;

	public static List<RackCharacter> charactersToPoll = new List<RackCharacter>();

	[CompilerGenerated]
	private static ThreadStart _003C_003Ef__mg_0024cache0;

	public static void reset()
	{
		Objectives.objectives = new List<Objective>();
	}

	public static bool haveObjective(string type)
	{
		for (int i = 0; i < Objectives.objectives.Count; i++)
		{
			if (Objectives.objectives[i].type == type)
			{
				return true;
			}
		}
		return false;
	}

	public static void completeObjective(string type)
	{
		int num = 0;
		while (true)
		{
			if (num < Objectives.objectives.Count)
			{
				if (!(Objectives.objectives[num].type == type))
				{
					num++;
					continue;
				}
				break;
			}
			return;
		}
		Objectives.objectives[num].completion = 1f;
		Objectives.objectives[num].completed = true;
	}

	public static void uncompleteObjective(string type)
	{
		int num = 0;
		while (true)
		{
			if (num < Objectives.objectives.Count)
			{
				if (!(Objectives.objectives[num].type == type))
				{
					num++;
					continue;
				}
				break;
			}
			return;
		}
		Objectives.objectives[num].completion = 0f;
		Objectives.objectives[num].completed = false;
	}

	public static void removeObjective(string type)
	{
		int num = 0;
		while (true)
		{
			if (num < Objectives.objectives.Count)
			{
				if (!(Objectives.objectives[num].type == type))
				{
					num++;
					continue;
				}
				break;
			}
			return;
		}
		Objectives.objectives[num].dead = true;
	}

	public static void wipeCompletedObjectives()
	{
		Debug.Log("Wiping completed objectives");
		for (int i = 0; i < Objectives.objectives.Count; i++)
		{
			if (Objectives.objectives[i].completed && Objectives.objectives[i].sourceCharacter == null)
			{
				Objectives.objectives[i].dead = true;
			}
		}
	}

	public static void addObjective(int source, string type, string description, float targetQuantity = 0f, RackCharacter sourceCharacter = null, bool bad = false, bool secret = false, object markerPosition = null)
	{
		Objective objective = new Objective();
		objective.source = source;
		objective.type = type;
		objective.description = description;
		objective.targetQuantity = targetQuantity;
		objective.sourceCharacter = sourceCharacter;
		objective.bad = bad;
		objective.secret = secret;
		Objectives.objectives.Add(objective);
		if (markerPosition == null)
		{
			objective.hasMarker = false;
		}
		else
		{
			objective.hasMarker = true;
			objective.markerPosition = (Vector3)markerPosition;
		}
		if (source == 0 && sourceCharacter != null)
		{
			sourceCharacter.objectives.Add(objective);
		}
	}

	public static void startTracking()
	{
		Objectives.objectiveTrackingThread = new Thread(Objectives.processObjectives);
		Objectives.objectiveTrackingThread.Start();
		Objectives.game = Game.gameInstance;
		Objectives.life++;
	}

	public static void kill()
	{
		Objectives.life++;
	}

	public static void processObjectives()
	{
		int num = Objectives.life;
		while (num == Objectives.life)
		{
			if (Objectives.deltaTime != 0f)
			{
				Objectives.processCharacterTracking();
				for (int i = 0; i < Objectives.objectives.Count; i++)
				{
					Objectives.curObjective = Objectives.objectives[i];
					if (Objectives.curObjective.dead)
					{
						Objectives.objectives.RemoveAt(i);
						i--;
					}
					else
					{
						Objectives.charactersToPoll.Clear();
						if (Objectives.curObjective.source == 0)
						{
							Objectives.charactersToPoll.Add(Objectives.curObjective.sourceCharacter);
						}
						else
						{
							for (int j = 0; j < Objectives.game.characters.Count; j++)
							{
								if ((Object)Objectives.game.characters[j].apparatus != (Object)null)
								{
									Objectives.charactersToPoll.Add(Objectives.game.characters[j]);
								}
							}
						}
						switch (Objectives.curObjective.type)
						{
						case "pain":
							for (int n = 0; n < Objectives.charactersToPoll.Count; n++)
							{
								if (Objectives.charactersToPoll[n].lastKnownInPain)
								{
									Objectives.curObjective.currentQuantity += Objectives.deltaTime;
								}
							}
							Objectives.curObjective.completion = Objectives.curObjective.currentQuantity / Objectives.curObjective.targetQuantity;
							Objectives.curObjective.completed = (Objectives.curObjective.completion >= 1f);
							break;
						case "attention":
							for (int m = 0; m < Objectives.charactersToPoll.Count; m++)
							{
								if (Objectives.charactersToPoll[m].lastKnownNumberOfStimulatingInteractions > 0)
								{
									Objectives.curObjective.currentQuantity += Objectives.deltaTime;
								}
							}
							Objectives.curObjective.completion = Objectives.curObjective.currentQuantity / Objectives.curObjective.targetQuantity;
							Objectives.curObjective.completed = (Objectives.curObjective.completion >= 1f);
							break;
						case "edging":
							for (int l = 0; l < Objectives.charactersToPoll.Count; l++)
							{
								if (Objectives.charactersToPoll[l].proximityToOrgasm > 0.7f && Objectives.charactersToPoll[l].orgasming <= 0f)
								{
									Objectives.curObjective.currentQuantity += Objectives.deltaTime * Game.cap((Objectives.charactersToPoll[l].proximityToOrgasm - 0.7f) * 10f, 0f, 1f);
								}
							}
							Objectives.curObjective.completion = Objectives.curObjective.currentQuantity / Objectives.curObjective.targetQuantity;
							Objectives.curObjective.completed = (Objectives.curObjective.completion >= 1f);
							break;
						case "orgasm":
							Objectives.curObjective.completion = 0f;
							for (int num2 = 0; num2 < Objectives.charactersToPoll.Count; num2++)
							{
								if (Objectives.charactersToPoll[num2].justOrgasmed_tracker == 1)
								{
									Objectives.curObjective.currentQuantity += 1f;
								}
								else if (Objectives.charactersToPoll[num2].proximityToOrgasm > Objectives.curObjective.completion && Objectives.charactersToPoll[num2].orgasming <= 0f)
								{
									Objectives.curObjective.completion = Objectives.charactersToPoll[num2].proximityToOrgasm;
								}
							}
							Objectives.curObjective.completion *= Objectives.curObjective.completion;
							Objectives.curObjective.completion /= Objectives.curObjective.targetQuantity;
							Objectives.curObjective.completion += Objectives.curObjective.currentQuantity / Objectives.curObjective.targetQuantity;
							if (Objectives.curObjective.currentQuantity >= Objectives.curObjective.targetQuantity)
							{
								Objectives.curObjective.completed = true;
							}
							break;
						case "sextoy_orgasm":
							for (int k = 0; k < Objectives.charactersToPoll.Count; k++)
							{
								if (Objectives.charactersToPoll[k].numberOfInteractionsFromSexToys > 0)
								{
									if (Objectives.charactersToPoll[k].justOrgasmed_tracker == 1)
									{
										Objectives.curObjective.currentQuantity += 1f;
									}
									else if (Objectives.charactersToPoll[k].proximityToOrgasm > Objectives.curObjective.completion && Objectives.charactersToPoll[k].orgasming <= 0f)
									{
										Objectives.curObjective.completion = Objectives.charactersToPoll[k].proximityToOrgasm;
									}
								}
							}
							Objectives.curObjective.completion *= Objectives.curObjective.completion;
							Objectives.curObjective.completion /= Objectives.curObjective.targetQuantity;
							Objectives.curObjective.completion += Objectives.curObjective.currentQuantity / Objectives.curObjective.targetQuantity;
							if (Objectives.curObjective.currentQuantity >= Objectives.curObjective.targetQuantity)
							{
								Objectives.curObjective.completed = true;
							}
							break;
						}
						Objectives.curObjective.completion = Game.cap(Objectives.curObjective.completion, 0f, 1f);
						if (Objectives.curObjective.completed)
						{
							Objectives.curObjective.completion = 1f;
							if (!Objectives.curObjective.wasCompleted)
							{
								Objectives.curObjective.wasCompleted = true;
								if (Objectives.curObjective.bad)
								{
									Objectives.game.playSoundFromThread("objective_fail", 0.1f, 1f);
								}
								else
								{
									Objectives.game.playSoundFromThread("objective_complete", 0.1f, 1f);
								}
							}
						}
					}
				}
				Objectives.deltaTime = 0f;
			}
		}
	}

	public static void processCharacterTracking()
	{
		for (int i = 0; i < Objectives.game.characters.Count; i++)
		{
			if (Objectives.game.characters[i].orgasming > 0f)
			{
				if (Objectives.game.characters[i].justOrgasmed_tracker == 1)
				{
					Objectives.game.characters[i].justOrgasmed_tracker = -1;
				}
				if (Objectives.game.characters[i].justOrgasmed_tracker == 0)
				{
					Objectives.game.characters[i].justOrgasmed_tracker = 1;
				}
			}
			else if (Objectives.game.characters[i].justOrgasmed_tracker == -1)
			{
				Objectives.game.characters[i].justOrgasmed_tracker = 0;
			}
		}
	}
}
