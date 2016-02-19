using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	private bool m_Toggled;
	// Use this for initialization
	void Start () {
		m_Toggled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !m_Toggled)
		{
			GameManager.GetGameRules().ToggleWinMenu();
			m_Toggled = true;
		}
	}
}
