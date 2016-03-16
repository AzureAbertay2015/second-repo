using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeaterScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_Triggered;
	private bool m_HeaterTurnedOn;
	private HeaterEmissionScript m_HeaterEmissionScript;

	// Use this for initialization
	void Start ()
    {
		m_Triggered = false;
		m_HeaterTurnedOn = true;
		m_HeaterEmissionScript = gameObject.GetComponent<HeaterEmissionScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {
            if (m_HeaterTurnedOn)
            {
                other.gameObject.GetComponent<StateChanger>().m_Triggered = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {            
            if(m_HeaterTurnedOn)
            {
                GameManager.GetGameRules().HeatUpObject(other.gameObject.GetComponent<StateChanger>());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {
            if (m_HeaterTurnedOn)
            {
                other.gameObject.GetComponent<StateChanger>().m_Triggered = false;
            }
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
