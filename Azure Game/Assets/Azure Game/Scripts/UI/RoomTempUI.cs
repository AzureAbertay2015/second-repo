using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomTempUI : MonoBehaviour {

    private Text m_Roomtemptext;
    int m_Roomtemp;

	// Use this for initialization
	void Start () {

        m_Roomtemptext = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        m_Roomtemp = (int)GameManager.GetTemperatureManager().m_RoomTemperature;
        m_Roomtemptext.text = m_Roomtemp.ToString() + "C";
	
	}
}
