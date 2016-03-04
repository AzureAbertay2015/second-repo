using UnityEngine;
using System.Collections;

// General purpose state changing class that can be inherited by any object
// Based upon Peter M's Player and PlayerModel Classes

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
        m_State = State.Solid;
        m_PreviousState = State.Solid;

        GetComponent<MeshFilter>().mesh = m_pSolidMesh;
        GetComponent<MeshRenderer>().material = m_SolidMaterial;
    }

    void Start()
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

    public void ChangeState(State state)
    {
        if (state < State.Solid)
            state = State.Solid;

        if (state > State.Gas)
            state = State.Gas;

        switch (state)
        {
            case State.Solid:
                GetComponent<MeshFilter>().mesh = m_pSolidMesh;
                GetComponent<MeshRenderer>().material = m_SolidMaterial;
                m_State = State.Solid;
                break;
            case State.Liquid:
                GetComponent<MeshFilter>().mesh = m_pLiquidMesh;
                GetComponent<MeshRenderer>().material = m_LiquidMaterial;
                m_State = State.Liquid;
                break;
            case State.Gas:
                GetComponent<MeshFilter>().mesh = m_pGasMesh;
                GetComponent<MeshRenderer>().material = m_GasMaterial;
                m_State = State.Gas;
                break;

            default:
                break;
        }

        SetupLayer();

        //call any instance specific code
        OnChangeState(state);
    }

    public virtual void OnChangeState(State state)
    {

    }

    public State GetState()
    {
        return m_State;
    }
}
