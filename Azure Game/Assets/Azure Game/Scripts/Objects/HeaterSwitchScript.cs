using UnityEngine;
using System.Collections;

public class HeaterSwitchScript : SwitchScript {

	public GameObject m_Heater;
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
