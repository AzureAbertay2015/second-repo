using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameRules m_GameRules;
	private bool m_GameRestarted;
    private GameObject m_ResumeButton, m_RestartButton;

    // Use this for initialization
    void Start()
    {
		
		m_GameRules = GameManager.GetGameRules();

        //Attach camera to each canvas in ui manager
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

    void Awake()
    {
        //get references to first button in each menu
        m_RestartButton = GetComponentInChildren<RestartLevelScript>().gameObject.GetComponentInParent<Button>().gameObject;
        m_ResumeButton = GetComponentInChildren<Button>().gameObject.GetComponentInParent<Button>().gameObject;
    }

    void ScanForKeyStroke()
    {
        if (!CrossPlatformInputManager.GetButtonDown("Submit"))
        {
            //If pause button is pressed
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                m_GameRules.TogglePauseMenu();

                //Set resume button to be selected
                EventSystem.current.SetSelectedGameObject(m_ResumeButton);
            }
        }
    }
	
	void ScanForDeath()
	{
        //If player is dead and the game has been restarted
        if (!m_GameRules.IsPlayerAlive() && m_GameRestarted)
		{
            m_GameRules.ToggleDeathMenu();

            //Set restart button to be selected
            EventSystem.current.SetSelectedGameObject(m_RestartButton);
            m_GameRestarted = false;
		}
		if (m_GameRules.IsPlayerAlive())
			m_GameRestarted = true;
	}    
}