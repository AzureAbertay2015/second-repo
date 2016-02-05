using UnityEngine;
using System.Collections;

public class RaycastButton : MonoBehaviour {
    /*
	// Use this for initialization
	void Start () {
	
	}
	*/
    // Update is called once per frame
   public float distanceToButton = 0;
    void Update () {
        RaycastHit hit;
        

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            distanceToButton = hit.distance;
        }
    }
    

    

    
}
