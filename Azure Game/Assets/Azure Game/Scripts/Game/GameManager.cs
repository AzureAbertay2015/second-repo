using UnityEngine;

/* The GameManager is a global accessor to the worker units of the game.

    Currently they are:

    GameRules - Does the game logic.
    UIManager - Does UI work.
    Player    - A global accessor to the player on the current scene. Requires a designer to place the player prefab in the level!

    The game requires a GameManagerObject to be placed in the level to instantiate the required prefabs. The references will all be NULL if that isn't done.

    Refactored from the original GameManager (now GameRules) by Peter McKeown on 12/02/2016.

*/

class GameManager
{
    private const string PLAYER_NAME = "Player"; // Player tag name
    private const string UIMANAGER_NAME = "UIManager"; // UI Manager prefab name.
    private const string GAMERULES_NAME = "GameRules"; // GameRules prefab name.

    // Static references
    private static Player g_pPlayer = null;
    private static UIManager g_pUIManager = null;
    private static GameRules g_pGameRules = null;

<<<<<<< HEAD
	public enum PlayerState { Solid, Liquid, Gas };
	public enum Temperature { Cold, Warm, Hot };

	public PlayerState m_State;
	public Temperature m_Temperature;
	public Text m_TemperatureText;
	private bool m_PlayerAlive;

    public float m_Roomtemp;
    public float m_Playertemp;


    //----------------------------------------
    // handles
    public UIManager UI;
	public Player player;

    //-----------------------------------------
    // function definitions
    void Start() {
		//player = GetComponent<Player>();
		m_State = PlayerState.Solid;
		ChangeLayer();
		player.ChangeState(0);
		m_Temperature = Temperature.Warm;
		m_TemperatureText.text = "Warm";
		m_PlayerAlive = true;

        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
	}

    public void TogglePauseMenu()
=======
    public static Player GetPlayer()
>>>>>>> refs/remotes/origin/master
    {
        // Instead of asserting, print a user friendly message for designers!
        if (g_pPlayer == null)
        {
            Debug.LogError("No global player found, did you forget to add a Player or GameManagerObject object to the scene?");
        }
       
        return g_pPlayer;

    }

    public static UIManager GetUIManager()
    {
        // Instead of asserting, print a user friendly message for designers!
        if (g_pUIManager == null)
        {
            Debug.LogError("No UI Manager found, did you forget to add a GameManager object to the scene?");
        }

        return g_pUIManager;
    }

    public static GameRules GetGameRules()
    {
        // Instead of asserting, print a user friendly message for designers!
        if (g_pGameRules == null)
        {
            Debug.LogError("No GameRules found, did you forget to add a GameManager object to the scene?");
        }

        return g_pGameRules;
    }

    // This is called by GameManagerObject's Awake() - No scene objects are available at this time.
    public static void LevelLoadBegin()
    {
        // Initialise managers.

        GameObject o;
        o = Object.Instantiate(Resources.Load(UIMANAGER_NAME)) as GameObject;
        g_pUIManager = o.GetComponent<UIManager>();

        o = Object.Instantiate(Resources.Load(GAMERULES_NAME)) as GameObject;
        g_pGameRules = o.GetComponent<GameRules>();
    }

    // This is called by GameManagerObject's Start() - Scene objects are available.
    public static void LevelLoadFinish()
    {
        // Grab the player from the scene.

        g_pPlayer = GameObject.FindGameObjectWithTag(PLAYER_NAME).GetComponent<Player>();

        // Is this critical? We might not have a player on a main menu scene for example.
        // LogWarning it for now in case a designer forgets in an actual level. - PeterM

        if (g_pPlayer == null)
            Debug.LogWarning("No player found in scene!");
    }

<<<<<<< HEAD
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

    void Update()
    {
      

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

	public void ChangeLayer()
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
        //switch (m_State)
        //{
        //	case PlayerState.Solid:
        //		m_State = PlayerState.Liquid;
        //		player.ChangeState(1);
        //		break;
        //	case PlayerState.Liquid:
        //		m_State = PlayerState.Gas;
        //		player.ChangeState(2);
        //		break;
        //}
        //ChangeLayer();

        if(m_Playertemp == m_Roomtemp)
        {
            m_Playertemp += 30;
        }
         
    }
	
	public void CoolDownPlayer()
	{
        //player.GetComponent<PlayerControls>().LowerState();
        //switch (m_State)
        //{
        //	case PlayerState.Gas:
        //		m_State = PlayerState.Liquid;
        //		player.ChangeState(1);
        //		break;
        //	case PlayerState.Liquid:
        //		m_State = PlayerState.Solid;
        //		player.ChangeState(0);
        //		break;
        //}
        //ChangeLayer();

        if(m_Playertemp == m_Roomtemp)
        {
            m_Playertemp -= 30;
        }
        
	}
=======
>>>>>>> refs/remotes/origin/master
}