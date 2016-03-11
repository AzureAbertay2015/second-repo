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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_CoolerTurnedOn)
            {
                GameManager.GetGameRules().CoolDownPlayer();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_CoolerTurnedOn)
            {
                GameManager.GetTemperatureManager().HeaterCooler(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_CoolerTurnedOn)
            {
                GameManager.GetTemperatureManager().HeaterCooler(false);
            }
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
