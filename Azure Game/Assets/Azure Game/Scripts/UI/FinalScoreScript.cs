using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour {

    private Text m_scoreText;
    private bool m_LevelComplete;

	// Use this for initialization
	void Start () {

        m_scoreText = GetComponent<Text>();

        m_LevelComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
	
      if(m_LevelComplete)
        {
            //So that show score is not continuously called
            m_LevelComplete = false;
            ShowScore();
        }
	}

    private void ShowScore()
    {
        m_scoreText.text = "You Won! Your score is " + GameManager.GetGameRules().GetScoreScript().CalculateFinalScore();
    }

    public void LevelComplete()
    {
        m_LevelComplete = true;
    }
}