using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public GameObject Door;

    void DoActivateTrigger(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            StartCoroutine(Door.GetComponent<DoorScript>().OpenUp());
    }
}
