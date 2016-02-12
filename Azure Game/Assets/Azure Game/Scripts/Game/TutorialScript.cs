using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TutorialScript : MonoBehaviour {
    
    public String m_tutorialString;
    
    //called when an object collides with this
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.GetGameRules().ToggleTutorial(m_tutorialString);
            Debug.Log("Tutorial triggered");
        }
    }
}
