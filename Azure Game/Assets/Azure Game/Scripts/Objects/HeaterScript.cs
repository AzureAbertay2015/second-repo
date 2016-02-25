using UnityEngine;
using System.Collections;

public class HeaterScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_Triggered;
	private bool m_HeaterTurnedOn;
	private HeaterEmissionScript m_HeaterEmissionScript;
    private TemperatureManager m_Tempmanager;

	// Use this for initialization
	void Start () {
		m_Triggered = false;
		m_HeaterTurnedOn = true;
		m_HeaterEmissionScript = gameObject.GetComponent<HeaterEmissionScript>();

        m_Tempmanager = GameObject.FindGameObjectWithTag("TemperatureManager").GetComponent<TemperatureManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_HeaterTurnedOn)
            {
                m_Tempmanager.HeaterCooler(true);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(m_HeaterTurnedOn)
            {
                GameManager.GetGameRules().HeatUpPlayer();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_HeaterTurnedOn)
            {
                m_Tempmanager.HeaterCooler(false);
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
