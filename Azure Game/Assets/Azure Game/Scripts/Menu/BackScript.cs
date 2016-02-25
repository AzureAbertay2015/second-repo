using UnityEngine;
using System.Collections;

public class BackScript : MonoBehaviour {

    public Canvas m_OptionsCanvas, m_MenuCanvas;

    public void GoToMenu()
    {
        m_OptionsCanvas.enabled = false;
        m_MenuCanvas.enabled = true;
    }
}
