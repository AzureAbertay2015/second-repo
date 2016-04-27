using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerState : MonoBehaviour {
    
    public Text m_TimeText;
    public Text m_PointsText;
    public Text m_ChargeText;
    public Text m_PuzzleText;
    
    private int m_Time;
    private int m_Points;
    private int m_Charge;
    private int m_PuzzlePieces;
    private int m_MaxPuzzlePieces;
    public int  m_TimePickup;

    private TimerScript m_Timer;
    private ScoreScript m_scoreScript;

    void Start()
    {
        
        m_Time = 50;
        m_TimePickup = 30;
        m_Points = 0;
        m_Charge = 0;
        m_PuzzlePieces = 0;

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Puzzle Piece"))
        {
            m_PuzzlePieces += 1;
        }
        m_MaxPuzzlePieces = m_PuzzlePieces;
        //SetUIStrings();
        StartCoroutine("CountDown");

    }

    void Update()
    {
        SetUIStrings();
        if (m_Time <= 0)
            StopCoroutine("CountDown");
    }

    void Awake()
    {
        m_Timer = FindObjectOfType<TimerScript>();
        m_scoreScript = GameManager.GetGameRules().GetScoreScript();
    }

    private void SetUIStrings()
    {
        //m_TimeText.text = "Time: " + m_Time;
        //m_PointsText.text = "Points: " + m_Points;
        //m_ChargeText.text = "Charge: " + m_Charge;
        //m_PuzzleText.text = "Puzzle: " + (m_MaxPuzzlePieces - m_PuzzlePieces) + "/" + m_MaxPuzzlePieces;
    }

    public void SpeedUp()
    {
        // increase player speed
        GameManager.GetPlayer().GetComponent<Player>().StartCoroutine("SpeedUp");
    }
    
    public void SpeedDown()
    {
        // decrease player speed
        GameManager.GetPlayer().GetComponent<Player>().StartCoroutine("SpeedDown");
    }

    public void Time()
    {
        // Add time to the clock
        m_Timer.addTime(m_TimePickup);
    }

    public void PointsUp()
    {
        // add points to the score
        m_Points += 1;

        m_scoreScript.AddtoScore(50);
    }

    public void PointsDown()
    {
        // add points to the score
        m_Points -= 1;

        m_scoreScript.TakeoffScore(50);
    }

    public void Charge()
    {
        // Add charge to the bar
        FindObjectOfType<EnergyBarScript>().StartCoroutine("ChargeUp");
    }

    public void Puzzle()
    {
        // collect a puzzle piece
        m_PuzzlePieces -= 1;

        m_scoreScript.AddtoScore(50);
    }

    private IEnumerator CountDown()
    {
        do
        {
            if (m_Time > 0)
                m_Time -= 1;

            yield return new WaitForSeconds(1);

        } while (m_Time > 0);
    }

}
