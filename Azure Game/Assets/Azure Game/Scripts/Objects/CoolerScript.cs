using UnityEngine;
using System.Collections;

public class CoolerScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_Triggered;
	private bool m_CoolerTurnedOn;
	private CoolerEmissionScript m_CoolerEmissionScript;

	// Use this for initialization
	void Start () {
		m_Triggered = false;
		m_CoolerTurnedOn = true;
		m_CoolerEmissionScript = gameObject.GetComponent<CoolerEmissionScript>();
    }
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!m_Triggered && m_CoolerTurnedOn)
			{
				GameManager.GetGameRules().CoolDownPlayer();
				m_Triggered = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			m_Triggered = false;
		}
	}

	void SwitchOn()
	{
		m_CoolerTurnedOn = true;
		m_CoolerEmissionScript.EmissionSwitchOn();
	}

	void SwitchOff()
	{
		m_CoolerTurnedOn = false;
		m_CoolerEmissionScript.EmissionSwitchOff();
	}
}
