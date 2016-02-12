using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    //public GameManager GM;
	private bool m_GameRestarted;

    // Use this for initialization
    void Start()
    {
       ///
    }

    // Update is called once per frame
    void Update()
    {
        ScanForKeyStroke();
		ScanForDeath();
    }

    void ScanForKeyStroke()
    {
        if (Input.GetKeyDown("escape")&& !Input.GetKeyDown("space"))
            GameManager.GetGameRules().TogglePauseMenu();
    }
	
	void ScanForDeath()
	{
        
// PeterM branch - commenting this out for now
/*
        if (!GM.IsPlayerAlive() && m_GameRestarted)
		{
			GM.ToggleDeathMenu();
			m_GameRestarted = false;
		}
		if (GM.IsPlayerAlive())
			m_GameRestarted = true;
*/

	}    
}