using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Defines

    const string SOLID_MODEL = "RollerBall"; //"CubePrototype02x02x02";
    const string LIQUID_MODEL = "RollerBall";
    const string GAS_MODEL = "RollerBall";

    const string SOLID_MATERIAL = "Black Grid";
    const string LIQUID_MATERIAL = "Blue";
    const string GAS_MATERIAL = "Green";

    const string SOLID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerSolidPhysics";
    const string LIQUID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerLiquidPhysics";
    const string GAS_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerGasPhysics";

    const string PLAYER_MODEL_PREFAB = "PlayerModel";

    const string PLAYER_TAG = "Player";

    // Temperature states
    
    public enum State { Solid, Liquid, Gas };
    public State m_State;
    private State m_PreviousState;

    private Mesh m_pSolidMesh;
    private Mesh m_pLiquidMesh;
    private Mesh m_pGasMesh;

    private Mesh[] m_pMeshes;
    private Material[] m_pMaterials;
    private PhysicMaterial[] m_pPhysicMaterials;

/*
    private Material m_SolidMaterial;
    private Material m_LiquidMaterial;
    private Material m_GasMaterial;

    private PhysicMaterial m_pSolidPhysicMaterial;
    private PhysicMaterial m_pLiquidPhysicMaterial;
    private PhysicMaterial m_pGasPhysicMaterial;
*/
            
    [SerializeField]
    private float m_MovePower = 10; // The force added to the player to move it.
    [SerializeField]
    private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    
    public float m_JumpPower = 20; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.

    private Rigidbody m_Rigidbody;
    private SphereCollider m_SphereCollider;
    private ParticleSystem m_GasParticleSystem;
    private PlayerModel m_PlayerModel;
   
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
       // m_pMeshes[2] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[2] = Resources.Load(GAS_MATERIAL) as Material;
        m_pPhysicMaterials[2] = Resources.Load(GAS_PHYSIC_MATERIAL) as PhysicMaterial;

        o = Instantiate(Resources.Load(PLAYER_MODEL_PREFAB)) as GameObject;
        m_PlayerModel = o.GetComponent<PlayerModel>();
        m_PlayerModel.SetHostPlayer(this);

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

    }

    private void InitPlayer()
    {
        LoadPlayerResources();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_SphereCollider = GetComponent<SphereCollider>();
        m_GasParticleSystem = GetComponent<ParticleSystem>();
        
        ChangeState(State.Solid);
              
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;

        // Ensure our tag is always Player!
        gameObject.tag = "Player";
    }
        
    private void SetMesh(Mesh target_mesh)
    {

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

    }

    private void SetMaterial(Material target_material)
    {
        GetComponent<MeshRenderer>().material = target_material;
    }

    private void Awake()
    {
        InitPlayer();
    }

    private void Start()
    {
          InitPlayer();
    }
    
    private void Update()
    {
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

                //SetMesh(m_pSolidMesh);
                //SetMaterial(m_SolidMaterial);
                m_State = State.Solid;
                m_GasParticleSystem.enableEmission = false;
                break;
            case State.Liquid:
                //SetMesh(m_pLiquidMesh);
                //SetMaterial(m_LiquidMaterial);
                m_State = State.Liquid;
                m_GasParticleSystem.enableEmission = false;
                break;
            case State.Gas:
               // SetMesh(m_pGasMesh);
                //SetMaterial(m_GasMaterial);
                m_State = State.Gas;
                m_GasParticleSystem.enableEmission = true;
                break;

            default:
                break;
        }


        GetComponent<MeshFilter>().mesh = m_pMeshes[(int)state];
        GetComponent<MeshRenderer>().material = m_pMaterials[(int)state];
              
        m_SphereCollider.sharedMaterial = m_pPhysicMaterials[(int)state];
        m_SphereCollider.enabled = false;
        m_SphereCollider.enabled = true;

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
