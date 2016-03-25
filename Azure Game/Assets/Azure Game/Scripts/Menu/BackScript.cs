using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackScript : MonoBehaviour {

    public Canvas m_OptionsCanvas, m_MenuCanvas;
    public GameObject m_NextObject;
    private EventSystem m_EventSystem;

    void Start()
    {
        m_EventSystem = GameObject.FindGameObjectWithTag("Event System").GetComponent<EventSystem>();
    }

    public void GoToMenu()
    {
        //disable options canvas and children
        m_OptionsCanvas.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            m_OptionsCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        foreach (Button button in m_OptionsCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }

        foreach (Dropdown dropdown in m_OptionsCanvas.GetComponentsInChildren<Dropdown>())
        {
            dropdown.interactable = false;
        }

        foreach (Toggle toggle in m_OptionsCanvas.GetComponentsInChildren<Toggle>())
        {
            toggle.interactable = false;
        }

        //enable menu canvas and children
        m_MenuCanvas.enabled = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            m_MenuCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < m_MenuCanvas.transform.childCount; i++)
        {
            m_MenuCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }

        foreach (Button button in m_MenuCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }

        //change event system selected
        m_EventSystem.SetSelectedGameObject(m_NextObject);
    }
}
