using UnityEngine;
using System.Collections;

public class PoolScript : StateChanger
{

    TrapScript m_TrapScript;
    BoxCollider m_BoxCollider;

    // Use this for initialization
    void Start()
    {
        LoadResources();
        m_TrapScript = GetComponent<TrapScript>();
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    public override void OnChangeState(State state)
    {
        if (state == State.Solid)
        {
            m_TrapScript.on = false;
            m_BoxCollider.isTrigger = false;
        }

        if (state == State.Liquid)
        {
            m_TrapScript.on = true;
            m_BoxCollider.isTrigger = false;
        }

        if (state == State.Gas)
        {
            m_TrapScript.on = false;
            m_BoxCollider.isTrigger = true;
        }
    }
}