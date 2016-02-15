using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Defines
<<<<<<< HEAD
    const string SOLID_MODEL = "RollerBall"; //"CubePrototype02x02x02";
    const string LIQUID_MODEL = "RollerBall";
    const string GAS_MODEL = "RollerBall";
=======
    const string SOLID_MODEL = "CubePrototype02x02x02";
    const string LIQUID_MODEL = "CubePrototype02x02x02";
    const string GAS_MODEL = "CubePrototype02x02x02";
>>>>>>> refs/remotes/origin/master

    const string SOLID_MATERIAL = "Black Grid";
    const string LIQUID_MATERIAL = "Blue";
    const string GAS_MATERIAL = "Green";

<<<<<<< HEAD
    const string SOLID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerSolidPhysics";
    const string LIQUID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerLiquidPhysics";
    const string GAS_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerGasPhysics";
    
=======
>>>>>>> refs/remotes/origin/master
    const string PLAYER_TAG = "Player";

    // Temperature states
    
    public enum State { Solid, Liquid, Gas };
    public State m_State;
    private State m_PreviousState;

    private Mesh m_pSolidMesh;
    private Mesh m_pLiquidMesh;
    private Mesh m_pGasMesh;

<<<<<<< HEAD
    private Mesh[] m_pMeshes;
    private Material[] m_pMaterials;
    private PhysicMaterial[] m_pPhysicMaterials;

    private Material m_SolidMaterial;
    private Material m_LiquidMaterial;
    private Material m_GasMaterial;

    private PhysicMaterial m_pSolidPhysicMaterial;
    private PhysicMaterial m_pLiquidPhysicMaterial;
    private PhysicMaterial m_pGasPhysicMaterial;
=======
    private Material m_SolidMaterial;
    private Material m_LiquidMaterial;
    private Material m_GasMaterial;
>>>>>>> refs/remotes/origin/master
        
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

<<<<<<< HEAD
    private SphereCollider m_SphereCollider;

    private void LoadPlayerResources()
    { 
        GameObject o;
      
        // Initialise arrays (3 states currently)
        m_pMeshes = new Mesh[3];
        m_pMaterials = new Material[3];
        m_pPhysicMaterials = new PhysicMaterial[3];

        o = Instantiate(Resources.Load(SOLID_MODEL)) as GameObject;
        m_pMeshes[0] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[0] = Resources.Load(SOLID_MATERIAL) as Material;
        m_pPhysicMaterials[0] = Resources.Load(SOLID_PHYSIC_MATERIAL) as PhysicMaterial;

        o = Instantiate(Resources.Load(LIQUID_MODEL)) as GameObject;
        m_pMeshes[1] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[1] = Resources.Load(LIQUID_MATERIAL) as Material;
        m_pPhysicMaterials[1] = Resources.Load(LIQUID_PHYSIC_MATERIAL) as PhysicMaterial;

        o = Instantiate(Resources.Load(GAS_MODEL)) as GameObject;
        m_pMeshes[2] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[2] = Resources.Load(GAS_MATERIAL) as Material;
        m_pPhysicMaterials[2] = Resources.Load(GAS_PHYSIC_MATERIAL) as PhysicMaterial;


        /*
        o = Instantiate(Resources.Load(SOLID_MODEL)) as GameObject;
        m_pSolidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

=======
        o = Instantiate(Resources.Load(SOLID_MODEL)) as GameObject;
        m_pSolidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

>>>>>>> refs/remotes/origin/master
        o = Instantiate(Resources.Load(LIQUID_MODEL)) as GameObject;
        m_pLiquidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

        o = Instantiate(Resources.Load(GAS_MODEL)) as GameObject;
        m_pGasMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

        m_SolidMaterial = Resources.Load(SOLID_MATERIAL) as Material;
        m_LiquidMaterial = Resources.Load(LIQUID_MATERIAL) as Material;
        m_GasMaterial = Resources.Load(GAS_MATERIAL) as Material;
<<<<<<< HEAD
        

        m_pSolidPhysicMaterial = Resources.Load(SOLID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pLiquidPhysicMaterial = Resources.Load(LIQUID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pGasPhysicMaterial = Resources.Load(GAS_PHYSIC_MATERIAL) as PhysicMaterial;

        */

        //SetMesh(m_pSolidMesh);
        //SetMaterial(m_SolidMaterial);

        //ChangeState(State.Solid);
=======

        SetMesh(m_pSolidMesh);
        SetMaterial(m_SolidMaterial);
>>>>>>> refs/remotes/origin/master

    }

    private void InitPlayer()
    {
        LoadPlayerResources();

<<<<<<< HEAD
        m_Rigidbody = GetComponent<Rigidbody>();
        m_SphereCollider = GetComponent<SphereCollider>();

        ChangeState(State.Solid);
              
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
                
=======
        ChangeState(State.Solid);

        m_Rigidbody = GetComponent<Rigidbody>();
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;

>>>>>>> refs/remotes/origin/master
        // Ensure our tag is always Player!
        gameObject.tag = "Player";
    }
        
    private void SetMesh(Mesh target_mesh)
    {
<<<<<<< HEAD
       // GetComponent<MeshFilter>().mesh = target_mesh;
        // switch the collider

        /*
=======
        GetComponent<MeshFilter>().mesh = target_mesh;
        // switch the collider
>>>>>>> refs/remotes/origin/master
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
<<<<<<< HEAD
        */
=======
>>>>>>> refs/remotes/origin/master
    }

    private void SetMaterial(Material target_material)
    {
        GetComponent<MeshRenderer>().material = target_material;
    }

<<<<<<< HEAD
    private void Awake()
    {
        InitPlayer();
    }

    private void Start()
    {
        
=======
    private void Start()
    {
        InitPlayer();
>>>>>>> refs/remotes/origin/master
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

    public void ChangeState(State state)
	{

        if (state < State.Solid)
            state = State.Solid;

        if (state > State.Gas)
            state = State.Gas;
            

        switch (state)
        {
            case State.Solid:
<<<<<<< HEAD
                //SetMesh(m_pSolidMesh);
                //SetMaterial(m_SolidMaterial);
                m_State = State.Solid;
                break;
            case State.Liquid:
                //SetMesh(m_pLiquidMesh);
                //SetMaterial(m_LiquidMaterial);
                m_State = State.Liquid;
                break;
            case State.Gas:
                //SetMesh(m_pGasMesh);
                //SetMaterial(m_GasMaterial);
=======
                SetMesh(m_pSolidMesh);
                SetMaterial(m_SolidMaterial);
                m_State = State.Solid;
                break;
            case State.Liquid:
                SetMesh(m_pLiquidMesh);
                SetMaterial(m_LiquidMaterial);
                m_State = State.Liquid;
                break;
            case State.Gas:
                SetMesh(m_pGasMesh);
                SetMaterial(m_GasMaterial);
>>>>>>> refs/remotes/origin/master
                m_State = State.Gas;
                break;

            default:
                break;
        }

<<<<<<< HEAD
        GetComponent<MeshFilter>().mesh = m_pMeshes[(int)state];
        GetComponent<MeshRenderer>().material = m_pMaterials[(int)state];
               
        m_SphereCollider.sharedMaterial = m_pPhysicMaterials[(int)state];
        m_SphereCollider.enabled = false;
        m_SphereCollider.enabled = true;

=======
>>>>>>> refs/remotes/origin/master
        SetupLayer();

    }

    public State GetState()
    {
        return m_State;
    }

    public void RaiseState()
    {
        ChangeState(++m_State);
    }

    public void LowerState()
    {
        ChangeState(--m_State);
    }
 
}
