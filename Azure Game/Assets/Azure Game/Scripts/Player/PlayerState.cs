using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerState : MonoBehaviour {

    public Text m_SpeedText;
    public Text m_TimeText;
    public Text m_PointsText;
    public Text m_ChargeText;
    public Text m_PuzzleText;

    private int m_Speed;
    private int m_Time;
    private int m_Points;
    private int m_Charge;
    private int m_PuzzlePieces;


    void Start()
    {
        m_Speed = 50;
        m_Time = 50;
        m_Points = 0;
        m_Charge = 0;
        m_PuzzlePieces = 0;

        SetUIStrings();
    }

    void Update()
    {
        SetUIStrings();
    }

    private void SetUIStrings()
    {
        m_SpeedText.text = "Speed: " + m_Speed;
        m_TimeText.text = "Time: " + m_Time;
        m_PointsText.text = "Points: " + m_Points;
        m_ChargeText.text = "Charge: " + m_Charge;
        m_PuzzleText.text = "Puzzle: " + m_PuzzlePieces;
    }

    public void SpeedUp()
    {
        // increase player speed
        m_Speed += 5;
    }
    
    public void SpeedDown()
    {
        // decrease player speed
        m_Speed -= 5;
    }

    public void Time()
    {
        // Add time to the clock
        m_Time += 10;
    }

    public void PointsUp()
    {
        // add points to the score
        m_Points += 1;
    }

    public void PointsDown()
    {
        // add points to the score
        m_Points -= 1;
    }

    public void Charge()
    {
        // Add charge to the bar
        m_Charge += 10;
    }

    public void Puzzle()
    {
        // collect a puzzle piece
        m_PuzzlePieces += 1;
    }

}
