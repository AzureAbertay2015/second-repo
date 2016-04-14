using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScores : MonoBehaviour {

    private HighScore[] m_HighScores;
    private HighScore[] m_tempScore;
    private bool m_scoreChanged;
    public bool m_savedNewScore;

	// Use this for initialization
	public void Start () {

        m_HighScores = new HighScore[5];
        m_tempScore = new HighScore[2];

        m_scoreChanged = false;

        m_savedNewScore = false;

    }
	
	// Update is called once per frame
	public void Update () {

	}

    public void CheckScore(string m_name, int m_currentScore)
    {
        LoadScores();
       
        if(m_currentScore > m_HighScores[0].m_Score)
        {
            m_tempScore[0] = m_HighScores[0];
            m_HighScores[0].m_playerName = m_name;
            m_HighScores[0].m_Score = m_currentScore;

            m_tempScore[1] = m_HighScores[1];
            m_HighScores[1] = m_tempScore[0];

            m_tempScore[0] = m_HighScores[2];
            m_HighScores[2] = m_tempScore[1];

            m_tempScore[1] = m_HighScores[3];
            m_HighScores[3] = m_tempScore[0];

            m_HighScores[4] = m_tempScore[1];

            m_scoreChanged = true;
        }
        else if (m_currentScore > m_HighScores[1].m_Score)
        {
            m_tempScore[0] = m_HighScores[1];
            m_HighScores[1].m_playerName = m_name;
            m_HighScores[1].m_Score = m_currentScore;

            m_tempScore[1] = m_HighScores[2];
            m_HighScores[2] = m_tempScore[0];

            m_tempScore[0] = m_HighScores[3];
            m_HighScores[3] = m_tempScore[1];

            m_HighScores[4] = m_tempScore[0];

            m_scoreChanged = true;
        }
        else if (m_currentScore > m_HighScores[2].m_Score)
        {
            m_tempScore[0] = m_HighScores[2];
            m_HighScores[2].m_playerName = m_name;
            m_HighScores[2].m_Score = m_currentScore;

            m_tempScore[1] = m_HighScores[3];
            m_HighScores[3] = m_tempScore[0];
            
            m_HighScores[4] = m_tempScore[1];

            m_scoreChanged = true;
        }
        else if (m_currentScore > m_HighScores[3].m_Score)
        {
            m_tempScore[0] = m_HighScores[3];
            m_HighScores[3].m_playerName = m_name;
            m_HighScores[3].m_Score = m_currentScore;

            m_HighScores[4] = m_tempScore[0];

            m_scoreChanged = true;
        }
        else if (m_currentScore > m_HighScores[4].m_Score)
        {
            m_HighScores[4].m_playerName = m_name;
            m_HighScores[4].m_Score = m_currentScore;

            m_scoreChanged = true;
        }

        if(m_scoreChanged)
        {
            SaveScores();
    
        }

        m_savedNewScore = true;
    }

    public void LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/highScores.dat"))
        {
            BinaryFormatter m_bf = new BinaryFormatter();
            FileStream m_File = File.Open(Application.persistentDataPath + "/highScores.dat", FileMode.Open);

            //Debug.Log("OPENED FILE");

            m_HighScores = (HighScore[])m_bf.Deserialize(m_File);

            //Debug.Log("LOADED");

            m_File.Close();
        }
        else
        {
            FileStream m_File = File.Create(Application.persistentDataPath + "/highScores.dat");

            m_File.Close();

        }

        //Debug.Log(m_HighScores[0].m_playerName + " " + m_HighScores[0].m_Score);
        //Debug.Log(m_HighScores[1].m_playerName + " " + m_HighScores[1].m_Score);
        //Debug.Log(m_HighScores[2].m_playerName + " " + m_HighScores[2].m_Score);
        //Debug.Log(m_HighScores[3].m_playerName + " " + m_HighScores[3].m_Score);
        //Debug.Log(m_HighScores[4].m_playerName + " " + m_HighScores[4].m_Score);

    }

    public void SaveScores()
    {
        BinaryFormatter m_bf = new BinaryFormatter();

        File.Delete(Application.persistentDataPath + "/highScores.dat");

        FileStream m_File = File.Create(Application.persistentDataPath + "/highScores.dat");

        m_bf.Serialize(m_File, m_HighScores);

        m_File.Close();

        //Debug.Log("SAVED");
    }

    public HighScore[] GetScores()
    {
        LoadScores();

        return m_HighScores;
    }
}