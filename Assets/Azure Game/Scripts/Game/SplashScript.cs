using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {

    public float timer = 2.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
			Application.LoadLevel("Menu Scene");
        }
    }
}
