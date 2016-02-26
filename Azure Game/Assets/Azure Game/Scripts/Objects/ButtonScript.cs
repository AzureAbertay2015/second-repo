using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public GameObject Door;

    void OnCollisionEnter(Collision collision)
    {
		Debug.Log("asdf");
		if (collision.collider.gameObject.tag == "Player" && GameManager.GetPlayer().GetState() == Player.State.Solid)
		{
			StartCoroutine(Door.GetComponent<DoorScript>().OpenUp());
			StartCoroutine(gameObject.GetComponent<DoorScript>().OpenUp());
		}
    }

	void OnCollisionExit(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player")
		{
			StartCoroutine(gameObject.GetComponent<DoorScript>().Close());
		}
	}
}
