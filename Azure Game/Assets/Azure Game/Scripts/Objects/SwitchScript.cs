﻿using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
	public bool m_SwitchState;

	public enum SwitchType { AirConditioner, Heater };
	public SwitchType m_SwitchType;
	protected HeaterEmissionScript m_HeaterEmissionScript;
    protected CoolerEmissionScript m_CoolerEmissionScript;

	// Use this for initialization
	void Start () {

        m_SwitchState = false;

        if (m_SwitchType == SwitchType.Heater)
		{
			m_HeaterEmissionScript = gameObject.transform.GetChild(1).GetComponent<HeaterEmissionScript>();
		}
		else if (m_SwitchType == SwitchType.AirConditioner)
		{
			m_CoolerEmissionScript = gameObject.transform.GetChild(1).GetComponent<CoolerEmissionScript>();
		}
		else
		{
			Debug.LogError("Switch Type neither a cooler nor a heater. Check the Inspector.");
        }
    }

	public void SwitchOn()
	{
		m_SwitchState = true;
		GameObject o = this.transform.GetChild(0).gameObject;
		o.transform.Rotate(12, 0, 0, Space.Self);
		if (m_SwitchType == SwitchType.AirConditioner)
		{
			GameManager.GetGameRules().CoolDownRoom();
			m_CoolerEmissionScript.EmissionSwitchOn();
		}
		else if (m_SwitchType == SwitchType.Heater)
		{
			GameManager.GetGameRules().HeatUpRoom();
			m_HeaterEmissionScript.EmissionSwitchOn();
		}
	}

	public void SwitchOff()
	{
		m_SwitchState = false;
		GameObject o = this.transform.GetChild(0).gameObject;
		o.transform.Rotate(348, 0, 0, Space.Self);
		if (m_SwitchType == SwitchType.AirConditioner)
		{
			GameManager.GetGameRules().HeatUpRoom();
			m_CoolerEmissionScript.EmissionSwitchOff();
		}
		else if (m_SwitchType == SwitchType.Heater)
		{
			GameManager.GetGameRules().CoolDownRoom();
			m_HeaterEmissionScript.EmissionSwitchOff();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				if (m_SwitchState)
					SwitchOff();
				else
					SwitchOn();
			}
		}
	}
}
