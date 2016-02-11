using UnityEngine;
using System.Collections;

public class RestartLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextScene(string scene_string)
	{
		Application.LoadLevel(scene_string);
		Time.timeScale = 1.0f;
	}
}
