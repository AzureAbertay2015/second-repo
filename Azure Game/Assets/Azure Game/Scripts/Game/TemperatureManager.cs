using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Modified version of Adam's Temperature manager
// Now can handle any number of state changing objects and not just player

public class TemperatureManager : MonoBehaviour {
    
    public float m_RoomTemperature;

    public float m_TemperatureChange;

    public float m_AbilityTemperatureChange;

    private EnergyBarScript m_Energyscript;
    private StateChanger[] m_stateChangers;

	// Use this for initialization
	void Start ()
    {        
        m_TemperatureChange = 2.0f;
        m_AbilityTemperatureChange = 20.0f;

        m_RoomTemperature = GameManager.temperatureValues[0];
        m_TemperatureChange = GameManager.temperatureValues[1];
        m_AbilityTemperatureChange = GameManager.temperatureValues[2];

        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        //m_stateChangers = FindObjectsOfType(typeof(StateChanger)) as List<StateChanger>;
        m_stateChangers = FindObjectsOfType(typeof(StateChanger)) as StateChanger[];
    }

    // Update is called once per frame
    void Update()
    {
        bool upArrow = Input.GetKeyDown(KeyCode.E);
        bool downArrow = Input.GetKeyDown(KeyCode.Q);

        if (upArrow)
        {
            if (m_Energyscript.TempUp())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                GameManager.GetPlayer().m_Temperature += m_AbilityTemperatureChange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (downArrow)
        {
            if (m_Energyscript.TempDown())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                GameManager.GetPlayer().m_Temperature -= m_AbilityTemperatureChange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }
              

        foreach (StateChanger stateChanger in m_stateChangers)
        {

            if (!stateChanger.m_Triggered)
            {
                if (GameManager.GetPlayer().m_Temperature > m_RoomTemperature)
                {
                    GameManager.GetPlayer().m_Temperature -= m_TemperatureChange * Time.deltaTime;
                }

                if (GameManager.GetPlayer().m_Temperature < m_RoomTemperature)
                {
                    GameManager.GetPlayer().m_Temperature += m_TemperatureChange * Time.deltaTime;
                }
            }
            else
            {
                if (stateChanger.m_Temperature > m_RoomTemperature)
                {
                    stateChanger.m_Temperature -= m_TemperatureChange * Time.deltaTime;
                }

                if (stateChanger.m_Temperature < m_RoomTemperature)
                {
                    stateChanger.m_Temperature += m_TemperatureChange * Time.deltaTime;
                }
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
        m_RoomTemperature += t;
    }

    public void ChangeObjectTemp(float t, StateChanger statechanger)
    {
        statechanger.m_Temperature += t;
    }

    public void SetObjectTemp(float t, StateChanger statechanger)
    {
        statechanger.m_Temperature = t;
    }

    public int GetNumberStateChangers()
    {
        //return m_stateChangers.Count;
        return 0;
    }

    public void AddStateChanger(StateChanger statechanger)
    {
       // m_stateChangers.Add(statechanger);
    }
}
