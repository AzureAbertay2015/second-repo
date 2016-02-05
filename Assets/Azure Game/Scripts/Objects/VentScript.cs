using UnityEngine;
using System.Collections;

public class VentScript : MonoBehaviour {

	public VentScript m_OtherVent;
	public GameManager m_GameManager;
	public Player m_Player;
	private Vector3 m_ApparitionPosition;
	private Vector3 m_TempVel;

	// Use this for initialization
	void Start () {
		m_ApparitionPosition = transform.position;
		m_ApparitionPosition.z += 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (m_GameManager.m_State == GameManager.PlayerState.Gas)
				TeleportToOtherVent();
			//Invoke("TeleportToOtherVent", 0.1f);
		}
		//Debug.Log(other.gameObject.tag);
	}


	public Vector3 GetApparitionPosition()
	{
		return m_ApparitionPosition;
	}

	private void TeleportToOtherVent()
	{
		Debug.Log("Teleport to: " + m_OtherVent.m_ApparitionPosition);
		m_Player.transform.position = m_OtherVent.m_ApparitionPosition;
		m_TempVel = m_Player.GetComponent<Rigidbody>().velocity;
		m_TempVel.z = -m_TempVel.z;
		m_Player.GetComponent<Rigidbody>().velocity = m_TempVel;
	}
}
