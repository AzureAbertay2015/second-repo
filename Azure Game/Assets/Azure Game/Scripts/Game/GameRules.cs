﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRules : MonoBehaviour {

    public enum PlayerState { Solid, Liquid, Gas };
    
    private bool m_PlayerAlive;

    private Checkpoint m_Checkpoint;
            
    //----------------------------------------
    // handles
 
    //-----------------------------------------
    // function definitions
    void Start()
    {
        m_PlayerAlive = true;
        m_Checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();

        GameManager.GetPlayer().transform.localPosition = m_Checkpoint.GetActiveCheckPoints();
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
        GameManager.GetTemperatureManager().ChangeRoomTemp(25.0f);
    }

    public void CoolDownRoom()
    {
        GameManager.GetTemperatureManager().ChangeRoomTemp(-25.0f);
    }

    public void RestartLevel()
    {
        Application.LoadLevel("Game Scene");
        GameManager.GetPlayer().transform.localPosition = m_Checkpoint.GetActiveCheckPoints();
    }

    public bool IsPlayerAlive()
    {
        return m_PlayerAlive;
    }

    public void KillPlayer()
    {
        m_PlayerAlive = false;
    }

    public void HeatUpObject(StateChanger statechanger)
    {
        if(statechanger.m_Temperature < 50)
        {
            GameManager.GetTemperatureManager().SetObjectTemp(50, statechanger);
        }
        else if(GameManager.GetPlayer().m_Temperature > 50)
        {
            GameManager.GetTemperatureManager().SetObjectTemp(-1.0f * Time.deltaTime, statechanger);
        }
    }

    public void CoolDownObject(StateChanger statechanger)
    {
        if(GameManager.GetPlayer().m_Temperature > -30)
        {
            GameManager.GetTemperatureManager().SetObjectTemp(-30, statechanger);
        }
        else if(GameManager.GetPlayer().m_Temperature < -30)
        {
            GameManager.GetTemperatureManager().SetObjectTemp(1.0f * Time.deltaTime, statechanger);
        }       
    }
       
}
