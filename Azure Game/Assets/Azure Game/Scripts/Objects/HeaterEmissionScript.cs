using UnityEngine;
using System.Collections;

public class HeaterEmissionScript : MonoBehaviour
{
	private Color m_EmissionOnColor;
	void Start()
	{
		m_EmissionOnColor = new Color(1f, 0.2f, 0);
		foreach (Material mat in GetComponent<Renderer>().materials)
		{
			mat.EnableKeyword("_EMISSION");
			mat.SetColor("_EmissionColor", m_EmissionOnColor);
		}

	}
	public void EmissionSwitchOn()
	{
		foreach (Material mat in GetComponent<Renderer>().materials)
		{
			mat.SetColor("_EmissionColor", m_EmissionOnColor);
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
