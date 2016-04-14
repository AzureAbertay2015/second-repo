using UnityEngine;

// Object used to setup the GameManager global class.
// This is necessary since the GameManager instantiates prefabs which is only legal in the main thread.
// We ensure we are in the main thread by using Awake() and Start().

[System.Serializable]
public struct HighScore
{
    public string m_playerName;
    public int m_Score;

}

public class GameManagerObject : MonoBehaviour {

    public float m_Roomtemperature;
    public float m_Temperaturechange;
    public float m_Abilitytemperaturechange;
    public float m_LevelLength; //in seconds

    void Awake()
    {
        //set level length if was not set
        if (m_Roomtemperature == 0)
        {
            m_Roomtemperature = 20.0f;
        }

        //set temperature change if was not set
        if (m_Temperaturechange == 0)
        {
            m_Temperaturechange = 2.0f;
        }

        //set ability temperature change if was not set
        if (m_Abilitytemperaturechange == 0)
        {
            m_Abilitytemperaturechange = 20.0f;
        }

        //set level length if was not set
        if (m_LevelLength == 0)
        {
            m_LevelLength = 180.0f;
        }

        GameManager.LevelLoadBegin(m_Roomtemperature, m_Temperaturechange, m_Abilitytemperaturechange, m_LevelLength);
    }

    // Use this for initialization
    void Start () {

        GameManager.LevelLoadFinish();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
