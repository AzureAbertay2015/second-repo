using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopulateResolutionsScript : MonoBehaviour
{

    Dropdown m_Dropdown;
    // Use this for initialization
    void Start ()
    {
        m_Dropdown = GetComponent<Dropdown>();
        Resolution[] resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(ResToString(resolution)));
        }

        m_Dropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[m_Dropdown.value].width, resolutions[m_Dropdown.value].height, true); });
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
        //dropdown.GetComponentInChildren<Text>().text = "Resolution";
    }
}
