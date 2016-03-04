using UnityEngine;

// Object used to setup the GameManager global class.
// This is necessary since the GameManager instantiates prefabs which is only legal in the main thread.
// We ensure we are in the main thread by using Awake() and Start().

public class GameManagerObject : MonoBehaviour {

    public float m_Roomtemperature;
    public float m_Playertemperature;
    public float m_Temperaturechange;
    public float m_Abilitytemperaturechange;

    void Awake()
    {
        m_Roomtemperature = 20.0f;
        m_Playertemperature = -10.0f;
        m_Temperaturechange = 2.0f;
        m_Abilitytemperaturechange = 20.0f;

        GameManager.LevelLoadBegin(m_Roomtemperature, m_Playertemperature, m_Temperaturechange, m_Abilitytemperaturechange);
    }

    // Use this for initialization
    void Start () {

        GameManager.LevelLoadFinish();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
