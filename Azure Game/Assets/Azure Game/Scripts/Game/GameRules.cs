using UnityEngine;
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

            GameManager.GetUIManager().GetComponentsInChildren<Button>()[1].interactable = false;
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].enabled = false;

            // PeterM - Let me tell you a story...
            // So, apparently making a Canvas enabled = false doesn't make the buttons stop receiving input.
            // This means if you click a button, it becomes "selected" and thus keyboard input can effect it whether the canvas is enabled or not.
            // This caused a bug where pressing spacebar after pressing restart level caused the restart button to be pressed even though it's invisible.
            // So, we set both buttons to not be interactable, disabling input for them, so this bug won't happen.

            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].GetComponentsInChildren<Button>()[0].interactable = false;
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].GetComponentsInChildren<Button>()[1].interactable = false;

            Time.timeScale = 1.0f;
            RestartLevel();
        }
        else
        {
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].enabled = true;
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].GetComponentsInChildren<Button>()[0].interactable = true;
            GameManager.GetUIManager().GetComponentsInChildren<Canvas>()[2].GetComponentsInChildren<Button>()[1].interactable = true;
            Time.timeScale = 0f;
        }
        Canvas.ForceUpdateCanvases();
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
        //Application.LoadLevel("Game Scene");
        //GameManager.GetPlayer().transform.localPosition = m_Checkpoint.GetActiveCheckPoints();
    }

    public bool IsPlayerAlive()
    {
        return m_PlayerAlive;
    }

    public void KillPlayer()
    {
        m_PlayerAlive = false;
    }

    public void RespawnPlayer()
    {
        m_PlayerAlive = true;
    }

    public void HeatUpObject(StateChanger statechanger)
    {
        if(GameManager.GetPlayer().m_Temperature < 50)
        {
            GameManager.GetTemperatureManager().SetObjectTemp(50, statechanger);
        }
        else if(GameManager.GetPlayer().m_Temperature > 50)
        {
            GameManager.GetTemperatureManager().ChangeObjectTemp(-1.0f * Time.deltaTime, statechanger);
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
            GameManager.GetTemperatureManager().ChangeObjectTemp(1.0f * Time.deltaTime, statechanger);
        }       
    }       
}
