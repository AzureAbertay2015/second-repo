using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScores : MonoBehaviour {

    public Text m_score1;
    public Text m_score2;
    public Text m_score3;
    public Text m_score4;
    public Text m_score5;

    private HighScore[] m_HighScores;


    // Use this for initialization
    void Start ()
    {
        m_HighScores = new HighScore[5];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(GameManager.GetGameRules().GetHighScores().m_savedNewScore)
        {
            m_HighScores = GameManager.GetGameRules().GetHighScores().GetScores();

            m_score1.text = "1. " + m_HighScores[0].m_playerName + ": " + m_HighScores[0].m_Score;
            m_score2.text = "2. " + m_HighScores[1].m_playerName + ": " + m_HighScores[1].m_Score;
            m_score3.text = "3. " + m_HighScores[2].m_playerName + ": " + m_HighScores[2].m_Score;
            m_score4.text = "4. " + m_HighScores[3].m_playerName + ": " + m_HighScores[3].m_Score;
            m_score5.text = "5. " + m_HighScores[4].m_playerName + ": " + m_HighScores[4].m_Score;

            GameManager.GetGameRules().GetHighScores().m_savedNewScore = false;
        }
              
    }
}
