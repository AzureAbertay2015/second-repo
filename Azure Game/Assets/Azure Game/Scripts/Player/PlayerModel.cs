using UnityEngine;
using System.Collections;

// Purpose: The visual representation of the player.
// We do this so the player's visual rotation can be made independent of the physics model in Player.
public class PlayerModel : MonoBehaviour {
        
    private Player m_pHostPlayer;
    private ParticleSystem m_GasParticleSystem;

    public void InitPlayerModel()
    {
        m_GasParticleSystem = GetComponent<ParticleSystem>();
    }

    // This will change the mesh and material of the player to the parameters.
    // Specifying null for one will not change it.
    public void UpdateRenderableData(Mesh target_mesh, Material target_material)
    {
        if ( target_mesh != null )
            GetComponent<MeshFilter>().mesh = target_mesh;

        if ( target_material != null )
             GetComponent<SkinnedMeshRenderer>().material = target_material;
    }

    // I was thinking about this for a while, we don't want too much logic for this
    // in the PlayerModel because doing special things will cause us to have to go back
    // and forth too much. The PlayerModel does what we say!
    public void SetEnableGasParticles( bool bEnabled )
    {
        m_GasParticleSystem.enableEmission = bEnabled;
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
        }
	}
}
