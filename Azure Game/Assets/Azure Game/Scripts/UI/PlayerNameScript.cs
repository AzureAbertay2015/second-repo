using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNameScript : MonoBehaviour {

    private Text m_playerName;
    private InputField m_input;
    bool m_enter;
    bool m_enabled;

	// Use this for initialization
	void Start ()
    {

        m_playerName = GetComponentInChildren<Text>();
        m_input = GetComponent<InputField>();

        m_enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
    {

        m_enter = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Submit");

        if (m_enter && m_enabled)
        {
            GameManager.GetGameRules().GetHighScores().CheckScore(m_playerName.text, GameManager.GetGameRules().GetScoreScript().GetScore());

            m_input.DeactivateInputField();

            m_enabled = false;
            
        }
	
	}

}