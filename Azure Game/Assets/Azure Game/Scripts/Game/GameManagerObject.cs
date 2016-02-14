using UnityEngine;

// Object used to setup the GameManager global class.
// This is necessary since the GameManager instantiates prefabs which is only legal in the main thread.
// We ensure we are in the main thread by using Awake() and Start().

public class GameManagerObject : MonoBehaviour {
    
    void Awake()
    {
        GameManager.LevelLoadBegin();
    }

    // Use this for initialization
    void Start () {
        GameManager.LevelLoadFinish();
        Debug.LogWarning("Start!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
