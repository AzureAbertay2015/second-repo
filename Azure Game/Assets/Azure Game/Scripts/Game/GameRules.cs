﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRules : MonoBehaviour {

    
    private bool m_PlayerAlive;
    public TemperatureManager m_Tempmanager;
            
    //----------------------------------------
    // handles
 
    //-----------------------------------------
    // function definitions
    void Start()
    {
        m_PlayerAlive = true;

        m_Tempmanager = GameObject.FindGameObjectWithTag("TemperatureManager").GetComponent<TemperatureManager>();
    }

    public void TogglePauseMenu()
    {
        // not the optimal way but for the sake of readability
        if (GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[0].enabled)
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[0].enabled = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[0].enabled = true;
            Time.timeScale = 0f;
        }

        //Debug.Log("GAMEMANAGER:: TimeScale: " + Time.timeScale);
    }

    public void ToggleDeathMenu()
    {
        // not the optimal way but for the sake of readability
        if (GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].enabled)
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].enabled = false;
            Time.timeScale = 1.0f;
            RestartLevel();
        }
        else
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].enabled = true;
            Time.timeScale = 0f;
        }
    }

    public void ToggleWinMenu()
    {
        if (GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[3].enabled)
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[3].enabled = false;
            Time.timeScale = 1.0f;

        }
        else
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[3].enabled = true;
            Time.timeScale = 0f;
        }
    }

    public void ToggleTutorial(string tutorialDialog)
    {
        if (tutorialDialog != "")
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[4].enabled = true;
        }
        else
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[4].enabled = false;
        }
        GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[4].GetComponentInChildren<Text>().text = tutorialDialog;
    }

    public Player GetPlayer()
    {
        return GameManager.GetPlayer();
    }

    public Player.State GetPlayerState()
    {
        return GameManager.GetPlayer().GetState();
    }
    
    public void HeatUpRoom()
    {
        m_Tempmanager.ChangeRoomTemp(25.0f);
    }

    public void CoolDownRoom()
    {
        m_Tempmanager.ChangeRoomTemp(-25.0f);
    }

    public void RestartLevel()
    {
        Application.LoadLevel("Game Scene");
    }

    public bool IsPlayerAlive()
    {
        return m_PlayerAlive;
    }

    public void KillPlayer()
    {
        m_PlayerAlive = false;
    }

    public void HeatUpPlayer()
    {
        if (m_Tempmanager.m_PlayerTemperature < 50)
        {
            m_Tempmanager.SetPlayerTemp(50);
        }
        else if (m_Tempmanager.m_PlayerTemperature > 50)
        {
            m_Tempmanager.ChangePlayerTemp(-1.0f * Time.deltaTime);
        }
    }

    public void CoolDownPlayer()
    {
        if (m_Tempmanager.m_PlayerTemperature > -30)
        {
            m_Tempmanager.SetPlayerTemp(-30);
        }
        else if (m_Tempmanager.m_PlayerTemperature < -30)
        {
            m_Tempmanager.ChangePlayerTemp(1.0f * Time.deltaTime);
        }

    }
}
