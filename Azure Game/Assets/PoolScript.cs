using UnityEngine;
using System.Collections;

public class PoolScript : StateChanger {

    TrapScript m_TrapScript;
    BoxCollider m_BoxCollider;
    // Use this for initialization
    void Start ()
    {
        m_TrapScript = GetComponentInParent<TrapScript>();
        m_BoxCollider = GetComponentInParent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (m_State == State.Liquid)
        {
            m_TrapScript.enabled = true;
        }
        else
        {
            m_TrapScript.enabled = false;
        }

        if (m_State == State.Gas)
        {
            m_BoxCollider.isTrigger = true;
        }
        else
        {
            m_BoxCollider.isTrigger = false;
        }
	}
}
