using System;

[Serializable]
public class PoolInstance
{
	public int id;

	public string title;

	public int[] limits;

	public PoolInstance(string Title, PoolInstance[] CurrentInstances)
	{
		this.id = this.UniqueID(CurrentInstances);
		this.title = Title;
		this.limits = new int[15];
		for (int i = 0; i < this.limits.Length; i++)
		{
			this.limits[i] = 250 + i * 50;
		}
	}

	private int UniqueID(PoolInstance[] CurrentInstances)
	{
		int num = 0;
		bool flag = false;
		if (CurrentInstances != null)
		{
			while (!flag)
			{
				num++;
				flag = true;
				for (int i = 0; i < CurrentInstances.Length; i++)
				{
					if (CurrentInstances[i] != null && num == CurrentInstances[i].id)
					{
						flag = false;
					}
				}
			}
		}
		return num;
	}
}
