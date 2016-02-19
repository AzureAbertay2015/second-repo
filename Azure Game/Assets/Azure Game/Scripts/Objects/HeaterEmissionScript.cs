using UnityEngine;
using System.Collections;

public class HeaterEmissionScript : MonoBehaviour
{
	public Material m_SwitchedOnMaterial;
	public Material m_SwitchedOffMaterial;

	void Start()
	{
		gameObject.GetComponent<Renderer>().material = m_SwitchedOnMaterial;
	}
	public void EmissionSwitchOn()
	{
		gameObject.GetComponent<Renderer>().material = m_SwitchedOnMaterial;
	}

	public void EmissionSwitchOff()
	{
		gameObject.GetComponent<Renderer>().material = m_SwitchedOffMaterial;
	}
}
