using UnityEngine;
using System.Collections;

public class HeaterScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_Triggered;
	private bool m_HeaterTurnedOn;

	// Use this for initialization
	void Start () {
		m_Triggered = false;
		m_HeaterTurnedOn = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!m_Triggered && m_HeaterTurnedOn)
			{
				GameManager.GetGameRules().HeatUpPlayer();
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
		m_HeaterTurnedOn = true;
		transform.GetChild(0).GetComponent<Light>().enabled = true;

		if (!transform.GetChild(0).GetComponent<Light>().enabled)
			Debug.LogError("Light is still off");
	}

	void SwitchOff()
	{
		m_HeaterTurnedOn = false;
		transform.GetChild(0).GetComponent<Light>().enabled = false;

		if (transform.GetChild(0).GetComponent<Light>().enabled)
			Debug.LogError("Light is still on");

	}
}
