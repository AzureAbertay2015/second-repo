using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour {
    
    public float m_Roomtemp;
    public float m_Playertemp;
    public float m_Prevplayertemp;

    public float m_Tempchange;

    public float m_LiqGascutoff;
    public float m_SolidLiqcutoff;

    public float m_Abilitytempchange;

    private EnergyBarScript m_Energyscript;
    private TemperatureScript[] m_temperatureScripts;

	// Use this for initialization
	void Start ()
    {        
        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
        m_Prevplayertemp = m_Playertemp;
        m_LiqGascutoff = 40.0f;
        m_SolidLiqcutoff = 10.0f;
        m_Tempchange = 2.0f;
        m_Abilitytempchange = 20.0f;

        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        m_temperatureScripts = FindObjectsOfType(typeof(TemperatureScript)) as TemperatureScript[]; 
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
            m_Playertemp -= m_Tempchange * Time.deltaTime;
        }

        if (m_Playertemp < m_Roomtemp)
        {
            m_Playertemp += m_Tempchange * Time.deltaTime; 
        }

        if (m_Playertemp >= m_LiqGascutoff && m_Prevplayertemp < m_LiqGascutoff)
        {
            //Debug.Log("GAS");
            GameManager.GetPlayer().ChangeState(2);
        }

        if (m_Playertemp >= m_SolidLiqcutoff && m_Playertemp < m_LiqGascutoff && (m_Prevplayertemp >= m_LiqGascutoff || m_Prevplayertemp < m_SolidLiqcutoff))
        {
            //Debug.Log("LIQUID");
            GameManager.GetPlayer().ChangeState(1);
        }

        if (m_Playertemp < m_SolidLiqcutoff && m_Prevplayertemp >= m_SolidLiqcutoff)
        {
            //Debug.Log("SOLID");
            GameManager.GetPlayer().ChangeState(0);
        }

        m_Prevplayertemp = m_Playertemp;

        foreach (TemperatureScript m_temperatureScript in m_temperatureScripts)
        {
            if (m_temperatureScript.m_temp > m_Roomtemp)
            {
                m_temperatureScript.m_temp -= m_Tempchange * Time.deltaTime;
            }

            if (m_temperatureScript.m_temp < m_Roomtemp)
            {
                m_temperatureScript.m_temp += m_Tempchange * Time.deltaTime;
            }

            if (m_temperatureScript.m_temp >= m_temperatureScript.m_LiquidGasCutoff && m_temperatureScript.m_prevTemp < m_temperatureScript.m_LiquidGasCutoff)
            {
                //Debug.Log("GAS");
                //GameManager.GetPlayer().ChangeState(2);
            }

            if (m_temperatureScript.m_temp >= m_temperatureScript.m_SolidLiquidCutoff && m_temperatureScript.m_temp < m_LiqGascutoff && (m_temperatureScript.m_prevTemp >= m_temperatureScript.m_LiquidGasCutoff || m_temperatureScript.m_prevTemp < m_temperatureScript.m_SolidLiquidCutoff))
            {
                //Debug.Log("LIQUID");
                //GameManager.GetPlayer().ChangeState(1);
            }

            if (m_temperatureScript.m_temp < m_temperatureScript.m_SolidLiquidCutoff && m_temperatureScript.m_prevTemp >= m_temperatureScript.m_SolidLiquidCutoff)
            {
                //Debug.Log("SOLID");
                //GameManager.GetPlayer().ChangeState(0);
            }
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
