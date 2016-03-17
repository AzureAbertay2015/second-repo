using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public GameObject Door;

    void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.gameObject.tag == "Player" || collision.collider.gameObject.tag == "State Changer" && collision.collider.gameObject.layer == LayerMask.NameToLayer("Solid"))
		{
			StartCoroutine(Door.GetComponent<DoorScript>().OpenUp());
			StartCoroutine(gameObject.GetComponent<DoorScript>().OpenUp());
		}
    }

	void OnCollisionExit(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player" || collision.collider.gameObject.tag == "State Changer")
		{
			StartCoroutine(gameObject.GetComponent<DoorScript>().Close());
		}
	}
}
