using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ObjectSelectorScript : MonoBehaviour {

    private EventSystem m_EventSystem;
    public GameObject m_Current;

	// Use this for initialization
	void Start ()
    {
        m_EventSystem = GameObject.FindGameObjectWithTag("Event System").GetComponent<EventSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_Current = m_EventSystem.currentSelectedGameObject;
	}
}
