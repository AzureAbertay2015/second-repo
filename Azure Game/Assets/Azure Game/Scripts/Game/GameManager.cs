using UnityEngine;
using System.Collections;

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
    // Static references
    private static Player g_pPlayer = null;
    private static UIManager g_pUIManager = null;
    private static GameRules g_pGameRules = null;

    public static Player GetPlayer()
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
        o = Object.Instantiate(Resources.Load("UIManager")) as GameObject;
        g_pUIManager = o.GetComponent<UIManager>();

        o = Object.Instantiate(Resources.Load("GameRules")) as GameObject;
        g_pGameRules = o.GetComponent<GameRules>();
    }

    // This is called by GameManagerObject's Start() - Scene objects are available.
    public static void LevelLoadFinish()
    {
        // Grab the player from the scene.

        g_pPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Is this critical? We might not have a player on a main menu scene for example.
        // LogWarning it for now in case a designer forgets in an actual level.
        // - PeterM

        if (g_pPlayer == null)
            Debug.LogWarning("No player found in scene!");
    }

}