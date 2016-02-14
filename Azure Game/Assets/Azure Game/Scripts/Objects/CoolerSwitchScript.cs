using UnityEngine;
using System.Collections;

public class CoolerSwitchScript : SwitchScript {

	public GameObject m_Cooler;

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
				}
				else
				{
					SwitchOn();
					m_Cooler.SendMessage("SwitchOn");
				}
			}
		}
	}
}
