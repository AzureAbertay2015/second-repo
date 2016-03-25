using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsScript : MonoBehaviour
{
    public Canvas m_OptionsCanvas, m_MenuCanvas;
    public GameObject m_NextObject;
    EventSystem m_EventSystem;

    void Start()
    {
        m_MenuCanvas.enabled = true;
        m_OptionsCanvas.enabled = false;

        for (int i = 0; i < m_MenuCanvas.transform.childCount; i++)
        {
            m_MenuCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < m_OptionsCanvas.transform.childCount; i++)
        {
            m_OptionsCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        m_EventSystem = GameObject.FindGameObjectWithTag("Event System").GetComponent<EventSystem>(); 
    }

	public void GoToOptions()
    {
        //enable options canvas and children
        m_OptionsCanvas.enabled = true;

        for (int i = 0; i < m_OptionsCanvas.transform.childCount; i++)
        {
            m_OptionsCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }

        foreach (Button button in m_OptionsCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }

        foreach (Dropdown dropdown in m_OptionsCanvas.GetComponentsInChildren<Dropdown>())
        {
            dropdown.interactable = true;
        }

        foreach (Toggle toggle in m_OptionsCanvas.GetComponentsInChildren<Toggle>())
        {
            toggle.interactable = true;
        }

        //disable menu canvas and children
        m_MenuCanvas.enabled = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            m_MenuCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        foreach (Button button in m_MenuCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }

        //change event system selected
        m_EventSystem.SetSelectedGameObject(m_NextObject);
    }
}
