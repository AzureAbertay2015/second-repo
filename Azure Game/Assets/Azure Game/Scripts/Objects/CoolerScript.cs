using UnityEngine;
using System.Collections;

public class CoolerScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_Triggered;
	private bool m_CoolerTurnedOn;

	// Use this for initialization
	void Start () {
		m_Triggered = false;
		m_CoolerTurnedOn = true;
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
		transform.GetChild(0).GetComponent<Light>().enabled = true;

		if (!transform.GetChild(0).GetComponent<Light>().enabled)
			Debug.LogError("Light is still off");
	}

	void SwitchOff()
	{
		m_CoolerTurnedOn = false;
		transform.GetChild(0).GetComponent<Light>().enabled = false;

		if (transform.GetChild(0).GetComponent<Light>().enabled)
			Debug.LogError("Light is still on");

	}
}
