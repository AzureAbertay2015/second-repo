using UnityEngine;
using System.Collections;

public class HeaterScript : MonoBehaviour {

	public GameManager m_GameManager;
	private bool m_Triggered;

	// Use this for initialization
	void Start () {
		m_Triggered = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!m_Triggered)
			{
				m_GameManager.HeatUpPlayer();
				m_Triggered = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			m_Triggered = false;
		}
	}
}
