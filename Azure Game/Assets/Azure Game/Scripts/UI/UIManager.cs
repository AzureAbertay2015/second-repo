using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
<<<<<<< HEAD
    //public GameManager GM;
=======
    private GameRules m_GameRules;
>>>>>>> refs/remotes/origin/master
	private bool m_GameRestarted;

    // Use this for initialization
    void Start()
    {
		///
		m_GameRules = GameManager.GetGameRules();
	  
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
<<<<<<< HEAD
            GameManager.GetGameRules().TogglePauseMenu();
=======
            m_GameRules.TogglePauseMenu();
>>>>>>> refs/remotes/origin/master
    }
	
	void ScanForDeath()
	{
<<<<<<< HEAD
        
// PeterM branch - commenting this out for now
/*
        if (!GM.IsPlayerAlive() && m_GameRestarted)
=======
		if (!m_GameRules.IsPlayerAlive() && m_GameRestarted)
>>>>>>> refs/remotes/origin/master
		{
			m_GameRules.ToggleDeathMenu();
			m_GameRestarted = false;
		}
		if (m_GameRules.IsPlayerAlive())
			m_GameRestarted = true;
*/

	}    
}