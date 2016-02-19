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

    private PhysicMaterial[] m_pPhysicMaterials;


    [SerializeField]
    private float m_MovePower = 1.5f; // The force added to the player to move it.
    [SerializeField]
    private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

    [SerializeField]
    private float m_ForceMultiplierSolid = 1.0f;
    [SerializeField]
    private float m_ForceMultiplierLiquid = 0.5f;
    [SerializeField]
    private float m_ForceMultiplierGas = 0.1f;

    [SerializeField]
    private float m_GravityForceSolid = 2.0f;
    [SerializeField]
    private float m_GravityForceLiquid = 0.5f;
    [SerializeField]
    private float m_GravityForceGas = 0.1f;

    [SerializeField]
    private float m_MaxAngularVelocitySolid = 15f;
    [SerializeField]
    private float m_MaxAngularVelocityLiquid = 25f;
    [SerializeField]
    private float m_MaxAngularVelocityGas = 15f;


    public float m_JumpPower = 20; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.

    private Rigidbody m_Rigidbody;
    private SphereCollider m_SphereCollider;
    private ParticleSystem m_GasParticleSystem;
    private PlayerModel m_PlayerModel;

    private RaycastHit m_GroundEntityData;
    private bool m_bOnGround;

    private void LoadPlayerResources()
    {
        GameObject o;
    
        m_pPhysicMaterials = new PhysicMaterial[3];
        m_pPhysicMaterials[0] = Resources.Load(SOLID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pPhysicMaterials[1] = Resources.Load(LIQUID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pPhysicMaterials[2] = Resources.Load(GAS_PHYSIC_MATERIAL) as PhysicMaterial;

        o = Instantiate(Resources.Load(PLAYER_MODEL_PREFAB)) as GameObject;
        m_PlayerModel = o.GetComponent<PlayerModel>();
        m_PlayerModel.SetHostPlayer(this);
        m_PlayerModel.InitPlayerModel();
    }

    private void InitPlayer()
    {
        LoadPlayerResources();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_SphereCollider = GetComponent<SphereCollider>();
        m_GasParticleSystem = GetComponent<ParticleSystem>();
        
        ChangeState(State.Solid);
              
        // Don't use gravity, use our own force.
        m_Rigidbody.useGravity = false;

        // Ensure our tag is always Player!
        gameObject.tag = "Player";
    }
       

    private void Awake()
    {
        InitPlayer();
    }

    private void Start()
    {
          //InitPlayer();
    }
    
    private void Update()
    {
    }   

    private void UpdateOnGround()
    {

        Ray ray = new Ray(transform.localPosition, -Vector3.up);
       
        m_bOnGround = Physics.Raycast(ray, out m_GroundEntityData);

    }

    public void Move(Vector3 moveDirection, bool jump)
    {

        float power = m_MovePower;
        float gravity = 0.0f;

        if ( GetState() == State.Solid )
        {
            power *= m_ForceMultiplierSolid;
            gravity = m_GravityForceSolid;
        }
        else if ( GetState() == State.Liquid )
        {
            power *= m_ForceMultiplierLiquid;
            gravity = m_GravityForceLiquid;
        }
        else
        {
            power *= m_ForceMultiplierGas;
            gravity = m_GravityForceGas;
        }

        UpdateOnGround();

        // Otherwise add force in the move direction.
        m_Rigidbody.AddForce(moveDirection * power);

        if (m_bOnGround)
            Debug.Log("Ground Normal is " + m_GroundEntityData.normal);

        m_Rigidbody.AddForce(-Vector3.up * gravity);

        
        
       // Debug.DrawRay(transform.position, -Vector3.up * k_GroundRayLength, Color.red, 1.0f, false);

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

    public int GetLayerForState( Player.State state )
    {
        switch (m_State)
        {
            case State.Solid:
               return 9;
                //Debug.Log("Layer changed to: " + player.gameObject.layer);
                break;
            case State.Liquid:
               return 10;// water
                                      //Debug.Log("Layer changed to: " + player.gameObject.layer);
                break;
            case State.Gas:
                return 11;
                //Debug.Log("Layer changed to: " + player.gameObject.layer);
                break;
            default:
                Debug.Assert(false); // This should never happen!
                break;
        }

        return 0;
    }

    private void SetupLayer()
    {
        gameObject.layer = GetLayerForState(m_State);
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
                // Set the maximum angular velocity.
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocitySolid;
                break;
            case State.Liquid:
                //SetMesh(m_pLiquidMesh);
                //SetMaterial(m_LiquidMaterial);
                m_State = State.Liquid;
                m_GasParticleSystem.enableEmission = false;
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocityLiquid;
                break;
            case State.Gas:
               // SetMesh(m_pGasMesh);
                //SetMaterial(m_GasMaterial);
                m_State = State.Gas;
                m_GasParticleSystem.enableEmission = true;
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocityGas;
                break;

            default:
                break;
        }
             
        m_SphereCollider.sharedMaterial = m_pPhysicMaterials[(int)state];
        m_SphereCollider.enabled = false;
        m_SphereCollider.enabled = true;

        m_PlayerModel.SetState(m_State);

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
