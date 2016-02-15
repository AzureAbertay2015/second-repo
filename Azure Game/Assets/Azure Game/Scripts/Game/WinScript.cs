using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {
<<<<<<< HEAD
	//public GameManager m_GameManager;
=======
	
>>>>>>> refs/remotes/origin/master
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
<<<<<<< HEAD
            //m_GameManager.ToggleWinMenu();
            GameManager.GetGameRules().ToggleWinMenu();
=======
			GameManager.GetGameRules().ToggleWinMenu();
>>>>>>> refs/remotes/origin/master
			m_Toggled = true;
		}
	}
}
