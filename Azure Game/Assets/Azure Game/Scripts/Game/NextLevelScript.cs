using UnityEngine;
using System.Collections;

public class NextLevelScript : MonoBehaviour {

	public string m_sNextLevel; // String containing next level name

	// Use this for initialization
	void Start () {
	
	}
	
	void NextLevel()
	{
		Application.LoadLevel(m_sNextLevel);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			NextLevel();
		}
	}
}
