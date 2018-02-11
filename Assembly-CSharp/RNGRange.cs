using System.Xml.Serialization;
using UnityEngine;

public class RNGRange
{
	[XmlElement("min")]
	public float min;

	[XmlElement("max")]
	public float max;

	[XmlElement("rarity")]
	public float rarity;

	[XmlElement("balance")]
	public int balance;

	public RNGRange()
	{
	}

	public RNGRange(float min, float max, float rarity, int balance)
	{
		this.min = min;
		this.max = max;
		this.rarity = rarity;
		this.balance = balance;
	}

	public void getValue(ref float stat, float inheritOffset)
	{
		if (!UserSettings.data.mod_inheritRng)
		{
			if (this.balance == 0)
			{
				stat = this.min + Mathf.Pow(Random.value, this.rarity) * (this.max - this.min);
			}
			else if (this.balance == 2)
			{
				stat = this.min + (1f - Mathf.Pow(Random.value, this.rarity)) * (this.max - this.min);
			}
			else if (this.balance == 3)
			{
				stat = stat * Mathf.Pow(Random.value, this.rarity) * (this.max - this.min);
			}
			else if (this.balance == 4)
			{
				stat *= (this.max - this.min) * 0.5f + Mathf.Pow(Random.value, this.rarity) * (this.max - this.min) * 0.5f * (float)RandomCharacterGenerator.randDir();
			}
			else if (this.balance == 5)
			{
				stat = stat * (1f - Mathf.Pow(Random.value, this.rarity)) * (this.max - this.min);
			}
			else if (this.balance != -1)
			{
				stat = this.min + (this.max - this.min) * 0.5f + Mathf.Pow(Random.value, this.rarity) * (this.max - this.min) * 0.5f * (float)RandomCharacterGenerator.randDir();
			}
		}
		else if (this.balance == 0)
		{
			stat += inheritOffset + this.min + Mathf.Pow(Random.value, this.rarity) * (this.max - this.min);
		}
		else if (this.balance == 2)
		{
			stat += inheritOffset + this.min + (1f - Mathf.Pow(Random.value, this.rarity)) * (this.max - this.min);
		}
		else if (this.balance == 3)
		{
			stat = stat * Mathf.Pow(Random.value, this.rarity) * (this.max - this.min);
		}
		else if (this.balance == 4)
		{
			stat *= (this.max - this.min) * 0.5f + Mathf.Pow(Random.value, this.rarity) * (this.max - this.min) * 0.5f * (float)RandomCharacterGenerator.randDir();
		}
		else if (this.balance == 5)
		{
			stat = stat * (1f - Mathf.Pow(Random.value, this.rarity)) * (this.max - this.min);
		}
		else if (this.balance != -1)
		{
			stat += inheritOffset + this.min + (this.max - this.min) * 0.5f + Mathf.Pow(Random.value, this.rarity) * (this.max - this.min) * 0.5f * (float)RandomCharacterGenerator.randDir();
		}
	}
}
