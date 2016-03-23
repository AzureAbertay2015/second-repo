using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public bool activated = false;
    public static GameObject[] m_Checkpoints;
    private Vector3 result;

    // Use this for initialization
    void Start () {

        m_Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

    }

    void Awake()
    {
        DontDestroyOnLoad(this);    
    }

   public void setResult(Vector3 pos)
    {
        result = pos;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void ActivateCheckpoint()
    {

        foreach(GameObject checkpt in m_Checkpoints)
        {
            checkpt.GetComponent<Checkpoint>().activated = false;
        }

        activated = true;

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            ActivateCheckpoint();
        }
    }

    public Vector3 GetActiveCheckPoints()
    {
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
