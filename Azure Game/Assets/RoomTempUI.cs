using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomTempUI : MonoBehaviour {

    private Text m_Roomtemptext;
    public TemperatureManager m_Tempmanager;
    int m_Roomtemp;

	// Use this for initialization
	void Start () {

        m_Roomtemptext = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

        m_Roomtemp = (int)m_Tempmanager.m_Roomtemp;
        m_Roomtemptext.text = m_Roomtemp.ToString() + "C";
	
	}
}
