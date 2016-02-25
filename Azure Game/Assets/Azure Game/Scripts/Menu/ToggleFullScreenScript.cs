using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleFullScreenScript : MonoBehaviour
{
    Toggle toggle;

    // Use this for initialization
    public void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { ToggleFullScreen(); });
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = toggle.isOn;
    }
}
