using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VSyncToggleScript : MonoBehaviour
{
    Toggle m_Toggle;

    public void Start()
    {
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate { ToggleVSync(); });
    }

	public void ToggleVSync()
    {
        if (m_Toggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
