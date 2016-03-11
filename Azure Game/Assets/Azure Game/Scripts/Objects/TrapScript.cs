using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {
    public bool on;

    void Start()
    {
        on = true;
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
