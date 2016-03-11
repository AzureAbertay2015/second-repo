using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : StateChanger {

    // Defines
    const string SOLID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerSolidPhysics";
    const string LIQUID_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerLiquidPhysics";
    const string GAS_PHYSIC_MATERIAL = "PhysicsMaterials/PlayerGasPhysics";

    const string PLAYER_MODEL_PREFAB = "PlayerModel";

    const string PLAYER_TAG = "Player";

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

    private Rigidbody m_Rigidbody;
    private SphereCollider m_SphereCollider;
    private ParticleSystem m_GasParticleSystem;

    private RaycastHit m_GroundEntityData;
    private bool m_bOnGround;
    private int m_nCollisionCount;
    private Dictionary<int, Collision> m_CollisionTable;

    private Vector3 m_vecGroundNormal;

    private void LoadPlayerResources()
    {    
        m_pPhysicMaterials = new PhysicMaterial[3];
        m_pPhysicMaterials[0] = Resources.Load(SOLID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pPhysicMaterials[1] = Resources.Load(LIQUID_PHYSIC_MATERIAL) as PhysicMaterial;
        m_pPhysicMaterials[2] = Resources.Load(GAS_PHYSIC_MATERIAL) as PhysicMaterial;
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

        m_CollisionTable = new Dictionary<int, Collision>();

        m_vecGroundNormal.Set(0, 0, 0);
    }
       

    void Awake()
    {
        InitPlayer();
    }

    private bool IsValidCollision( Collision collision, int hash = -1 )
    {
        // Only flag a valid collision for objects with a z normal that isn't 0.
        // This filters out walls and other stuff that doesn't count as the ground entity.

        //Vector3 normal;

        if ( hash != -1 )
        {
            // Get from hashtable (probably exit).
            m_CollisionTable.TryGetValue(hash, out collision);
                 //Debug.LogError("Failed to get collision table entry! (hash = " + hash + ")");

            //m_CollisionTable.Remove(hash);
            Debug.Assert(collision.contacts.Length > 0, "Contact length 0, hash = " + hash + ".");
        }
        else
        {
            //normal = collision.contacts[0].normal;
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

    private void UpdateOnGround()
    {
        /*
        Vector3 startPosition = transform.localPosition;
        startPosition.z+=2;

        Ray ray = new Ray(startPosition, -Vector3.up);
       
        m_bOnGround = Physics.Raycast(ray, out m_GroundEntityData, k_GroundRayLength, 1 );
        */

        //m_bOnGround = Physics.CheckSphere(transform.localPosition, 0.75f, GetLayerForState(GetState()), QueryTriggerInteraction.Ignore); //Physics.SphereCast(transform.localPosition, 0.75f, -Vector3.up, out m_GroundEntityData, 1f, GetLayerForState(GetState()), QueryTriggerInteraction.Ignore);
        
        //if ( m_bOnGround )
         //Debug.DrawLine(transform.localPosition, m_GroundEntityData.transform.localPosition);

        //Debug.Assert(m_nCollisionCount >= 0);

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

        //Debug.Log("Friction: " + vecFriction.ToString());

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
        
        UpdateOnGround();

        Vector3 dir = moveDirection;

        if (!IsOnGround())
            power = m_MovePower * m_ForceMultiplierGas;
       //else
         //  dir += m_GroundEntityData.normal;


        dir.Normalize();

        dir.y = 0;

        Debug.DrawLine(transform.localPosition, transform.localPosition + dir * 20);

        // Otherwise add force in the move direction.
        m_Rigidbody.AddForce(dir * power);
              

        if ( IsOnGround() && dir.magnitude == 0 )
            Friction();

        /*
        if (IsOnGround())
            Debug.Log("On Ground. (n=" + m_nCollisionCount +")" );
        else
            Debug.Log("Not on ground.");
            */

        m_Rigidbody.AddForce(-Vector3.up * gravity);
        


        // If on the ground and jump is pressed...
        if ( IsOnGround() && jump )
        {
            Debug.Assert(m_vecGroundNormal.sqrMagnitude != 0);

            // ... add force in upwards.
            m_Rigidbody.AddForce(m_vecGroundNormal * m_JumpPower, ForceMode.Impulse);
            Debug.Log("Jump vector is " + m_vecGroundNormal.ToString() + ". (n=" + m_nCollisionCount + ").");
           // Debug.Log("Jumping! " + m_Rigidbody.velocity.y );
            
        }
    }

    public override void OnChangeState(State state)
	{      
        switch (state)
        {
            case State.Solid:               
                m_GasParticleSystem.enableEmission = false;
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocitySolid;
                break;
            case State.Liquid:
                m_GasParticleSystem.enableEmission = false;
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocityLiquid;
                break;
            case State.Gas:
                m_GasParticleSystem.enableEmission = true;
                m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocityGas;
                break;

            default:
                break;
        }
             
        m_SphereCollider.sharedMaterial = m_pPhysicMaterials[(int)state];
        m_SphereCollider.enabled = false;
        m_SphereCollider.enabled = true;

        // PeterM - Reset collision count since Unity seems to do this when we change physics material
        m_nCollisionCount = 0;
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
