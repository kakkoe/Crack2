using UnityEngine;

namespace TriLib
{
	public static class MatrixExtensions
	{
		public static Quaternion ExtractRotation(this Matrix4x4 matrix)
		{
			return Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
		}

		public static Vector3 ExtractPosition(this Matrix4x4 matrix)
		{
			return matrix.GetColumn(3);
		}

		public static Vector3 ExtractScale(this Matrix4x4 matrix)
		{
			return new Vector3(matrix.GetColumn(0).magnitude, matrix.GetColumn(1).magnitude, matrix.GetColumn(2).magnitude);
		}
	}
}
