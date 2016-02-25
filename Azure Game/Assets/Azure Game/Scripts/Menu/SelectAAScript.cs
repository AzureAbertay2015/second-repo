using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectAAScript : MonoBehaviour {

    Dropdown dropdown;
    // Use this for initialization
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate { SetAntiAliasing(); });
    }

    void SetAntiAliasing()
    {
        QualitySettings.antiAliasing = dropdown.value * 2;
    }
}
