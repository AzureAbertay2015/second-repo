using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour {

    public GameManager m_Gamemanager;
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
	void Update () {

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
            m_Gamemanager.ChangeState(2);
            m_Gamemanager.ChangeLayer();
        }

        else if ((int)m_Playertemp > 10)
        {
            m_Gamemanager.ChangeState(1);
            m_Gamemanager.ChangeLayer();
        }

        else
        {
            m_Gamemanager.ChangeState(0);
            m_Gamemanager.ChangeLayer();
        }

    }
}
