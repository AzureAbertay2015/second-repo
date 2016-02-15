using UnityEngine;

public class PullScript : MonoBehaviour {

	public float m_PullForce;
	public enum PullerType { Solid, Liquid, Gas };
	public PullerType m_PullerType;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			switch (m_PullerType)
			{
				case PullerType.Solid:
					if (GameManager.GetPlayer().GetState() == Player.State.Solid)
						other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * m_PullForce, ForceMode.Force);
					break;
				case PullerType.Liquid:
					if (GameManager.GetPlayer().GetState() == Player.State.Liquid)
						other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * m_PullForce, ForceMode.Force);
					break;
				case PullerType.Gas:
					if (GameManager.GetPlayer().GetState() == Player.State.Gas)
						other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * m_PullForce, ForceMode.Force);
					break;
			}
		}
	}
}
