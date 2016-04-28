using UnityEngine;

// Purpose: The visual representation of the player.
// We do this so the player's visual rotation can be made independent of the physics model in Player.
public class PlayerModel : MonoBehaviour {
 

    private Player m_pHostPlayer;
    private ParticleSystem m_GasParticleSystem;
    private SphereCollider m_pClothGroundCollider;

    private struct stateChangeEffectData
    {
        public StateChanger.State fromState;
        public StateChanger.State toState;

        public float flStartTime;

        public bool bActive;

        public bool IsActive() { return bActive; }
    }

    private stateChangeEffectData m_StateChangeEffect;

    public void InitPlayerModel()
    {
        m_GasParticleSystem = GetComponent<ParticleSystem>();

       // m_pClothGroundCollider = GetComponent<Cloth>().sphereColliders[0].second;
        gameObject.layer = 30; // Layer 30 NOCOLLISION

        //m_GasParticleSystem.GetComponent<Renderer>().sortingLayerName = "PlayerGasParticles";
        //GetComponent<Renderer>().sortingLayerName = "PlayerGasParticles";
    }

    // This will change the mesh and material of the player to the parameters.
    // Specifying null for one will not change it.
    public void UpdateRenderableData(Mesh target_mesh, Material target_material, bool bRender)
    {

        GetComponent<MeshFilter>().mesh = target_mesh;
                       
        if ( target_material != null )
            GetComponent<Renderer>().material = target_material;

        GetComponent<Renderer>().enabled = bRender;
        //GetComponent<Cloth>().enabled = bRender;
    }

    // I was thinking about this for a while, we don't want too much logic for this
    // in the PlayerModel because doing special things will cause us to have to go back
    // and forth too much. The PlayerModel does what we say!
    public void SetEnableGasEffects( bool bEnabled )
    {
        //m_GasParticleSystem.enableEmission = bEnabled;

        m_GasParticleSystem.startSize = bEnabled ? 0.6f : 0.0f;
                
    }

    public void SetEnableWaterEffects( bool bEnabled )
    {
        GetComponent<Cloth>().enabled = bEnabled;

        // Any time our cloth state changed, clear the current motion.
        GetComponent<Cloth>().ClearTransformMotion();
    }
    
    public void StateChangeEffect( StateChanger.State fromState, StateChanger.State toState )
    {
        m_StateChangeEffect.fromState = fromState;
        m_StateChangeEffect.toState = toState;
        m_StateChangeEffect.flStartTime = Time.time;
        m_StateChangeEffect.bActive = true;

        StartStateChangeEffect();
    }
    
    private void StartStateChangeEffect()
    {
        switch (m_StateChangeEffect.fromState)
        {
            case StateChanger.State.Solid:
                {
                    StartStateChangeEffectFromSolid();
                    break;
                }

            case StateChanger.State.Liquid:
                {
                    StartStateChangeEffectFromLiquid();
                    break;
                }

            case StateChanger.State.Gas:
                {
                    StartStateChangeEffectFromGas();
                    break;
                }

            default:
                Debug.Assert(false);
                break;
        }
               

        m_StateChangeEffect.bActive = true;

    }

    private void UpdateStateChangeEffects()
    {

        switch (m_StateChangeEffect.fromState)
        {
            case StateChanger.State.Solid:
                {
                    UpdateStateChangeEffectFromSolid();
                    break;
                }

            case StateChanger.State.Liquid:
                {
                    UpdateStateChangeEffectFromLiquid();
                    break;
                }

            case StateChanger.State.Gas:
                {
                    UpdateStateChangeEffectFromGas();
                    break;
                }
        }

    }

    private void UpdateStateChangeEffectFromSolid()
    {
        if ( m_StateChangeEffect.toState == StateChanger.State.Liquid)
        {

        }
        else
        {

        }
    }
    private void UpdateStateChangeEffectFromLiquid()
    {
        if (m_StateChangeEffect.toState == StateChanger.State.Gas)
        {

        }
        else
        {

        }

    }


    private void UpdateStateChangeEffectFromGas()
    {
        if (m_StateChangeEffect.toState == StateChanger.State.Liquid)
        {
            // Turn off emitter
            //SetEnableGasEffects(false);
            //m_GasParticleSystem.enableEmission = false;
            GetComponent<Renderer>().enabled = false;
            SetEnableWaterEffects(false);

            bool bEffectDone = Time.time > m_StateChangeEffect.flStartTime + 5.5f;

            // Enumerate particles.
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[m_GasParticleSystem.maxParticles];
            int activeParticles = m_GasParticleSystem.GetParticles(particles);

            for (int i = 0; i < activeParticles; i++)
            {
                if (!bEffectDone)
                {
                    float timeAddition = Random.Range(0.0f, 0.15f);
                    particles[i].lifetime = 0.15f + timeAddition; //Time.time + 0.25f;
                    particles[i].startLifetime = 0.15f + timeAddition; //Time.time + 100.0f;
                }
                    

                particles[i].velocity = (m_pHostPlayer.transform.position - particles[i].position).normalized * (m_pHostPlayer.GetComponent<Rigidbody>().velocity.magnitude + 0.1f);
                particles[i].size = 1.50f - (m_pHostPlayer.transform.position - particles[i].position).magnitude;
            }

            m_GasParticleSystem.SetParticles(particles, activeParticles);

            

            // Begin lerping our scale back to normal.
            float curScale = Mathf.Lerp(transform.localScale.x, 1.0f, Time.deltaTime * 3);
            if (curScale > 0.9f)
                curScale = 1.0f;

            Debug.Log(curScale);
            transform.localScale = new Vector3(curScale, curScale, curScale);
            GetComponent<Cloth>().ClearTransformMotion();

            if (bEffectDone)
            {
                GetComponent<Renderer>().enabled = true;
            }
            
            if ( transform.localScale.x >= 1.0f )
            {
                m_StateChangeEffect.bActive = false;
                m_GasParticleSystem.enableEmission = true;
                SetEnableGasEffects(false);
            }


        }
        else
        {

        }

    }

    private void StartStateChangeEffectFromSolid()
    {


    }

    private void StartStateChangeEffectFromLiquid()
    {

    }

    private void StartStateChangeEffectFromGas()
    {
        GetComponent<Renderer>().enabled = false;
        SetEnableWaterEffects(false);

        // Become small!
        transform.localScale = new Vector3(0, 0, 0);
        GetComponent<Cloth>().ClearTransformMotion();

    }

    public void SetHostPlayer( Player pPlayer )
    {
        m_pHostPlayer = pPlayer;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if ( m_pHostPlayer != null )
        {
            transform.position = m_pHostPlayer.transform.position;
            //GetComponent<Rigidbody>().velocity = new Vector3( Random.Range(0, 1) * 10, Random.Range(0, 1) * 10, Random.Range(0, 1) * 10 ); //m_pHostPlayer.GetComponent<Rigidbody>().velocity;
            //m_pClothGroundCollider.center.Set(0, 0, m_pHostPlayer.IsOnGround() ? -0.75f : -2.0f);

            if (m_StateChangeEffect.IsActive())
                UpdateStateChangeEffects();
        }
	}
}
