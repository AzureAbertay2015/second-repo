using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTempUI : MonoBehaviour {

    private Text m_Playertemptext;
    public GameManager m_Gamemanager;
    int m_Playertemp;


	// Use this for initialization
	void Start () {

        m_Playertemptext = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {

        m_Playertemp = (int)m_Gamemanager.m_Playertemp;
        m_Playertemptext.text = m_Playertemp.ToString() + "C";

    }
}
