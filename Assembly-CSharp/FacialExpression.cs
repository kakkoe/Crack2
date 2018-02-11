using System.Collections.Generic;

public class FacialExpression
{
	public static int get(string expressionName)
	{
		if (expressionName != null)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>(26);
			dictionary.Add("angry", 0);
			dictionary.Add("concerned", 1);
			dictionary.Add("confused", 2);
			dictionary.Add("confusedAmused", 3);
			dictionary.Add("confusedSmug", 4);
			dictionary.Add("grumpy", 5);
			dictionary.Add("happy", 6);
			dictionary.Add("hurt", 7);
			dictionary.Add("seductive", 8);
			dictionary.Add("smile", 9);
			dictionary.Add("unamused", 10);
			dictionary.Add("weaksmile", 11);
			dictionary.Add("coy", 12);
			dictionary.Add("impressed", 13);
			dictionary.Add("serious", 14);
			dictionary.Add("surprised", 15);
			dictionary.Add("wink", 16);
			dictionary.Add("worried", 17);
			dictionary.Add("pleasure", 18);
			dictionary.Add("pleasureConflicted", 19);
			dictionary.Add("pleasureEnraptured", 20);
			dictionary.Add("pleasureIntense", 21);
			dictionary.Add("pleasureSerious", 22);
			dictionary.Add("dominant", 23);
			dictionary.Add("grit", 24);
			dictionary.Add("pleading", 25);
			int num = default(int);
			if (dictionary.TryGetValue(expressionName, out num))
			{
				switch (num)
				{
				case 0:
					return 0;
				case 1:
					return 1;
				case 2:
					return 2;
				case 3:
					return 3;
				case 4:
					return 4;
				case 5:
					return 5;
				case 6:
					return 6;
				case 7:
					return 7;
				case 8:
					return 8;
				case 9:
					return 9;
				case 10:
					return 10;
				case 11:
					return 11;
				case 12:
					return 12;
				case 13:
					return 13;
				case 14:
					return 14;
				case 15:
					return 15;
				case 16:
					return 16;
				case 17:
					return 17;
				case 18:
					return 18;
				case 19:
					return 19;
				case 20:
					return 20;
				case 21:
					return 21;
				case 22:
					return 22;
				case 23:
					return 23;
				case 24:
					return 24;
				case 25:
					return 25;
				}
			}
		}
		return -1;
	}
}
