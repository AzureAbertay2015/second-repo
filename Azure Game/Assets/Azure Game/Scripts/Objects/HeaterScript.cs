using UnityEngine;
using System.Collections;

public class HeaterScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_Triggered;
	private bool m_HeaterTurnedOn;
	private HeaterEmissionScript m_HeaterEmissionScript;
	private TemperatureManager m_TemperatureManager;

	// Use this for initialization
	void Start () {
		m_Triggered = false;
		m_HeaterTurnedOn = true;
		m_HeaterEmissionScript = gameObject.GetComponent<HeaterEmissionScript>();
		m_TemperatureManager = FindObjectOfType<TemperatureManager>();
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!m_Triggered && m_HeaterTurnedOn)
			{
				//GameManager.GetGameRules().HeatUpPlayer();
				//StartCoroutine(m_TemperatureManager.HeatUpPlayer());
				m_TemperatureManager.StartCoroutine("HeatUpPlayer");
				m_Triggered = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			m_Triggered = false;
			//StopCoroutine(m_TemperatureManager.HeatUpPlayer());
			m_TemperatureManager.StopCoroutine("HeatUpPlayer");
			m_TemperatureManager.ResetTempChangeSpeed();
		}
	}

	void SwitchOn()
	{
		m_HeaterTurnedOn = true;
		m_HeaterEmissionScript.EmissionSwitchOn();
	}

	void SwitchOff()
	{
		m_HeaterTurnedOn = false;
		m_HeaterEmissionScript.EmissionSwitchOff();
	}
}
