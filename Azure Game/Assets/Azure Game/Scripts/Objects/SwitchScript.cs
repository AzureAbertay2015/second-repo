using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
	public bool m_SwitchState;

	public enum SwitchType { AirConditioner, Heater };
	public SwitchType m_SwitchType;
	
	// Use this for initialization
	void Start () {
		transform.GetChild(0).gameObject.transform.Rotate(6, 0, 0, Space.Self);
		m_SwitchState = true;	
	}

	public void SwitchOn()
	{
		m_SwitchState = true;
		GameObject o = this.transform.GetChild(0).gameObject;
		o.transform.Rotate(12, 0, 0, Space.Self);
		if (m_SwitchType == SwitchType.AirConditioner) {
			GameManager.GetGameRules().CoolDownRoom();
		}
		else if (m_SwitchType == SwitchType.Heater) {
			GameManager.GetGameRules().HeatUpRoom();
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
		}
		else if (m_SwitchType == SwitchType.Heater)
		{
			GameManager.GetGameRules().CoolDownRoom();
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
