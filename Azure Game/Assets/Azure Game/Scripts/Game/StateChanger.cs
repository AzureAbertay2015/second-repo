using UnityEngine;
using System.Collections;

// General purpose state changing class that can be inherited by any object
// Based upon Peter M's Player and PlayerModel Classes

public class StateChanger : MonoBehaviour {

    public float m_LiquidGasCutoff, m_Temperature;
    [HideInInspector]
    public float m_SolidLiquidCutoff, m_PrevTemperature;

    // Temperature states

    public enum State { Solid, Liquid, Gas };
    public State m_State;

    public Mesh m_pSolidMesh;
    public Mesh m_pLiquidMesh;
    public Mesh m_pGasMesh;

    public Material m_SolidMaterial;
    public Material m_LiquidMaterial;
    public Material m_GasMaterial;

    [HideInInspector]
    public bool m_Triggered;

    protected Renderer m_Renderer;

    protected void LoadResources()
    {
        m_State = State.Solid;

        GetComponent<MeshFilter>().mesh = m_pSolidMesh;

        //m_SolidLiquidCutoff = m_LiquidGasCutoff;

        m_SolidLiquidCutoff = 10.0f;

        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material = m_SolidMaterial;

        if (tag == "Untagged")
        {
            tag = "State Changer";
        }
    }

    void Start()
    {
        LoadResources();
    }

    void Awake()
    {
        //GameManager.GetTemperatureManager().AddStateChanger(this);
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
                m_Renderer.material = m_SolidMaterial;
                m_State = State.Solid;
                break;
            case State.Liquid:
                GetComponent<MeshFilter>().mesh = m_pLiquidMesh;
                m_Renderer.material = m_LiquidMaterial;
                m_State = State.Liquid;
                break;
            case State.Gas:
                GetComponent<MeshFilter>().mesh = m_pGasMesh;
                m_Renderer.material = m_GasMaterial;
                m_State = State.Gas;
                break;

            default:
                break;
        }

        SetupLayer();

        //call any instance specific code
        OnChangeState(state);
    }

    //virtual function that enacts child specific state changing code
    public virtual void OnChangeState(State state)
    {

    }

    public State GetState()
    {
        return m_State;
    }
}
