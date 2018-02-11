using UnityEngine;

public class DemoUI : MonoBehaviour
{
	protected SSAOPro m_SSAOPro;

	private void Start()
	{
		this.m_SSAOPro = base.GetComponent<SSAOPro>();
	}

	private void OnGUI()
	{
		GUI.Box(new Rect(10f, 10f, 130f, 194f), string.Empty);
		GUI.BeginGroup(new Rect(20f, 15f, 200f, 200f));
		this.m_SSAOPro.enabled = GUILayout.Toggle(this.m_SSAOPro.enabled, "Enable SSAO");
		this.m_SSAOPro.DebugAO = GUILayout.Toggle(this.m_SSAOPro.DebugAO, "Show AO Only");
		bool value = this.m_SSAOPro.Blur == SSAOPro.BlurMode.HighQualityBilateral;
		value = GUILayout.Toggle(value, "HQ Bilateral Blur");
		this.m_SSAOPro.Blur = (SSAOPro.BlurMode)(value ? 2 : 0);
		GUILayout.Space(10f);
		bool value2 = this.m_SSAOPro.Samples == SSAOPro.SampleCount.VeryLow;
		value2 = GUILayout.Toggle(value2, "4 samples");
		this.m_SSAOPro.Samples = ((!value2) ? this.m_SSAOPro.Samples : SSAOPro.SampleCount.VeryLow);
		value2 = (this.m_SSAOPro.Samples == SSAOPro.SampleCount.Low);
		value2 = GUILayout.Toggle(value2, "8 samples");
		this.m_SSAOPro.Samples = (value2 ? SSAOPro.SampleCount.Low : this.m_SSAOPro.Samples);
		value2 = (this.m_SSAOPro.Samples == SSAOPro.SampleCount.Medium);
		value2 = GUILayout.Toggle(value2, "12 samples");
		this.m_SSAOPro.Samples = ((!value2) ? this.m_SSAOPro.Samples : SSAOPro.SampleCount.Medium);
		value2 = (this.m_SSAOPro.Samples == SSAOPro.SampleCount.High);
		value2 = GUILayout.Toggle(value2, "16 samples");
		this.m_SSAOPro.Samples = ((!value2) ? this.m_SSAOPro.Samples : SSAOPro.SampleCount.High);
		value2 = (this.m_SSAOPro.Samples == SSAOPro.SampleCount.Ultra);
		value2 = GUILayout.Toggle(value2, "20 samples");
		this.m_SSAOPro.Samples = ((!value2) ? this.m_SSAOPro.Samples : SSAOPro.SampleCount.Ultra);
		GUI.EndGroup();
	}
}
