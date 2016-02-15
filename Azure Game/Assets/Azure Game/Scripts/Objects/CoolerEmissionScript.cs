using UnityEngine;
using System.Collections;

public class CoolerEmissionScript : MonoBehaviour {

	private Color m_EmissionOnColour;
	void Start()
	{
		m_EmissionOnColour = new Color(0, 0.75f, 0.25f);
		foreach (Material mat in GetComponent<Renderer>().materials)
		{
			mat.EnableKeyword("_EMISSION");
			mat.SetColor("_EmissionColor", m_EmissionOnColour);
		}

	}
	public void EmissionSwitchOn()
	{
		foreach (Material mat in GetComponent<Renderer>().materials)
		{
			mat.SetColor("_EmissionColor", m_EmissionOnColour);
		}
	}

	public void EmissionSwitchOff()
	{
		foreach (Material mat in GetComponent<Renderer>().materials)
		{
			mat.SetColor("_EmissionColor", Color.black);
		}
	}
}
