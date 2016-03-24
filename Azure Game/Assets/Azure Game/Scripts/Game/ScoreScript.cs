using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
    private int m_playerScore;
    private bool m_LevelComplete;
    private TimerScript m_Timer;
    private PlayerState m_pState;
    // Use this for initialization
    public void Start () {

        m_playerScore = 0;

        m_LevelComplete = false;

        m_Timer = GameManager.GetUIManager().gameObject.GetComponentInChildren<TimerScript>();
        m_pState = FindObjectOfType<PlayerState>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public int CalculateFinalScore()
    {
        if(m_Timer.getTimeSpent() < 120)
        {
            m_playerScore += ((int)(m_Timer.getTimeLeft())/3);
        }
        else if(m_Timer.getTimeSpent() < 180)
        {
            m_playerScore += ((int)(m_Timer.getTimeLeft())/2);
        }

        if (m_LevelComplete == true)
        {
            m_playerScore += 50;
        }

        return m_playerScore;
    }

    public void LevelFinised()
    {
        m_LevelComplete = true;
    }

    public void AddtoScore(int score)
    {
        m_playerScore += score;
    }

    public void TakeoffScore(int score)
    {
        m_playerScore -= score;
    }
}
