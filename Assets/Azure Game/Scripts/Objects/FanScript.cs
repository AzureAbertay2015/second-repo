using UnityEngine;
using System.Collections;

public class FanScript : MonoBehaviour {
	public float m_FanForce;
	private GameRules m_GameRules;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (GameManager.GetPlayer().GetState() == Player.State.Gas)
			{
				other.gameObject.GetComponent<Rigidbody>().AddForce(GetComponentInParent<Transform>().up * m_FanForce);
			}			
		}
		//Debug.Log(other.gameObject.tag);
	}
}
