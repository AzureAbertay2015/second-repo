using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Player : StateChanger {

    // Defines
    const string SOLID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerSolidPhysics";
    const string LIQUID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerLiquidPhysics";
    const string GAS_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerGasPhysics";

    const string PLAYER_MODEL_PREFAB = "PlayerModel";
    
    // Temperature states 
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
    private float m_GravityOnGroundFactor= 0.5f;

    [SerializeField]
    private float m_MaxAngularVelocitySolid = 15f;
    [SerializeField]
    private float m_MaxAngularVelocityLiquid = 25f;
    [SerializeField]
    private float m_MaxAngularVelocityGas = 15f;

    public float m_JumpPower = 20; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 2.5f; // The length of the ray to check if the ball is grounded.

    [SerializeField]
    private int m_TimerCount = 60;

    public float m_SpeedChangeAmount; // The Amount by which the forcemultiplier changes when speed pickup is consumed
    public int m_SpeedChangeDuration; // How long the speed change is active
    private bool m_SpeededUp;
    private bool m_SpeededDown;
    private float m_OrigForceMultiplierSolid;
    private float m_OrigForceMultiplierLiquid;
    private float m_OrigForceMultiplierGas;


    private Rigidbody m_Rigidbody;
    private SphereCollider m_SphereCollider;
   
    private RaycastHit m_GroundEntityData;
    private bool m_bOnGround;
    private int m_nCollisionCount;
    private Dictionary<int, Collision> m_CollisionTable;

    private Vector3 m_vecGroundNormal;

    private PlayerModel m_PlayerModel;

    private Checkpoint m_Checkpoint;

    private void LoadPlayerResources()
    {    
        m_pPhysicMaterials = new PhysicMaterial[3];
        m_pPhysicMaterials[0] = Resources.Load(SOLID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pPhysicMaterials[1] = Resources.Load(LIQUID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pPhysicMaterials[2] = Resources.Load(GAS_PHYSIC_MATERIAL) as PhysicMaterial;

        GameObject o;

        o = Instantiate(Resources.Load(PLAYER_MODEL_PREFAB)) as GameObject;
        m_PlayerModel = o.GetComponent<PlayerModel>();
        m_PlayerModel.SetHostPlayer(this);
        m_PlayerModel.InitPlayerModel();

        LoadResources();
        // Ensure our tag is always Player!
        tag = "Player";
    }

    private void InitPlayer()
    {
        LoadPlayerResources();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_SphereCollider = GetComponent<SphereCollider>();

        // Don't use gravity, use our own force.
        m_Rigidbody.useGravity = false;
        

        m_CollisionTable = new Dictionary<int, Collision>();

        m_vecGroundNormal.Set(0, 0, 0);

        m_Renderer.enabled = false;

        ChangeState(State.Solid);

        m_SpeededUp = false;
        m_SpeededDown = false;

        m_OrigForceMultiplierGas = m_ForceMultiplierGas;
        m_OrigForceMultiplierLiquid = m_ForceMultiplierLiquid;
        m_OrigForceMultiplierSolid = m_ForceMultiplierSolid;
    }
       

    void Awake()
    {
        InitPlayer();

        m_Checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();

        m_Checkpoint.setResult(transform.localPosition);

    }

    private bool IsValidCollision( Collision collision, int hash = -1 )
    {
        // Only flag a valid collision for objects with a z normal that isn't 0.
        // This filters out walls and other stuff that doesn't count as the ground entity.

        if ( hash != -1 )
        {
            // Get from hashtable (probably exit).
            m_CollisionTable.TryGetValue(hash, out collision);
                      
            Debug.Assert(collision.contacts.Length > 0, "Contact length 0, hash = " + hash + ".");
        }
     
        return (collision.contacts[0].normal.y > 0);

    }

    public void OnCollisionEnter( Collision collision )
    {

        // Debug.Assert(collision.contacts.GetLength(0) == 1);

        m_CollisionTable[collision.gameObject.GetInstanceID()] = collision;

        if (IsValidCollision(collision))
        {
            m_nCollisionCount++;

            if ( m_vecGroundNormal.sqrMagnitude == 0 )
            {
                m_vecGroundNormal = collision.contacts[0].normal;
            }

            //m_CollisionTable.Add(collision.gameObject.GetInstanceID(), collision);
        }
    }

    public void OnCollisionExit(Collision collision )
    {
        // Debug.Assert(collision.contacts.GetLength(0) == 1, "count = " + collision.contacts.GetLength(0));

        if (IsValidCollision(collision, collision.gameObject.GetInstanceID()))
        {
            m_nCollisionCount--;

            if (m_nCollisionCount == 0)
                m_vecGroundNormal.Set(0, 0, 0);
        }
    }

    
    public bool IsOnGround()
    {
        return (m_nCollisionCount > 0); //m_bOnGround;
    }

    private void Friction()
    {
        Vector3 vecVelocity = m_Rigidbody.velocity;

        if (vecVelocity.magnitude < 1.0f)
            return;


        float force = vecVelocity.magnitude * 10.0f;
        Vector3 vecFriction = -m_Rigidbody.velocity.normalized * force;

        m_Rigidbody.AddForce( vecFriction, ForceMode.Acceleration );
    }

    public void Move(Vector3 moveDirection, bool jump)
    {

        float power = m_MovePower;
        float gravity = 0.0f;
        float airGravFactor = IsOnGround() ? m_GravityOnGroundFactor : 1.0f;

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
     
        Vector3 dir = moveDirection.normalized;
        dir.y = 0;

        if (!IsOnGround())
            power = m_MovePower * m_ForceMultiplierGas;
     
        // Otherwise add force in the move direction.
        m_Rigidbody.AddForce(dir * power);
        
        if ( IsOnGround() && dir.magnitude == 0 )
            Friction();

        m_Rigidbody.AddForce(-Vector3.up * gravity);
        
        // If on the ground and jump is pressed...
        if ( IsOnGround() && jump )
        {
            Debug.Assert(m_vecGroundNormal.sqrMagnitude != 0);

            // ... add force in upwards.
            m_Rigidbody.AddForce(m_vecGroundNormal * m_JumpPower, ForceMode.Impulse);
           // Debug.Log("Jumping! " + m_Rigidbody.velocity.y );
            
        }
    }

    public override void OnChangeState(State state)
	{      
        switch (state)
        {
            case State.Solid:
                m_PlayerModel.SetEnableGasParticles(false);
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocitySolid;
                m_Renderer.enabled = true;
                break;
            case State.Liquid:
                m_PlayerModel.SetEnableGasParticles(false);
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocityLiquid;
                m_Renderer.enabled = true;
                break;
            case State.Gas:
                m_PlayerModel.SetEnableGasParticles(true);
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocityGas;
                m_Renderer.enabled = false;
                break;

            default:
                break;
        }
             
        m_SphereCollider.sharedMaterial = m_pPhysicMaterials[(int)state];
        m_SphereCollider.enabled = false;
        m_SphereCollider.enabled = true;

        // PeterM - Reset collision count since Unity seems to do this when we change physics material
        m_nCollisionCount = 0;

        m_PlayerModel.UpdateRenderableData(GetComponent<MeshFilter>().mesh, GetComponent<Renderer>().material);        

    }

    public void RaiseState()
    {
        ChangeState(++m_State);
    }

    public void LowerState()
    {
        ChangeState(--m_State);
    }
 
    public IEnumerator SpeedUp()
    {
        m_ForceMultiplierSolid *= m_SpeedChangeAmount;
        m_ForceMultiplierLiquid *= m_SpeedChangeAmount;
        m_ForceMultiplierGas *= m_SpeedChangeAmount;
        m_SpeededUp = true;
        StartCoroutine("PowerUpDisabler");
        yield return null;
    }

    public IEnumerator SpeedDown()
    {
        m_ForceMultiplierSolid /= m_SpeedChangeAmount;
        m_ForceMultiplierLiquid /= m_SpeedChangeAmount;
        m_ForceMultiplierGas /= m_SpeedChangeAmount;
        m_SpeededDown = true;
        StartCoroutine("PowerUpDisabler");
        yield return null;
    }

    private IEnumerator PowerUpDisabler()
    {
        yield return new WaitForSeconds(m_SpeedChangeDuration);
        m_ForceMultiplierGas = m_OrigForceMultiplierGas;
        m_ForceMultiplierLiquid = m_OrigForceMultiplierLiquid;
        m_ForceMultiplierSolid = m_OrigForceMultiplierSolid;
    }
    

}
