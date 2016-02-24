using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopulateResolutionsScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Dropdown dropdown = GetComponent<Dropdown>();
        Resolution[] resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            dropdown.options[i].text = resolutions[i].width + " x " + resolutions[i].height;// ResToString(resolutions[i]);
            dropdown.value = i;

            dropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdown.value].width, resolutions[dropdown.value].height, true); });
        }
    }
    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}
