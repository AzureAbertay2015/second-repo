﻿using UnityEngine;
using System.Collections;

public class PlayerModel : MonoBehaviour {

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
    
    private Player m_pHostPlayer;

    private Mesh[] m_pMeshes;
    private Material[] m_pMaterials;
    
    public void InitPlayerModel()
    {
        GameObject o;

        // Initialise arrays (3 states currently)
        m_pMeshes = new Mesh[3];
        m_pMaterials = new Material[3];

        o = Instantiate(Resources.Load(SOLID_MODEL)) as GameObject;
        m_pMeshes[0] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[0] = Resources.Load(SOLID_MATERIAL) as Material;

        o = Instantiate(Resources.Load(LIQUID_MODEL)) as GameObject;
        m_pMeshes[1] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[1] = Resources.Load(LIQUID_MATERIAL) as Material;
       
        o = Instantiate(Resources.Load(GAS_MODEL)) as GameObject;
        // m_pMeshes[2] = o.GetComponent<MeshFilter>().mesh;
        m_pMaterials[2] = Resources.Load(GAS_MATERIAL) as Material;
       
    }

    private void SetMesh(Mesh target_mesh)
    {

    }

    private void SetMaterial(Material target_material)
    {
        GetComponent<MeshRenderer>().material = target_material;
    }

    public void SetState(Player.State state)
    {

       GetComponent<MeshFilter>().mesh = m_pMeshes[(int)state];
       GetComponent<MeshRenderer>().material = m_pMaterials[(int)state];

        Debug.Log("State = " + (int)state);

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