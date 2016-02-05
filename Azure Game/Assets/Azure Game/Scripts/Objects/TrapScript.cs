using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

	public GameManager m_GameManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Debug.Log("Player collided with trap!");
			m_GameManager.KillPlayer();
		}
	}
}
