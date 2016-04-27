using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public bool activated = false;
    public static GameObject[] m_Checkpoints;
    private Vector3 result;
    private ParticleSystem m_CheckpointParticles;

    // Use this for initialization
    void Start () {

        //Find the checkpoints in the scene
        m_Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        //Get the particle system for the checkpoint
        m_CheckpointParticles = GetComponent<ParticleSystem>();

    }

    void Awake()
    {

    }

   public void setResult(Vector3 pos)
    {
        //Set result to players initial position
        result = pos;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void ActivateCheckpoint()
    {
        //Disable all other checkpoints
        foreach (GameObject checkpt in m_Checkpoints)
        {
            checkpt.GetComponent<Checkpoint>().activated = false;
        }

        activated = true;

        //Disable particle system
        m_CheckpointParticles.enableEmission = false;

    }

    void OnTriggerEnter(Collider collider)
    {
        //If player collided with checkpoint, activate the checkpoint
        if (collider.tag == "Player")
        {
            ActivateCheckpoint();
        }
    }

    public Vector3 GetActiveCheckPoints()
    {
        //Find the position of the active checkpoint (if no checkpoints are active player's start position is returned)
        if (m_Checkpoints != null)
        {
            foreach(GameObject checkpt in m_Checkpoints)
            {
                if(checkpt.GetComponent<Checkpoint>().activated)
                {
                    result = checkpt.transform.position;
                    break;
                }
            }
        }
  
        return result;
    }

    public GameObject[] GetCheckpoints()
    {
        return m_Checkpoints;
    }

    public void SetCheckpoints(GameObject[] m_Checkpointlist)
    {
        m_Checkpoints = m_Checkpointlist;
    }
}
