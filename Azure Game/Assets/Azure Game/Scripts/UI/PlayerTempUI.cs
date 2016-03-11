using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTempUI : MonoBehaviour {

    private Text m_Playertemptext;
    private Player m_Player;

	// Use this for initialization
	void Start () {

        m_Playertemptext = GetComponent<Text>();

        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {        
        m_Playertemptext.text = ((int)m_Player.m_Temperature).ToString() + "C";
    }
}
