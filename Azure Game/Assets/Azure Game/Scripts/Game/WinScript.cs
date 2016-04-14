using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

    private ScoreScript m_scoreScript;
    private FinalScoreScript m_finalScoreScript;
    private bool m_Toggled;

	// Use this for initialization
	void Start ()
    {
		m_Toggled = false;
        m_finalScoreScript = GameManager.GetUIManager().gameObject.GetComponentInChildren<FinalScoreScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !m_Toggled)
		{
            GameManager.GetGameRules().GetScoreScript().LevelFinised();
            m_finalScoreScript.LevelComplete();
			GameManager.GetGameRules().ToggleWinMenu();
			m_Toggled = true;
		}
	}
}
