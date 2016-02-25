using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VSyncToggleScript : MonoBehaviour
{
    Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { ToggleVSync(); });
    }

	public void ToggleVSync()
    {
        if (toggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
