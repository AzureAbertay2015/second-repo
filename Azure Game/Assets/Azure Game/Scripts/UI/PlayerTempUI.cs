using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTempUI : MonoBehaviour {

    private Text m_Playertemptext;
    private TemperatureManager m_Tempmanager;

	// Use this for initialization
	void Start () {

        m_Playertemptext = GetComponent<Text>();

        m_Tempmanager = GameObject.FindGameObjectWithTag("TemperatureManager").GetComponent<TemperatureManager>();

    }
	
	// Update is called once per frame
	void Update ()
    {        
        m_Playertemptext.text = ((int)m_Tempmanager.m_PlayerTemperature).ToString() + "C";
    }
}
