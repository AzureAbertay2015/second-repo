using UnityEngine;
using System.Collections;

public class VentScript : MonoBehaviour {

	public VentScript m_OtherVent;
	private Vector3 m_ApparitionPosition;
	public float m_ApparitionDistance;

	// Use this for initialization
	void Start () {
		m_ApparitionPosition = transform.position;
		m_ApparitionPosition += Vector3.Scale(transform.forward, new Vector3(m_ApparitionDistance, m_ApparitionDistance, m_ApparitionDistance));
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Gas"))
                TeleportToOtherVent();
		}
	}

	public Vector3 GetApparitionPosition()
	{
		return m_ApparitionPosition;
	}

	private void TeleportToOtherVent()
	{
		// teleport player to the other vent
		GameManager.GetPlayer().transform.position = m_OtherVent.m_ApparitionPosition;

		// keep the velocity, rotate it to the exit's forward axis
		float magnitude = GameManager.GetPlayer().GetComponent<Rigidbody>().velocity.magnitude;
		GameManager.GetPlayer().GetComponent<Rigidbody>().velocity = m_OtherVent.transform.forward * magnitude;
	}
}
