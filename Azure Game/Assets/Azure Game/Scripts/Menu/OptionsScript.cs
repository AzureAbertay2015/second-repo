using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour
{
    public Canvas m_OptionsCanvas, m_MenuCanvas;

	public void GoToOptions()
    {
        m_OptionsCanvas.enabled = true;
        m_MenuCanvas.enabled = false;
    }
}
