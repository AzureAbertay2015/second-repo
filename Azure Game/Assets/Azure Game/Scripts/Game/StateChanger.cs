﻿using UnityEngine;
using System.Collections;

public class StateChanger : MonoBehaviour {

    public float m_SolidLiquidCutoff, m_LiquidGasCutoff, m_Temperature;
    [HideInInspector]
    public float m_PrevTemperature;

    // Temperature states

    public enum State { Solid, Liquid, Gas };
    public State m_State;
    private State m_PreviousState;

    public Mesh m_pSolidMesh;
    public Mesh m_pLiquidMesh;
    public Mesh m_pGasMesh;

    public Material m_SolidMaterial;
    public Material m_LiquidMaterial;
    public Material m_GasMaterial;

    private void LoadResources()
    {
        GameObject o;
        m_State = State.Solid;
        m_PreviousState = State.Solid;

        SetMesh(m_pSolidMesh);
        SetMaterial(m_SolidMaterial);
    }

    private void SetMesh(Mesh target_mesh)
    {
        GetComponent<MeshFilter>().mesh = target_mesh;
        // switch the collider
        if (target_mesh == m_pSolidMesh)
        {
            GetComponents<BoxCollider>()[0].enabled = true;
            GetComponents<BoxCollider>()[1].enabled = false;
            GetComponents<BoxCollider>()[2].enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (target_mesh == m_pLiquidMesh)
        {
            GetComponents<BoxCollider>()[0].enabled = false;
            GetComponents<BoxCollider>()[1].enabled = true;
            GetComponents<BoxCollider>()[2].enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (target_mesh == m_pGasMesh)
        {
            GetComponents<BoxCollider>()[0].enabled = false;
            GetComponents<BoxCollider>()[1].enabled = false;
            GetComponents<BoxCollider>()[2].enabled = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void SetMaterial(Material target_material)
    {
        GetComponent<MeshRenderer>().material = target_material;
    }

    private void Start()
    {
        LoadResources();
    }

    private void SetupLayer()
    {
        switch (m_State)
        {
            case State.Solid:
                gameObject.layer = 9;
                break;
            case State.Liquid:
                gameObject.layer = 10;// water
                break;
            case State.Gas:
                gameObject.layer = 11;
                break;
            default:
                Debug.Assert(false); // This should never happen!
                break;
        }
    }

    public void ChangeState(int state)
    {
        switch (state)
        {
            case 0:
                SetMesh(m_pSolidMesh);
                SetMaterial(m_SolidMaterial);
                m_State = State.Solid;
                break;
            case 1:
                SetMesh(m_pLiquidMesh);
                SetMaterial(m_LiquidMaterial);
                m_State = State.Liquid;
                break;
            case 2:
                SetMesh(m_pGasMesh);
                SetMaterial(m_GasMaterial);
                m_State = State.Gas;
                break;

            default:
                break;
        }

        SetupLayer();
    }

    public State GetState()
    {
        return m_State;
    }
}
