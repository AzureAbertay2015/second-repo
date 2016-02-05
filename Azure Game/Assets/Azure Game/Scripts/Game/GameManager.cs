using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public enum PlayerState { Solid, Liquid, Gas };
	public enum Temperature { Cold, Warm, Hot };

	public PlayerState m_State;
	public Temperature m_Temperature;
	//public Text m_TemperatureText;
	public Text m_TemperatureText;
	private bool m_PlayerAlive;
	

    //----------------------------------------
    // handles
    public UIManager UI;
	public Player player;

    //-----------------------------------------
    // function definitions
    void Start() {
		//player = GetComponent<Player>();
		m_State = PlayerState.Liquid;
		ChangeLayer();
		player.ChangeState(1);
		m_Temperature = Temperature.Warm;
		m_TemperatureText.text = "Warm";
		m_PlayerAlive = true;
	}

    public void TogglePauseMenu()
    {
        // not the optimal way but for the sake of readability
        if (UI.GetComponentsInChildren<Canvas>()[0].enabled)
        {
            UI.GetComponentsInChildren<Canvas>()[0].enabled = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            UI.GetComponentsInChildren<Canvas>()[0].enabled = true;
            Time.timeScale = 0f;
        }

        //Debug.Log("GAMEMANAGER:: TimeScale: " + Time.timeScale);
    }

	public void ToggleDeathMenu()
	{
		// not the optimal way but for the sake of readability
		if (UI.GetComponentsInChildren<Canvas>()[2].enabled)
		{
			UI.GetComponentsInChildren<Canvas>()[2].enabled = false;
			Time.timeScale = 1.0f;
			RestartLevel();
		}
		else
		{
			UI.GetComponentsInChildren<Canvas>()[2].enabled = true;
			Time.timeScale = 0f;			
		}
	}

	public void ToggleWinMenu()
	{
		if (UI.GetComponentsInChildren<Canvas>()[3].enabled)
		{
			UI.GetComponentsInChildren<Canvas>()[3].enabled = false;
			Time.timeScale = 1.0f;
			
		}
		else
		{
			UI.GetComponentsInChildren<Canvas>()[3].enabled = true;
			Time.timeScale = 0f;
		}
	}

	public void ChangeState(int state)
	{
		if (state >= 0)
		{
			switch (state)
			{
				case 0:
					m_State = PlayerState.Solid;
					break;
				case 1:
					m_State = PlayerState.Liquid;
					break;
				case 2:
					m_State = PlayerState.Gas;
					break;
				default:
					Debug.Log("state: " + state);
					break;
			}
		}
		ChangeLayer();
	}

	public void ChangeTemperature(Temperature t)
	{
		m_Temperature = t;
		switch (t)
		{
			case Temperature.Cold:
				m_TemperatureText.text = "Cold";
				break;
			case Temperature.Warm:
				m_TemperatureText.text = "Warm";
				break;
			case Temperature.Hot:
				m_TemperatureText.text = "Hot";
				break;
			default:
				m_TemperatureText.text = "Sample Text";
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

	private void ChangeLayer()
	{
		switch (m_State)
		{
			case PlayerState.Solid:
				player.gameObject.layer = 9;
				//Debug.Log("Layer changed to: " + player.gameObject.layer);
				break;
			case PlayerState.Liquid:
				player.gameObject.layer = 10;// water
				//Debug.Log("Layer changed to: " + player.gameObject.layer);
				break;
			case PlayerState.Gas:
				player.gameObject.layer = 11;
				//Debug.Log("Layer changed to: " + player.gameObject.layer);
				break;		
			default:
				player.gameObject.layer = 0; // default
				//Debug.Log("Layer changed to: " + player.gameObject.layer);
				break;
		}
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
		//player.GetComponent<PlayerControls>().RaiseState();
		switch (m_State)
		{
			case PlayerState.Solid:
				m_State = PlayerState.Liquid;
				player.ChangeState(1);
				break;
			case PlayerState.Liquid:
				m_State = PlayerState.Gas;
				player.ChangeState(2);
				break;
		}
		ChangeLayer();
	}
	
	public void CoolDownPlayer()
	{
		//player.GetComponent<PlayerControls>().LowerState();
		switch (m_State)
		{
			case PlayerState.Gas:
				m_State = PlayerState.Liquid;
				player.ChangeState(1);
				break;
			case PlayerState.Liquid:
				m_State = PlayerState.Solid;
				player.ChangeState(0);
				break;
		}
		ChangeLayer();
	}
}