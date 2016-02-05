using UnityEngine;
using System.Collections;

public class PipeScript : MonoBehaviour {

	public GameManager m_GameManager;
	private Vector3 m_EndPosition1;
	private Vector3 m_EndPosition2;
	private enum ClosestEnd { end1, end2 };
	private ClosestEnd m_ClosestEnd;


	// Use this for initialization
	void Start () {
		m_EndPosition1 = this.gameObject.transform.GetChild(2).transform.position;
		m_EndPosition1.x -= 1;
		m_EndPosition2 = this.gameObject.transform.GetChild(1).transform.position;
		m_EndPosition2.x += 1;
		m_ClosestEnd = ClosestEnd.end1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (m_GameManager.m_State == GameManager.PlayerState.Liquid)
			{
				//other.gameObject.transform.position = m_EndPosition;
				Debug.Log("Player piped");
				FindNearestEnd(other.transform.position);
				if (m_ClosestEnd == ClosestEnd.end1)
				{
					other.gameObject.transform.position = m_EndPosition2;
				}
				else
				{
					other.gameObject.transform.position = m_EndPosition1;
				}
			}
		}
	}

	private void FindNearestEnd(Vector3 playerPosition)
	{
		float dist1 = Vector3.Distance(playerPosition, m_EndPosition1);
		float dist2 = Vector3.Distance(playerPosition, m_EndPosition2);

		if (dist1 < dist2)
		{
			m_ClosestEnd = ClosestEnd.end1;
		}
		else
		{
			m_ClosestEnd = ClosestEnd.end2;
		}
	}
}
