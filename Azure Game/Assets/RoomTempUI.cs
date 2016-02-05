using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomTempUI : MonoBehaviour {

    private Text m_Roomtemptext;
    public GameManager m_Gamemanager;
    int m_Roomtemp;

	// Use this for initialization
	void Start () {

        m_Roomtemptext = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

        m_Roomtemp = (int)m_Gamemanager.m_Roomtemp;
        m_Roomtemptext.text = m_Roomtemp.ToString() + "C";
	
	}
}
