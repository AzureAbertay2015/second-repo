using UnityEngine;
using System.Collections;

public class CoolerScript : MonoBehaviour {

	//public GameManager m_GameManager;
	private bool m_CoolerTurnedOn;
	private CoolerEmissionScript m_CoolerEmissionScript;

    // Use this for initialization
    void Start () {

		m_CoolerTurnedOn = true;
		m_CoolerEmissionScript = gameObject.GetComponent<CoolerEmissionScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {
            if (m_CoolerTurnedOn)
            {
                other.gameObject.GetComponent<StateChanger>().m_Triggered = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {
            if (m_CoolerTurnedOn)
            {
                GameManager.GetGameRules().CoolDownObject(other.gameObject.GetComponent<StateChanger>());
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {
            if (m_CoolerTurnedOn)
            {
                other.gameObject.GetComponent<StateChanger>().m_Triggered = false;
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
