using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameManager GM;
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
            GM.TogglePauseMenu();
    }
	
	void ScanForDeath()
	{
		if (!GM.IsPlayerAlive() && m_GameRestarted)
		{
			GM.ToggleDeathMenu();
			m_GameRestarted = false;
		}
		if (GM.IsPlayerAlive())
			m_GameRestarted = true;
	}    
}