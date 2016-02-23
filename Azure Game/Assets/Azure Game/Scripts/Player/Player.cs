using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Defines
    [SerializeField]
    const string SOLID_MODEL = "CubePrototype02x02x02";
    const string LIQUID_MODEL = "CubePrototype02x02x02";
    const string GAS_MODEL = "CubePrototype02x02x02";

    const string SOLID_MATERIAL = "Black Grid";
    const string LIQUID_MATERIAL = "Blue";
    const string GAS_MATERIAL = "Green";

    const string PLAYER_TAG = "Player";

    // Temperature states    
    public enum State { Solid, Liquid, Gas };
    public State m_State;
    private State m_PreviousState;

    public Mesh m_pSolidMesh;
    public Mesh m_pLiquidMesh;
    public Mesh m_pGasMesh;

    private Material m_SolidMaterial;
    private Material m_LiquidMaterial;
    private Material m_GasMaterial;
        
    [SerializeField]
    private float m_MovePower = 10; // The force added to the player to move it.
    [SerializeField]
    private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    
    public float m_JumpPower = 20; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody m_Rigidbody;
    
    private void LoadPlayerResources()
    { 
        GameObject o;
        m_State = State.Solid;
        m_PreviousState = State.Solid;

        /*o = Instantiate(Resources.Load(SOLID_MODEL)) as GameObject;
        m_pSolidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

        o = Instantiate(Resources.Load(LIQUID_MODEL)) as GameObject;
        m_pLiquidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

        o = Instantiate(Resources.Load(GAS_MODEL)) as GameObject;
        m_pGasMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);*/

        m_SolidMaterial = Resources.Load(SOLID_MATERIAL) as Material;
        m_LiquidMaterial = Resources.Load(LIQUID_MATERIAL) as Material;
        m_GasMaterial = Resources.Load(GAS_MATERIAL) as Material;

        SetMesh(m_pSolidMesh);
        SetMaterial(m_SolidMaterial);

    }

    private void InitPlayer()
    {
        LoadPlayerResources();
        
        m_Rigidbody = GetComponent<Rigidbody>();
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;

        // Ensure our tag is always Player!
        gameObject.tag = "Player";
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
        InitPlayer();
    }


    public void Move(Vector3 moveDirection, bool jump)
    {
        // Otherwise add force in the move direction.
        m_Rigidbody.AddForce(moveDirection * m_MovePower);

        Debug.DrawRay(transform.position, -Vector3.up * k_GroundRayLength, Color.red, 1.0f, false);

        Vector3 vecStart = transform.position + Vector3.up * 2;

        bool bJump = Physics.Raycast(vecStart, -Vector3.up, k_GroundRayLength);

        //Debug.Log("bJump = " + bJump.ToString());

        // If on the ground and jump is pressed...
        if ( bJump && jump )
        {
            // ... add force in upwards.
            m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
           // Debug.Log("Jumping! " + m_Rigidbody.velocity.y );
            
        }
    }

    private void SetupLayer()
    {
        switch (m_State)
        {
            case State.Solid:
                gameObject.layer = 9;
                //Debug.Log("Layer changed to: " + player.gameObject.layer);
                break;
            case State.Liquid:
                gameObject.layer = 10;// water
                                           //Debug.Log("Layer changed to: " + player.gameObject.layer);
                break;
            case State.Gas:
                gameObject.layer = 11;
                //Debug.Log("Layer changed to: " + player.gameObject.layer);
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
