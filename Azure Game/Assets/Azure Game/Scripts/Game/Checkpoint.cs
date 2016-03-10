﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public bool activated = false;
    public static GameObject[] m_Checkpoints;


	// Use this for initialization
	void Start () {

        m_Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void ActivateCheckpoint()
    {

        foreach(GameObject checkpt in m_Checkpoints)
        {
            checkpt.GetComponent<Checkpoint>().activated = false;
        }

        activated = true;

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            ActivateCheckpoint();
        }
    }

    public Vector3 GetActiveCheckPoints()
    {
        Vector3 result = new Vector3(0, 0, 0);

        if(m_Checkpoints != null)
        {
            foreach(GameObject checkpt in m_Checkpoints)
            {
                if(checkpt.GetComponent<Checkpoint>().activated)
                {
                    result = checkpt.transform.position;
                    break;
                }
            }
        }

        return result;
    }
}