using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTempUI : MonoBehaviour {

    private Text m_Playertemptext;

	// Use this for initialization
	void Start () {

        m_Playertemptext = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        
        m_Playertemptext.text = ((int)GameManager.GetTemperatureManager().m_Playertemperature).ToString() + "C";

    }
}
