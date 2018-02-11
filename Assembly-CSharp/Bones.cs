using UnityEngine;

public class Bones
{
	public Transform mainHand;

	public Transform Armature;

	public Transform Root;

	public Transform SpineLower;

	public Transform SpineMiddle;

	public Transform SpineUpper;

	public Transform Belly;

	public Transform Shoulder_L;

	public Transform UpperArm_L;

	public Transform LowerArm_L;

	public Transform Hand_L;

	public Transform Thumb0_L;

	public Transform Thumb1_L;

	public Transform Thumb2_L;

	public Transform Finger00_L;

	public Transform Finger01_L;

	public Transform Finger02_L;

	public Transform Finger10_L;

	public Transform Finger11_L;

	public Transform Finger12_L;

	public Transform Finger20_L;

	public Transform Finger21_L;

	public Transform Finger22_L;

	public Transform Finger30_L;

	public Transform Finger31_L;

	public Transform Finger32_L;

	public Transform Shoulder_R;

	public Transform UpperArm_R;

	public Transform LowerArm_R;

	public Transform Hand_R;

	public Transform Thumb0_R;

	public Transform Thumb1_R;

	public Transform Thumb2_R;

	public Transform Finger00_R;

	public Transform Finger01_R;

	public Transform Finger02_R;

	public Transform Finger10_R;

	public Transform Finger11_R;

	public Transform Finger12_R;

	public Transform Finger20_R;

	public Transform Finger21_R;

	public Transform Finger22_R;

	public Transform Finger30_R;

	public Transform Finger31_R;

	public Transform Finger32_R;

	public Transform Hip_L;

	public Transform UpperLeg_L;

	public Transform LowerLeg_L;

	public Transform Foot_L;

	public Transform Footpad_L;

	public Transform Toe0_L;

	public Transform Toe1_L;

	public Transform Toe2_L;

	public Transform Toe3_L;

	public Transform Hip_R;

	public Transform UpperLeg_R;

	public Transform LowerLeg_R;

	public Transform Foot_R;

	public Transform Footpad_R;

	public Transform Toe0_R;

	public Transform Toe1_R;

	public Transform Toe2_R;

	public Transform Toe3_R;

	public int numToes;

	public Transform Butt_L;

	public Transform Butt_R;

	public Transform Asshole;

	public Transform AssholeTop;

	public Transform AssholeBottom;

	public Transform AssholeSide_L;

	public Transform AssholeSide_R;

	public Transform Pubic;

	public Transform Sheath;

	public Transform Penis0;

	public Transform Penis1;

	public Transform Penis2;

	public Transform Penis3;

	public Transform Penis0_inverter;

	public Transform Penis1_inverter;

	public Transform Penis2_inverter;

	public Transform Penis3_inverter;

	public Transform Penis4;

	public Transform UrethraTop;

	public Transform UrethraBottom;

	public Transform UrethraSide_L;

	public Transform UrethraSide_R;

	public Transform Ballsack0;

	public Transform Ballsack1;

	public Transform Nut_L;

	public Transform Nut_R;

	public Transform Clit;

	public Transform LabiaMajorUpper_L;

	public Transform LabiaMajorLower_L;

	public Transform LabiaMinorUpper_L;

	public Transform LabiaMinorLower_L;

	public Transform LabiaMajorUpper_R;

	public Transform LabiaMajorLower_R;

	public Transform LabiaMinorUpper_R;

	public Transform LabiaMinorLower_R;

	public Transform VaginaRearLip;

	public Transform VaginaUpper_L;

	public Transform VaginaLower_L;

	public Transform VaginaUpper_R;

	public Transform VaginaLower_R;

	public Transform Knot_L;

	public Transform Knot_R;

	public Transform Tail0;

	public Transform Tail1;

	public Transform Tail2;

	public Transform Tail3;

	public Transform Tail4;

	public Transform Tail5;

	public Transform Tail6;

	public Transform Tail7;

	public Transform Tail8;

	public Transform Neck;

	public Transform Neck_inverter;

	public Transform Head;

	public Transform Jaw;

	public Transform MouthCornerL;

