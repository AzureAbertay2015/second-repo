using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour {
    
    public float m_Roomtemp;
    public float m_Playertemp;

    public float m_Tempchange;

    private EnergyBarScript m_Energyscript;
    

	// Use this for initialization
	void Start () {
        
        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
        m_Tempchange = 2.0f;

        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        bool upArrow = Input.GetKeyDown(KeyCode.U);
        bool downArrow = Input.GetKeyDown(KeyCode.J);

        if (upArrow)
        {
            if (m_Energyscript.TempUp())
            {
                Debug.Log("temp before: " + m_Playertemp);
                m_Playertemp += 20.0f;
                Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (downArrow)
        {
            if (m_Energyscript.TempDown())
            {
                Debug.Log("temp before: " + m_Playertemp);
                m_Playertemp -= 20.0f;
                Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (m_Playertemp > m_Roomtemp)
        {
            m_Playertemp -= m_Tempchange * Time.deltaTime;
        }

        if (m_Playertemp < m_Roomtemp)
        {
            m_Playertemp += m_Tempchange * Time.deltaTime; ;
        }

        if (m_Playertemp > 40)
        {
            GameManager.GetPlayer().ChangeState(2);
        }

        else if (m_Playertemp > 10)
        {
            GameManager.GetPlayer().ChangeState(1);
        }

        else
        {
            GameManager.GetPlayer().ChangeState(0);
        }

    }

    public void ChangeRoomTemp(float t)
    {
        m_Roomtemp += t;
    }

    public void ChangePlayerTemp(float t)
    {
        m_Playertemp += t;

        if(t > 0 && m_Playertemp > (m_Roomtemp + t))
        {
            m_Playertemp = m_Roomtemp + t;
        }

        else if (t < 0 && m_Playertemp < (m_Roomtemp + t))
        {
            m_Playertemp = m_Roomtemp + t;
        }
    }
}
