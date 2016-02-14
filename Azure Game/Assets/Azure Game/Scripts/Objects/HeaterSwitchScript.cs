using UnityEngine;
using System.Collections;

public class HeaterSwitchScript : SwitchScript {

	public GameObject m_Heater;
	
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
				}
				else
				{
					SwitchOn();
					m_Heater.SendMessage("SwitchOn");
				}
			}
		}
	}
	
}
