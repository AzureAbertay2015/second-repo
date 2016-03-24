using UnityEngine;

public class PullScript : MonoBehaviour {

	public float m_PullForce;
	public enum PullerType { Solid, Liquid, Gas };
	public PullerType m_PullerType;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "State Changer")
		{
			switch (m_PullerType)
			{
				case PullerType.Solid:
					if (other.gameObject.layer == LayerMask.NameToLayer("Solid"))
						other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * m_PullForce, ForceMode.Force);
					break;
				case PullerType.Liquid:
                    if (other.gameObject.layer == LayerMask.NameToLayer("Liquid"))
                        other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * m_PullForce, ForceMode.Force);
					break;
				case PullerType.Gas:
                    if (other.gameObject.layer == LayerMask.NameToLayer("Gas"))
                        other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * m_PullForce, ForceMode.Force);
					break;
			}
		}
	}
}
