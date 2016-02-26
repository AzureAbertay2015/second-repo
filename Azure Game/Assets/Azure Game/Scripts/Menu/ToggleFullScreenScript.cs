using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleFullScreenScript : MonoBehaviour
{
    Toggle m_Toggle;

    // Use this for initialization
    public void Start()
    {
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate { ToggleFullScreen(); });
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = m_Toggle.isOn;
    }
}
