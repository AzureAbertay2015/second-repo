using UnityEngine;
using System.Collections;

// Modified version of Adam's Temperature manager
// Now can handle any number of state changing objects and not just player

public class TemperatureManager : MonoBehaviour {
    
    public float m_Roomtemp;

    public float m_TemperatureChange;

    public float m_Abilitytempchange;

    private EnergyBarScript m_Energyscript;
    private StateChanger[] m_stateChangers;

    [HideInInspector]
    public float m_PlayerTemperature;

    private bool m_Trigger;

	// Use this for initialization
	void Start ()
    {        
        m_Roomtemp = 20.0f;
        m_TemperatureChange = 2.0f;
        m_Abilitytempchange = 20.0f;
        m_PlayerTemperature = GameManager.GetPlayer().m_Temperature;
        m_Trigger = false;

        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        m_PlayerTemperature = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_Temperature;
        m_stateChangers = FindObjectsOfType(typeof(StateChanger)) as StateChanger[]; 
    }

	// Update is called once per frame
	void Update ()
    {
        bool upArrow = Input.GetKeyDown(KeyCode.E);
        bool downArrow = Input.GetKeyDown(KeyCode.Q);

        if (upArrow)
        {
            if (m_Energyscript.TempUp())
            {
               m_PlayerTemperature += m_Abilitytempchange;
            }
        }

        if (downArrow)
        {
            if (m_Energyscript.TempDown())
            {
               m_PlayerTemperature -= m_Abilitytempchange;
            }
        }

        if (!m_Trigger)
        {
            if (m_PlayerTemperature > m_Roomtemp)
            {
                m_PlayerTemperature -= m_TemperatureChange * Time.deltaTime * 2;
            }

            if (m_PlayerTemperature < m_Roomtemp)
            {
                m_PlayerTemperature += m_TemperatureChange * Time.deltaTime * 2;
            }
        }

        /*if (m_Playertemp > m_Roomtemp)
        {
            m_Playertemp -= m_TemperatureChange * Time.deltaTime;
        }

        if (m_Playertemp < m_Roomtemp) bv
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

        m_Prevplayertemp = m_Playertemp;*/

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

            if (stateChanger.m_Temperature >= stateChanger.m_SolidLiquidCutoff && stateChanger.m_Temperature < stateChanger.m_LiquidGasCutoff && (stateChanger.m_PrevTemperature >= stateChanger.m_LiquidGasCutoff || stateChanger.m_PrevTemperature < stateChanger.m_SolidLiquidCutoff))
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
        GameManager.GetPlayer().m_Temperature += t;
    }

    public void SetPlayerTemp(float t)
    {
        GameManager.GetPlayer().m_Temperature = t;
    }

    public void HeaterCooler(bool trigger)
    {
        m_Trigger = trigger;
    }
}
