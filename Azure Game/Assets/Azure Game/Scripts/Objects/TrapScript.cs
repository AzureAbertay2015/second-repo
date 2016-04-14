using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

    public bool on;

	// Use this for initialization
	void Start ()
    {

        on = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	void OnCollisionEnter(Collision collision)
	{
        if (on)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player collided with trap!");
                GameManager.GetGameRules().KillPlayer();
            }
        }
	}
}
