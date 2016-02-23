using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour {
    
    public float m_Roomtemp;
    public float m_Playertemp;
    public float m_Prevplayertemp;

    public float m_TemperatureChange;

    public float m_LiqGascutoff;
    public float m_SolidLiqcutoff;

    public float m_Abilitytempchange;

    private EnergyBarScript m_Energyscript;
    private StateChanger[] m_stateChangers;

	// Use this for initialization
	void Start ()
    {        
        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
        m_Prevplayertemp = m_Playertemp;
        m_LiqGascutoff = 40.0f;
        m_SolidLiqcutoff = 30.0f;
        m_TemperatureChange = 2.0f;
        m_Abilitytempchange = 20.0f;

        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        m_stateChangers = FindObjectsOfType(typeof(StateChanger)) as StateChanger[]; 
    }

	// Update is called once per frame
	void Update ()
    {
        bool upArrow = Input.GetKeyDown(KeyCode.Q);
        bool downArrow = Input.GetKeyDown(KeyCode.E);

        if (upArrow)
        {
            if (m_Energyscript.TempUp())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                m_Playertemp += m_Abilitytempchange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (downArrow)
        {
            if (m_Energyscript.TempDown())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                m_Playertemp -= m_Abilitytempchange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (m_Playertemp > m_Roomtemp)
        {
            m_Playertemp -= m_TemperatureChange * Time.deltaTime;
        }

        if (m_Playertemp < m_Roomtemp)
        {
            m_Playertemp += m_TemperatureChange * Time.deltaTime; 
        }

        if (m_Playertemp >= m_LiqGascutoff && m_Prevplayertemp < m_LiqGascutoff)
        {
            //Debug.Log("GAS");
            GameManager.GetPlayer().ChangeState(Player.State.Gas);
        }

        if (m_Playertemp >= m_SolidLiqcutoff && m_Playertemp < m_LiqGascutoff && (m_Prevplayertemp >= m_LiqGascutoff || m_Prevplayertemp < m_SolidLiqcutoff))
        {
            //Debug.Log("LIQUID");
            GameManager.GetPlayer().ChangeState(Player.State.Liquid);
        }

        if (m_Playertemp < m_SolidLiqcutoff && m_Prevplayertemp >= m_SolidLiqcutoff)
        {
            //Debug.Log("SOLID");
            GameManager.GetPlayer().ChangeState(Player.State.Solid);
        }

        m_Prevplayertemp = m_Playertemp;

        foreach (StateChanger stateChanger in m_stateChangers)
        {
            if (stateChanger.m_Temperature > m_Roomtemp)
            {
                stateChanger.m_Temperature -= m_TemperatureChange * Time.deltaTime;
            }

            if (stateChanger.m_Temperature < m_Roomtemp)
            {
                stateChanger.m_Temperature += m_TemperatureChange * Time.deltaTime;
            }

            if (stateChanger.m_Temperature >= stateChanger.m_LiquidGasCutoff && stateChanger.m_PrevTemperature < stateChanger.m_LiquidGasCutoff)
            {
                //Debug.Log("GAS");
                stateChanger.ChangeState(StateChanger.State.Gas);
            }

            if (stateChanger.m_Temperature >= stateChanger.m_SolidLiquidCutoff && stateChanger.m_Temperature < m_LiqGascutoff && (stateChanger.m_PrevTemperature >= stateChanger.m_LiquidGasCutoff || stateChanger.m_PrevTemperature < stateChanger.m_SolidLiquidCutoff))
            {
                //Debug.Log("LIQUID");
                stateChanger.ChangeState(StateChanger.State.Liquid);
            }

            if (stateChanger.m_Temperature < stateChanger.m_SolidLiquidCutoff && stateChanger.m_PrevTemperature >= stateChanger.m_SolidLiquidCutoff)
            {
                //Debug.Log("SOLID");
                stateChanger.ChangeState(StateChanger.State.Solid);
            }

            stateChanger.m_PrevTemperature = stateChanger.m_Temperature;
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
