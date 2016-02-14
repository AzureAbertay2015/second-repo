using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour {
    
    public float m_Roomtemp;
    public float m_Playertemp;

    public float m_Tempchange;
    

	// Use this for initialization
	void Start () {
        
        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
        m_Tempchange = 2.0f;
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (m_Playertemp > m_Roomtemp)
        {
            m_Playertemp -= m_Tempchange * Time.deltaTime;
        }

        if (m_Playertemp < m_Roomtemp)
        {
            m_Playertemp += m_Tempchange * Time.deltaTime; ;
        }

        if ((int)m_Playertemp > 40)
        {
            GameManager.GetPlayer().ChangeState(2);
        }

        else if ((int)m_Playertemp > 10)
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
    }
}
