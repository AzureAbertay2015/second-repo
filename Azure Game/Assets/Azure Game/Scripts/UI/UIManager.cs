using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private GameRules m_GameRules;
	private bool m_GameRestarted;

    // Use this for initialization
    void Start()
    {
		
		m_GameRules = GameManager.GetGameRules();
        foreach (Canvas canvas in GetComponentsInChildren<Canvas>())
        {
            canvas.worldCamera = Camera.main;
        }
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
            m_GameRules.TogglePauseMenu();
    }
	
	void ScanForDeath()
	{
		if (!m_GameRules.IsPlayerAlive() && m_GameRestarted)
		{
			m_GameRules.ToggleDeathMenu();
			m_GameRestarted = false;
		}
		if (m_GameRules.IsPlayerAlive())
			m_GameRestarted = true;
	}    
}