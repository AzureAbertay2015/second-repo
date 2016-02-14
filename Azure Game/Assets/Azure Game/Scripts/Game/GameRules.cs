using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRules : MonoBehaviour {

    public enum PlayerState { Solid, Liquid, Gas };
    public enum Temperature { Cold, Warm, Hot };

    public Temperature m_Temperature;
    //public Text m_TemperatureText;
    //public Text m_TemperatureText;
    private bool m_PlayerAlive;
    
    //----------------------------------------
    // handles
 
    //-----------------------------------------
    // function definitions
    void Start()
    {
        //player = GetComponent<Player>();
        //m_State = PlayerState.Liquid;
        GameManager.GetPlayer().ChangeState(Player.State.Liquid);
        m_Temperature = Temperature.Warm;
        //m_TemperatureText.text = "Warm"; // Adam needs to fix this -PeterM
        m_PlayerAlive = true;
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
        GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[4].enabled = true;
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

    public void ChangeState(int state)
    {
      
    }

    public void ChangeTemperature(Temperature t)
    {
        m_Temperature = t;
        switch (t)
        {
            case Temperature.Cold:
                //m_TemperatureText.text = "Cold";
                break;
            case Temperature.Warm:
                //m_TemperatureText.text = "Warm";
                break;
            case Temperature.Hot:
                //m_TemperatureText.text = "Hot";
                break;
            default:
                //m_TemperatureText.text = "Sample Text";
                break;
        }
    }

    public void HeatUpRoom()
    {
        if (m_Temperature == Temperature.Cold) ChangeTemperature(Temperature.Warm);
        else if (m_Temperature == Temperature.Warm) ChangeTemperature(Temperature.Hot);
    }

    public void CoolDownRoom()
    {
        if (m_Temperature == Temperature.Hot) ChangeTemperature(Temperature.Warm);
        else if (m_Temperature == Temperature.Warm) ChangeTemperature(Temperature.Cold);
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
         GameManager.GetPlayer().RaiseState();
    }

    public void CoolDownPlayer()
    {
          GameManager.GetPlayer().LowerState();
    }
       
}
