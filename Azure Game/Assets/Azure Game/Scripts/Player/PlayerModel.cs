using UnityEngine;
using System.Collections;

// Purpose: The visual representation of the player.
// We do this so the player's visual rotation can be made independent of the physics model in Player.
public class PlayerModel : MonoBehaviour {
        
    private Player m_pHostPlayer;
    private ParticleSystem m_GasParticleSystem;
    private SphereCollider m_pClothGroundCollider;

    public void InitPlayerModel()
    {
        m_GasParticleSystem = GetComponent<ParticleSystem>();

       // m_pClothGroundCollider = GetComponent<Cloth>().sphereColliders[0].second;
        gameObject.layer = 30; // Layer 30 NOCOLLISION
        
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

        }
	}
}
