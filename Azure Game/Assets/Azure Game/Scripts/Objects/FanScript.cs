using UnityEngine;
using System.Collections;

public class FanScript : MonoBehaviour {
	public float m_FanForce;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Gas"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(GetComponentInParent<Transform>().up * m_FanForce);
        }	
	}
}