	public Transform MouthCornerR;

	public Transform Tongue0;

	public Transform Tongue1;

	public Transform Tongue2;

	public Transform TopLip;

	public Transform Nose;

	public Transform Cheek_L;

	public Transform UpperEyelid_L;

	public Transform LowerEyelid_L;

	public Transform Eye_L;

	public Transform Pupil_L;

	public Transform EyebrowInner_L;

	public Transform EyebrowOuter_L;

	public Transform Cheek_R;

	public Transform UpperEyelid_R;

	public Transform LowerEyelid_R;

	public Transform Eye_R;

	public Transform Pupil_R;

	public Transform Ear0_R;

	public Transform Ear1_R;

	public Transform Ear2_R;

	public Transform Ear3_R;

	public Transform Ear4_R;

	public Transform Ear5_R;

	public Transform Ear0_L;

	public Transform Ear1_L;

	public Transform Ear2_L;

	public Transform Ear3_L;

	public Transform Ear4_L;

	public Transform Ear5_L;

	public Transform EyebrowInner_R;

	public Transform EyebrowOuter_R;

	public Transform Breast_R;

	public Transform Breast_L;

	public Transform Wing0_R;

	public Transform Wing1_R;

	public Transform Wing2_R;

	public Transform Wing3_R;

	public Transform Wing4_R;

	public Transform Wing0_L;

	public Transform Wing1_L;

	public Transform Wing2_L;

	public Transform Wing3_L;

	public Transform Wing4_L;

	public Collider AssholeCollider;

	public Collider Hip_LCollider;

	public Collider Butt_LCollider;

	public Collider UpperLeg_LCollider;

	public Collider LowerLeg_LCollider;

	public Collider Foot_LCollider;

	public Collider Footpad_LCollider;

	public Collider Hip_RCollider;

	public Collider Butt_RCollider;

	public Collider UpperLeg_RCollider;

	public Collider LowerLeg_RCollider;

	public Collider Foot_RCollider;

	public Collider Footpad_RCollider;

	public Collider PubicCollider1;

	public Collider PubicCollider2;

	public Collider Ballsack0Collider;

	public Collider Ballsack1Collider;

	public Collider Nut_LCollider;

	public Collider Nut_RCollider;

	public Collider Penis0Collider;

	public Collider Penis1Collider;

	public Collider Penis2Collider;

	public Collider Penis3Collider;

	public Collider Penis4Collider;

	public Collider Penis4Collider2;

	public Collider BallCatcherCollider;

	public Collider SpineLowerCollider1;

	public Collider SpineLowerCollider2;

	public Collider SpineMiddleCollider;

	public Collider SpineUpperCollider;

	public Collider Breast_LCollider;

	public Collider Breast_RCollider;

	public Collider NeckCollider;

	public Collider HeadCollider1;

	public Collider HeadCollider2;

	public Collider HeadCollider3;

	public Collider HeadCollider4;

	public Collider Ear1_LCollider;

	public Collider Ear2_LCollider;

	public Collider Ear4_LCollider;

	public Collider Ear1_RCollider;

	public Collider Ear2_RCollider;

	public Collider Ear4_RCollider;

	public Collider Shoulder_LCollider;

	public Collider UpperArm_LCollider;

	public Collider LowerArmLCollider0;

	public Collider LowerArmLCollider1;

	public Collider Hand_LCollider;

	public Collider Finger02_LCollider;

	public Collider Finger30_LCollider;

	public Collider Thumb0_LCollider;

	public Collider Thumb2_LCollider;

	public Collider Shoulder_RCollider;

	public Collider UpperArm_RCollider;

	public Collider LowerArmRCollider0;

	public Collider LowerArmRCollider1;

	public Collider Hand_RCollider;

	public Collider Finger02_RCollider;

	public Collider Finger30_RCollider;

	public Collider Thumb0_RCollider;

	public Collider Thumb2_RCollider;

	public Collider Wing3_LCollider;

	public Collider Wing4_LCollider;

	public Collider Wing3_RCollider;

	public Collider Wing4_RCollider;

	public Transform InjectionPoint;
}
