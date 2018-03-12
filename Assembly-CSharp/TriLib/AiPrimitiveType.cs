using System;

namespace TriLib
{
	[Flags]
	public enum AiPrimitiveType
	{
		Point = 1,
		Line = 2,
		Triangle = 4,
		Polygon = 8
	}
}
