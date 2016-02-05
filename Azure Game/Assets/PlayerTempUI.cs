using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTempUI : MonoBehaviour {

    private Text m_Playertemptext;
    public PlayerControls m_Playercontrols;
    public GameManager m_Gamemanager;
    int m_Playertemp;


	// Use this for initialization
	void Start () {

        m_Playertemptext = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {

        // if (playerControls.GetState() == 0)
        // {
        //     m_stateText.text = "Solid";
        // }
        //if (playerControls.GetState() == 1)
        // {
        //     m_stateText.text = "Liquid";
        // }
        //if (playerControls.GetState() == 2)
        // {
        //     m_stateText.text = "Gas";
        // }

        m_Playertemp = (int)m_Gamemanager.m_Playertemp;
        m_Playertemptext.text = m_Playertemp.ToString() + "C";

    }
}
