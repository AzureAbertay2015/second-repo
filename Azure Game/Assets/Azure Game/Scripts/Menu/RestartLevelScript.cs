using UnityEngine;
using System.Collections;
using System;

public class RestartLevelScript : MonoBehaviour {

    private Checkpoint m_Checkpoint;
    public static GameObject[] m_Checkpoints;

    // Use this for initialization
    void Start () {

        m_Checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();

    }

    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update () {
	
	}

	public void NextScene(string scene_string)
	{
        m_Checkpoints = m_Checkpoint.GetCheckpoints();

        Application.LoadLevel(scene_string);

        m_Checkpoint.SetCheckpoints(m_Checkpoints);

        GameManager.GetPlayer().transform.localPosition = m_Checkpoint.GetActiveCheckPoints();

        Time.timeScale = 1.0f;
	}
}
