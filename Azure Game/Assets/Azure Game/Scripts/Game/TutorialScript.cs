using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TutorialScript : MonoBehaviour {
    
    [SerializeField]
    public String m_tutorialString;
    public GameManager m_gameManager;

	// Use this for initialization
	void Start ()
    {
	}

    // Update is called once per frame
    void DoActivateTrigger(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_gameManager.ToggleTutorial(m_tutorialString);
        }
    }
}
