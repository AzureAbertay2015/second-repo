using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectAAScript : MonoBehaviour {

    Dropdown m_Dropdown;
    // Use this for initialization
    void Start()
    {
        m_Dropdown = GetComponent<Dropdown>();

        m_Dropdown.onValueChanged.AddListener(delegate { SetAntiAliasing(); });
    }

    void SetAntiAliasing()
    {
        QualitySettings.antiAliasing = m_Dropdown.value * 2;
    }
}
