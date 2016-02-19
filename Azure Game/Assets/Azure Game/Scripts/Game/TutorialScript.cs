using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TutorialScript : MonoBehaviour {
    
    public String m_tutorialDialog;
    private bool m_triggeredDialog;
    
    void Start()
    {
        m_triggeredDialog = false;
    }

    //called when an object collides with this
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !m_triggeredDialog)
        {
            GameManager.GetGameRules().ToggleTutorial(m_tutorialDialog);
            m_triggeredDialog = true;   
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && m_triggeredDialog == true)
        {
            m_tutorialDialog = "";
            GameManager.GetGameRules().ToggleTutorial(m_tutorialDialog);
        }
    }
}
