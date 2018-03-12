using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriLib.Extras
{
	public class AvatarLoader : MonoBehaviour
	{
		public GameObject CurrentAvatar;

		public RuntimeAnimatorController RuntimeAnimatorController;

		public float ArmStretch = 0.05f;

		public float FeetSpacing;

		public bool HasTranslationDof;

		public float LegStretch = 0.5f;

		public float LowerArmTwist = 0.5f;

		public float LowerLegTwist = 0.5f;

		public float UpperArmTwist = 0.5f;

		public float UpperLegTwist = 0.5f;

		public float Scale = 0.01f;

		public float HeightOffset = 0.01f;

		public BoneRelationshipList CustomBoneNames;

		private static readonly BoneRelationshipList BipedBoneNames = new BoneRelationshipList
		{
			{
				"Head",
				"Head",
				false
			},
			{
				"Neck",
				"Neck",
				true
			},
			{
				"Chest",
				"Spine3",
				true
			},
			{
				"UpperChest",
				"Spine1",
				true
			},
			{
				"Spine",
				"Spine",
				false
			},
			{
				"Hips",
				"Bip01",
				false
			},
			{
				"LeftShoulder",
				"L Clavicle",
				true
			},
			{
				"LeftUpperArm",
				"L UpperArm",
				false
			},
			{
				"LeftLowerArm",
				"L Forearm",
				false
			},
			{
				"LeftHand",
				"L Hand",
				false
			},
			{
				"RightShoulder",
				"R Clavicle",
				true
			},
			{
				"RightUpperArm",
				"R UpperArm",
				false
			},
			{
				"RightLowerArm",
				"R Forearm",
				false
			},
			{
				"RightHand",
				"R Hand",
				false
			},
			{
				"LeftUpperLeg",
				"L Thigh",
				false
			},
			{
				"LeftLowerLeg",
				"L Calf",
				false
			},
			{
				"LeftFoot",
				"L Foot",
				false
			},
			{
				"LeftToes",
				"L Toe0",
				true
			},
			{
				"RightUpperLeg",
				"R Thigh",
				false
			},
			{
				"RightLowerLeg",
				"R Calf",
				false
			},
			{
				"RightFoot",
				"R Foot",
				false
			},
			{
				"RightToes",
				"R Toe0",
				true
			},
			{
				"Left Thumb Proximal",
				"L Finger0",
				true
			},
			{
				"Left Thumb Intermediate",
				"L Finger01",
				true
			},
			{
				"Left Thumb Distal",
				"L Finger02",
				true
			},
			{
				"Left Index Proximal",
				"L Finger1",
				true
			},
			{
				"Left Index Intermediate",
				"L Finger11",
				true
			},
			{
				"Left Index Distal",
				"L Finger12",
				true
			},
			{
				"Left Middle Proximal",
				"L Finger2",
				true
			},
			{
				"Left Middle Intermediate",
				"L Finger21",
				true
			},
			{
				"Left Middle Distal",
				"L Finger22",
				true
			},
			{
				"Left Ring Proximal",
				"L Finger3",
				true
			},
			{
				"Left Ring Intermediate",
				"L Finger31",
				true
			},
			{
				"Left Ring Distal",
				"L Finger32",
				true
			},
			{
				"Left Little Proximal",
				"L Finger4",
				true
			},
			{
				"Left Little Intermediate",
				"L Finger41",
				true
			},
			{
				"Left Little Distal",
				"L Finger42",
				true
			},
			{
				"Right Thumb Proximal",
				"R Finger0",
				true
			},
			{
				"Right Thumb Intermediate",
				"R Finger01",
				true
			},
			{
				"Right Thumb Distal",
				"R Finger02",
				true
			},
			{
				"Right Index Proximal",
				"R Finger1",
				true
			},
			{
				"Right Index Intermediate",
				"R Finger11",
				true
			},
			{
				"Right Index Distal",
				"R Finger12",
				true
			},
			{
				"Right Middle Proximal",
				"R Finger2",
				true
			},
			{
				"Right Middle Intermediate",
				"R Finger21",
				true
			},
			{
				"Right Middle Distal",
				"R Finger22",
				true
			},
			{
				"Right Ring Proximal",
				"R Finger3",
				true
			},
			{
				"Right Ring Intermediate",
				"R Finger31",
				true
			},
			{
				"Right Ring Distal",
				"R Finger32",
				true
			},
			{
				"Right Little Proximal",
				"R Finger4",
				true
			},
			{
				"Right Little Intermediate",
				"R Finger41",
				true
			},
			{
				"Right Little Distal",
				"R Finger42",
				true
			}
		};

		private static readonly BoneRelationshipList MixamoBoneNames = new BoneRelationshipList
		{
			{
				"Head",
				"Head",
				false
			},
			{
				"Neck",
				"Neck",
				true
			},
			{
				"Chest",
				"Spine1",
				true
			},
			{
				"UpperChest",
				"Spine2",
				true
			},
			{
				"Spine",
				"Spine",
				false
			},
			{
				"Hips",
				"Hips",
				false
			},
			{
				"LeftShoulder",
				"LeftShoulder",
				true
			},
			{
				"LeftUpperArm",
				"LeftArm",
				false
			},
			{
				"LeftLowerArm",
				"LeftForeArm",
				false
			},
			{
				"LeftHand",
				"LeftHand",
				false
			},
			{
				"RightShoulder",
				"RightShoulder",
				true
			},
			{
				"RightUpperArm",
				"RightArm",
				false
			},
			{
				"RightLowerArm",
				"RightForeArm",
				false
			},
			{
				"RightHand",
				"RightHand",
				false
			},
			{
				"LeftUpperLeg",
				"LeftUpLeg",
				false
			},
			{
				"LeftLowerLeg",
				"LeftLeg",
				false
			},
			{
				"LeftFoot",
				"LeftFoot",
				false
			},
			{
				"LeftToes",
				"LeftToeBase",
				true
			},
			{
				"RightUpperLeg",
				"RightUpLeg",
				false
			},
			{
				"RightLowerLeg",
				"RightLeg",
				false
			},
			{
				"RightFoot",
				"RightFoot",
				false
			},
			{
				"RightToes",
				"RightToeBase",
				true
			},
			{
				"Left Thumb Proximal",
				"LeftHandThumb1",
				true
			},
			{
				"Left Thumb Intermediate",
				"LeftHandThumb2",
				true
			},
			{
				"Left Thumb Distal",
				"LeftHandThumb3",
				true
			},
			{
				"Left Index Proximal",
				"LeftHandIndex1",
				true
			},
			{
				"Left Index Intermediate",
				"LeftHandIndex2",
				true
			},
			{
				"Left Index Distal",
				"LeftHandIndex3",
				true
			},
			{
				"Left Middle Proximal",
				"LeftHandMiddle1",
				true
			},
			{
				"Left Middle Intermediate",
				"LeftHandMiddle2",
				true
			},
			{
				"Left Middle Distal",
				"LeftHandMiddle3",
				true
			},
			{
				"Left Ring Proximal",
				"LeftHandRing1",
				true
			},
			{
				"Left Ring Intermediate",
				"LeftHandRing2",
				true
			},
			{
				"Left Ring Distal",
				"LeftHandRing3",
				true
			},
			{
				"Left Little Proximal",
				"LeftHandPinky1",
				true
			},
			{
				"Left Little Intermediate",
				"LeftHandPinky2",
				true
			},
			{
				"Left Little Distal",
				"LeftHandPinky3",
				true
			},
			{
				"Right Thumb Proximal",
				"RightHandThumb1",
				true
			},
			{
				"Right Thumb Intermediate",
				"RightHandThumb2",
				true
			},
			{
				"Right Thumb Distal",
				"RightHandThumb3",
				true
			},
			{
				"Right Index Proximal",
				"RightHandIndex1",
				true
			},
			{
				"Right Index Intermediate",
				"RightHandIndex2",
				true
			},
			{
				"Right Index Distal",
				"RightHandIndex3",
				true
			},
			{
				"Right Middle Proximal",
				"RightHandMiddle1",
				true
			},
			{
				"Right Middle Intermediate",
				"RightHandMiddle2",
				true
			},
			{
				"Right Middle Distal",
				"RightHandMiddle3",
				true
			},
			{
				"Right Ring Proximal",
				"RightHandRing1",
				true
			},
			{
				"Right Ring Intermediate",
				"RightHandRing2",
				true
			},
			{
				"Right Ring Distal",
				"RightHandRing3",
				true
			},
			{
				"Right Little Proximal",
				"RightHandPinky1",
				true
			},
			{
				"Right Little Intermediate",
				"RightHandPinky2",
				true
			},
			{
				"Right Little Distal",
				"RightHandPinky3",
				true
			}
		};

		private AssetLoaderOptions _loaderOptions;

		protected void Start()
		{
			this._loaderOptions = AssetLoaderOptions.CreateInstance();
			this._loaderOptions.UseLegacyAnimations = false;
			this._loaderOptions.AnimatorController = this.RuntimeAnimatorController;
		}

		public bool LoadAvatar(string filename, GameObject templateAvatar)
		{
			if ((UnityEngine.Object)this.CurrentAvatar != (UnityEngine.Object)null)
			{
				UnityEngine.Object.Destroy(this.CurrentAvatar);
			}
			GameObject gameObject = default(GameObject);
			try
			{
				using (AssetLoader assetLoader = new AssetLoader())
				{
					gameObject = assetLoader.LoadFromFile(filename, this._loaderOptions, templateAvatar);
				}
			}
			catch
			{
				return false;
			}
			if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
			{
				if ((UnityEngine.Object)templateAvatar != (UnityEngine.Object)null)
				{
					gameObject.transform.parent = templateAvatar.transform;
					this.CurrentAvatar = templateAvatar;
				}
				else
				{
					this.CurrentAvatar = gameObject;
				}
				this.CurrentAvatar.transform.localScale = Vector3.one * this.Scale;
				this.CurrentAvatar.tag = "Player";
				this.SetupCapsuleCollider();
				return this.BuildAvatar();
			}
			return false;
		}

		private bool BuildAvatar()
		{
			Animator component = this.CurrentAvatar.GetComponent<Animator>();
			if ((UnityEngine.Object)component == (UnityEngine.Object)null)
			{
				return false;
			}
			List<SkeletonBone> list = new List<SkeletonBone>();
			List<HumanBone> list2 = new List<HumanBone>();
			Dictionary<string, Transform> dictionary = this.FindOutBoneTransforms(this.CurrentAvatar);
			if (dictionary.Count == 0)
			{
				return false;
			}
			foreach (KeyValuePair<string, Transform> item in dictionary)
			{
				list2.Add(AvatarLoader.CreateHumanBone(item.Key, item.Value.name));
			}
			Transform[] componentsInChildren = this.CurrentAvatar.GetComponentsInChildren<Transform>();
			Transform transform = componentsInChildren[1];
			list.Add(AvatarLoader.CreateSkeletonBone(transform));
			transform.localEulerAngles = Vector3.zero;
			foreach (Transform transform2 in componentsInChildren)
			{
				MeshRenderer[] componentsInChildren2 = ((Component)transform2).GetComponentsInChildren<MeshRenderer>();
				if (componentsInChildren2.Length <= 0)
				{
					SkinnedMeshRenderer[] componentsInChildren3 = ((Component)transform2).GetComponentsInChildren<SkinnedMeshRenderer>();
					if (componentsInChildren3.Length <= 0)
					{
						list.Add(AvatarLoader.CreateSkeletonBone(transform2));
					}
				}
			}
			HumanDescription humanDescription = default(HumanDescription);
			humanDescription.armStretch = this.ArmStretch;
			humanDescription.feetSpacing = this.FeetSpacing;
			humanDescription.hasTranslationDoF = this.HasTranslationDof;
			humanDescription.legStretch = this.LegStretch;
			humanDescription.lowerArmTwist = this.LowerArmTwist;
			humanDescription.lowerLegTwist = this.LowerLegTwist;
			humanDescription.upperArmTwist = this.UpperArmTwist;
			humanDescription.upperLegTwist = this.UpperLegTwist;
			humanDescription.skeleton = list.ToArray();
			humanDescription.human = list2.ToArray();
			component.avatar = AvatarBuilder.BuildHumanAvatar(this.CurrentAvatar, humanDescription);
			return true;
		}

		private Dictionary<string, Transform> FindOutBoneTransforms(GameObject loadedObject)
		{
			Dictionary<string, Transform> dictionary = new Dictionary<string, Transform>();
			List<BoneRelationshipList> list = new List<BoneRelationshipList>();
			list.Add(AvatarLoader.BipedBoneNames);
			list.Add(AvatarLoader.MixamoBoneNames);
			if (this.CustomBoneNames != null)
			{
				list.Add(this.CustomBoneNames);
			}
			bool flag = false;
			foreach (BoneRelationshipList item in list)
			{
				if (flag)
				{
					return dictionary;
				}
				flag = true;
				foreach (BoneRelationship item2 in item)
				{
					Transform transform = loadedObject.transform.FindDeepChild(item2.BoneName, true);
					if ((UnityEngine.Object)transform == (UnityEngine.Object)null)
					{
						if (!item2.Optional)
						{
							dictionary.Clear();
							flag = false;
							break;
						}
					}
					else
					{
						dictionary.Add(item2.HumanBone, transform);
					}
				}
			}
			return dictionary;
		}

		private void SetupCapsuleCollider()
		{
			CapsuleCollider component = this.CurrentAvatar.GetComponent<CapsuleCollider>();
			if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
			{
				Bounds bounds = this.CurrentAvatar.transform.EncapsulateBounds();
				float num = 1f / this.Scale;
				Vector3 extents = bounds.extents;
				float num2 = extents.x * num;
				Vector3 extents2 = bounds.extents;
				float num3 = extents2.y * num;
				Vector3 extents3 = bounds.extents;
				float num4 = extents3.z * num;
				component.height = (float)Math.Round((double)(num3 * 2f), 1);
				component.radius = (float)Math.Round((double)(Mathf.Sqrt(num2 * num2 + num4 * num4) * 0.5f), 1);
				component.center = new Vector3(0f, (float)Math.Round((double)num3, 1) + this.HeightOffset, 0f);
			}
		}

		private static SkeletonBone CreateSkeletonBone(Transform boneTransform)
		{
			SkeletonBone result = default(SkeletonBone);
			result.name = boneTransform.name;
			result.position = boneTransform.localPosition;
			result.rotation = boneTransform.localRotation;
			result.scale = boneTransform.localScale;
			return result;
		}

		private static HumanBone CreateHumanBone(string humanName, string boneName)
		{
			HumanBone result = default(HumanBone);
			result.boneName = boneName;
			result.humanName = humanName;
			result.limit.useDefaultValues = true;
			return result;
		}
	}
}
