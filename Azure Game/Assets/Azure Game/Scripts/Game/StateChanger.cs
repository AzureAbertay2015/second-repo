using UnityEngine;
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

    public Mesh[] m_pMeshes;
    private Material[] m_pMaterials;

    private void LoadResources()
    {
        m_State = State.Solid;
        m_PreviousState = State.Solid;

        SetMesh(m_pSolidMesh);
        SetMaterial(m_SolidMaterial);
    }

    public void InitModel()
    {
        // Initialise arrays (3 states currently)
        m_pMeshes = new Mesh[3];
        m_pMaterials = new Material[3];


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

    public void ChangeState(State state)
    {

        if (state < State.Solid)
            state = State.Solid;

        if (state > State.Gas)
            state = State.Gas;


        switch (state)
        {
            case State.Solid:

                //SetMesh(m_pSolidMesh);
                //SetMaterial(m_SolidMaterial);
                m_State = State.Solid;
                // Set the maximum angular velocity.
                break;
            case State.Liquid:
                //SetMesh(m_pLiquidMesh);
                //SetMaterial(m_LiquidMaterial);
                m_State = State.Liquid;
                break;
            case State.Gas:
                // SetMesh(m_pGasMesh);
                //SetMaterial(m_GasMaterial);
                m_State = State.Gas;
                break;

            default:
                break;
        }

        //m_PlayerModel.SetState(m_State);

        SetupLayer();

    }
    public void SetState(State state)
    {

        GetComponent<MeshFilter>().mesh = m_pMeshes[(int)state];
        GetComponent<MeshRenderer>().material = m_pMaterials[(int)state];

        Debug.Log("State = " + (int)state);

    }

    public State GetState()
    {
        return m_State;
    }
}
