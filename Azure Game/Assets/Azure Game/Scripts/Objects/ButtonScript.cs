using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public GameObject m_ToActivate = null;

    private float m_ButtonHeight;
    private float m_PressHeight;
    private float m_OriginalYPosition;
    private float m_PressedYPosition;

    private Vector3 m_PressedVector;
    private Vector3 m_DepressedVector;

    private bool m_Pressed;

    void Start()
    {
        m_Pressed = false;
        m_ButtonHeight = transform.localScale.y;
        m_PressHeight = m_ButtonHeight * 0.2f ;

        m_OriginalYPosition = transform.position.y;
        m_PressedYPosition = m_OriginalYPosition - m_PressHeight;

        m_PressedVector.Set(transform.position.x, m_PressedYPosition, transform.position.z);
        m_DepressedVector.Set(transform.position.x, transform.position.y, transform.position.z);
    }

    void OnCollisionStay(Collision collision)
    {
        if (!m_Pressed)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("State Changer"))
            {
                if (collision.gameObject.layer == 9)
                { // solid
                    StopCoroutine("DepressButton");
                    StartCoroutine("PressButton");
                    m_Pressed = true;
                }
            }
        }
    }

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("State Changer"))
		{
            StartCoroutine("CoolOffPeriod");            
		}
	}

    private IEnumerator PressButton()
    {
        for (float i = 0; i < 1.0f; i += 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_PressedVector, Time.deltaTime);
            if (Vector3.Distance(transform.position, m_PressedVector) <= 0.1)
            {
                transform.position = m_PressedVector;
                ActivateObject();
            }
            yield return null;
        }
    }

    private IEnumerator DepressButton()
    {
        StopCoroutine("CoolOffPeriod");
        for (float i = 0; i < 1.0f; i += 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_DepressedVector, Time.deltaTime);
            if (Vector3.Distance(transform.position, m_DepressedVector) <= 0.1f)
            {
                transform.position = m_DepressedVector;
            }
            yield return null;
        }
        m_Pressed = false;
    }

    private IEnumerator CoolOffPeriod()
    {
        yield return new WaitForSeconds(0.5f);
        StopCoroutine("PressButton");
        while (m_Pressed)
        {
            if (IsPlayerOffButton())
                StartCoroutine("DepressButton");
            yield return null;
        }
        
    }

    private bool IsPlayerOffButton()
    {
        bool off = false;
        float safeDistance = 1.25f;
        Vector3 buttonPosition = transform.position;
        Vector3 playerPosition = GameManager.GetPlayer().transform.position;
        if (Vector3.Distance(buttonPosition, playerPosition) > safeDistance)
            off = true;
        else
            off = false;
        return off;
    }

    private void ActivateObject()
    {
        if (m_ToActivate != null)
        {
            m_ToActivate.SendMessage("DoActivateTrigger");
        }
    }
}
