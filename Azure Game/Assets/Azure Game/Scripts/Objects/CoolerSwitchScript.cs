using UnityEngine;
using System.Collections;

public class CoolerSwitchScript : SwitchScript {

	public GameObject m_Cooler;
	private CoolerEmissionScript m_BaseEmissionScript;

	void Start()
	{
		m_CoolerEmissionScript = m_Cooler.GetComponent<CoolerEmissionScript>();
		m_BaseEmissionScript = transform.GetChild(1).gameObject.GetComponent<CoolerEmissionScript>();
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
					m_Cooler.SendMessage("SwitchOff");
					m_CoolerEmissionScript.EmissionSwitchOff();
					m_BaseEmissionScript.EmissionSwitchOff();
				}
				else
				{
					SwitchOn();
					m_Cooler.SendMessage("SwitchOn");
					m_CoolerEmissionScript.EmissionSwitchOn();
					m_BaseEmissionScript.EmissionSwitchOn();
				}
			}
		}
	}
}
