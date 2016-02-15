using UnityEngine;
using System.Collections;

public class FanScript : MonoBehaviour {
	public float m_FanForce;
<<<<<<< HEAD
	public Player m_Player;
	//public GameManager m_GameManager;
=======
	private GameRules m_GameRules;
>>>>>>> refs/remotes/origin/master

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
<<<<<<< HEAD
			//if (m_GameManager.m_State == GameManager.PlayerState.Gas)
            //if ( m_GameManager.GetPlayerState() == Player.State.Gas )
            if ( GameManager.GetGameRules().GetPlayerState() == Player.State.Gas )
=======
			if (GameManager.GetPlayer().GetState() == Player.State.Gas)
>>>>>>> refs/remotes/origin/master
			{
				other.gameObject.GetComponent<Rigidbody>().AddForce(GetComponentInParent<Transform>().up * m_FanForce);
			}			
		}
		//Debug.Log(other.gameObject.tag);
	}
}
