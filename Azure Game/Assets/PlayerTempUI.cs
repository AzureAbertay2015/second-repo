using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTempUI : MonoBehaviour {

    private Text m_Playertemptext;
    public TemperatureManager m_Tempmanager;
    int m_Playertemp;


	// Use this for initialization
	void Start () {

        m_Playertemptext = GetComponent<Text>();

        m_Tempmanager = GameObject.FindGameObjectWithTag("TemperatureManager").GetComponent<TemperatureManager>();

    }
	
	// Update is called once per frame
	void Update () {

        m_Playertemp = (int)m_Tempmanager.m_Playertemp;
        m_Playertemptext.text = m_Playertemp.ToString() + "C";

    }
}
