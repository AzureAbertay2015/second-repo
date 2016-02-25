using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopulateResolutionsScript : MonoBehaviour
{

    Dropdown dropdown;
    // Use this for initialization
    void Start ()
    {
        dropdown = GetComponent<Dropdown>();
        Resolution[] resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            dropdown.options.Add(new Dropdown.OptionData(ResToString(resolution)));
        }

        dropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdown.value].width, resolutions[dropdown.value].height, true); });
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
        //dropdown.GetComponentInChildren<Text>().text = "Resolution";
    }
}
