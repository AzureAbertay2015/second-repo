using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour {

    private Text m_scoreText;
    private ScoreScript m_scoreScript;
    private bool m_LevelComplete;

	// Use this for initialization
	void Start () {

        m_scoreText = GetComponent<Text>();

        m_scoreScript = FindObjectOfType<ScoreScript>();

        m_LevelComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
	
      if(m_LevelComplete)
        {
            ShowScore();
        }
	}

    private void ShowScore()
    {
        m_scoreText.text = "You Won! Your score is " + m_scoreScript.CalculateFinalScore().ToString();
    }

    public void LevelComplete()
    {
        m_LevelComplete = true;
    }
}