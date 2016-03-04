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

	new public void SwitchOn()
	{
		m_SwitchState = true;
		GameObject o = this.transform.GetChild(0).gameObject;
		o.transform.Rotate(12, 0, 0, Space.Self);
		if (m_SwitchType == SwitchType.AirConditioner)
		{
			//GameManager.GetGameRules().CoolDownRoom();
			m_CoolerEmissionScript.EmissionSwitchOn();
		}
		else if (m_SwitchType == SwitchType.Heater)
		{
			//GameManager.GetGameRules().HeatUpRoom();
			m_HeaterEmissionScript.EmissionSwitchOn();
		}
	}

	new public void SwitchOff()
	{
		m_SwitchState = false;
		GameObject o = this.transform.GetChild(0).gameObject;
		o.transform.Rotate(348, 0, 0, Space.Self);
		if (m_SwitchType == SwitchType.AirConditioner)
		{
			//GameManager.GetGameRules().HeatUpRoom();
			m_CoolerEmissionScript.EmissionSwitchOff();
		}
		else if (m_SwitchType == SwitchType.Heater)
		{
			//GameManager.GetGameRules().CoolDownRoom();
			m_HeaterEmissionScript.EmissionSwitchOff();
		}
	}
}
