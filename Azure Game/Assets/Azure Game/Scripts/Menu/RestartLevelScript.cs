using UnityEngine;
using System.Collections;
using System;

public class RestartLevelScript : MonoBehaviour {

    private Checkpoint m_Checkpoint;

    // Use this for initialization
    void Start () {

        //m_Checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();

    }

    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update () {
	
	}

	public void NextScene(string scene_string)
	{
        //Application.LoadLevel(Application.loadedLevelName);

        GameManager.GetGameRules().ToggleDeathMenu();
        
        GameManager.GetGameRules().RespawnPlayer();

        GameManager.GetPlayer().transform.localPosition = m_Checkpoint.GetActiveCheckPoints();

        Time.timeScale = 1.0f;

        GameManager.GetPlayer().GetComponent<Rigidbody>().velocity = Vector3.zero;
        
    }
}
