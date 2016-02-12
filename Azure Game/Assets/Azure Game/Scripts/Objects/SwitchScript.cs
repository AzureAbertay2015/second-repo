using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {

	public Quaternion m_OpenRotation;
	public Quaternion m_ClosedRotation;

	public bool m_SwitchState;

	public enum SwitchType { AirConditioner, Heater };
	public SwitchType m_SwitchType;
	
	// Use this for initialization
	void Start () {
		m_OpenRotation = Quaternion.Euler(6, 0, 0);
		m_ClosedRotation = Quaternion.Euler(354, 0, 0);
		m_SwitchState = false;	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void SwitchOn()
	{
		m_SwitchState = true;
		GameObject o = this.transform.GetChild(0).gameObject;
		o.transform.rotation = m_OpenRotation;
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
		o.transform.rotation = m_ClosedRotation;
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
