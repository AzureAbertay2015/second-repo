using UnityEngine;
using System.Collections;

public class FanScript : MonoBehaviour {

	//public Vector3 m_FanForce;
	public float m_FanForce;
	private Player m_Player;
	private GameRules m_GameRules;

	// Use this for initialization
	void Start () {
		m_Player = GameManager.GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (m_Player.GetState() == Player.State.Gas)
			{
				other.gameObject.GetComponent<Rigidbody>().AddForce(GetComponentInParent<Transform>().up * m_FanForce);
			}			
		}
		//Debug.Log(other.gameObject.tag);
	}
}
