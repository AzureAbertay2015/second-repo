using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScores : MonoBehaviour {

    private HighScore[] m_HighScores;
    private int temp;

	// Use this for initialization
	void Start () {

        m_HighScores = new HighScore[5];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CheckScore()
    {

        return false;
    }

    public void LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/highScores.dat"))
        {
            BinaryFormatter m_bf = new BinaryFormatter();
            FileStream m_File = File.Open(Application.persistentDataPath + "/highScores.dat", FileMode.Open);

            m_HighScores = (HighScore[])m_bf.Deserialize(m_File);
        }
        else
        {
            FileStream m_File = File.Create(Application.persistentDataPath + "/highScores.dat");
        }
    }

    public void SaveScores()
    {
        BinaryFormatter m_bf = new BinaryFormatter();
        FileStream m_File = File.Create(Application.persistentDataPath + "/highScores.dat");

        m_bf.Serialize(m_File, m_HighScores[0]);
        m_bf.Serialize(m_File, m_HighScores[1]);
        m_bf.Serialize(m_File, m_HighScores[2]);
        m_bf.Serialize(m_File, m_HighScores[3]);
        m_bf.Serialize(m_File, m_HighScores[4]);

        m_File.Close();
    }
}

[Serializable]
class HighScore
{
    string m_playerName;
    int m_Score;
}

