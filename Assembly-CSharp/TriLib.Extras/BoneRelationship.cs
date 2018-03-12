namespace TriLib.Extras
{
	public class BoneRelationship
	{
		public string HumanBone;

		public string BoneName;

		public bool Optional;

		public BoneRelationship(string humanBone, string boneName, bool optional)
		{
			this.HumanBone = humanBone;
			this.BoneName = boneName;
			this.Optional = optional;
		}
	}
}
