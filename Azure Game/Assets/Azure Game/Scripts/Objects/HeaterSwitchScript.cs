using UnityEngine;
using System.Collections;

public class HeaterSwitchScript : SwitchScript {

	public GameObject m_Heater;
	private HeaterEmissionScript m_HeaterEmissionScript;
	private HeaterEmissionScript m_BaseEmissionScript;


	void Start()
	{
		m_HeaterEmissionScript = m_Heater.GetComponent<HeaterEmissionScript>();
		
		m_BaseEmissionScript = transform.GetChild(1).gameObject.GetComponent<HeaterEmissionScript>();
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				if (m_SwitchState)
				{
					SwitchOff();
					m_Heater.SendMessage("SwitchOff");
					m_HeaterEmissionScript.EmissionSwitchOff();
					m_BaseEmissionScript.EmissionSwitchOff();

				}
				else
				{
					SwitchOn();
					m_Heater.SendMessage("SwitchOn");
					m_HeaterEmissionScript.EmissionSwitchOn();
					m_BaseEmissionScript.EmissionSwitchOn();
				}
			}
		}
	}
	
}
