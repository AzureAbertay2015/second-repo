using UnityEngine;
using System.Collections;

// Modified version of Adam's Temperature manager
// Now can handle any number of state changing objects and not just player

public class TemperatureManager : MonoBehaviour {
    
    public float m_RoomTemperature;

    public float m_TemperatureChange;

    public float m_AbilityTemperatureChange;

    private EnergyBarScript m_Energyscript;
    private StateChanger[] m_stateChangers;
   
    private Player m_Player;

    private bool m_Trigger;

	// Use this for initialization
	void Start ()
    {        
        m_TemperatureChange = 2.0f;
        m_AbilityTemperatureChange = 20.0f;
        m_Player = GameManager.GetPlayer();
        m_Trigger = false;

        m_RoomTemperature = GameManager.temperatureValues[0];
        m_TemperatureChange = GameManager.temperatureValues[1];
        m_AbilityTemperatureChange = GameManager.temperatureValues[2];

        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
                m_Player.m_Temperature += m_AbilityTemperatureChange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (downArrow)
        {
            if (m_Energyscript.TempDown())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                m_Player.m_Temperature -= m_AbilityTemperatureChange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

<<<<<<< HEAD
        if (!m_Trigger)
        {
            if (m_Player.m_Temperature > m_RoomTemperature)
            {
                m_Player.m_Temperature -= m_TemperatureChange * Time.deltaTime;
            }

            if (m_Player.m_Temperature < m_RoomTemperature)
            {
                m_Player.m_Temperature += m_TemperatureChange * Time.deltaTime;
            }
        }

        /*if (m_Playertemp > m_Roomtemp)
        {
            m_Playertemp -= m_TemperatureChange * Time.deltaTime;
        }

        if (m_Playertemp < m_Roomtemp) 
        {
            m_Playertemp += m_TemperatureChange * Time.deltaTime; 
        }

        if (m_Playertemperature >= m_LiqGascutoff && m_Prevplayertemperature < m_LiqGascutoff)
        {
            //Debug.Log("GAS");
            GameManager.GetPlayer().ChangeState(Player.State.Gas);
        }

        if (m_Playertemperature >= m_SolidLiqcutoff && m_Playertemperature < m_LiqGascutoff && (m_Prevplayertemperature >= m_LiqGascutoff || m_Prevplayertemperature < m_SolidLiqcutoff))
        {
            //Debug.Log("LIQUID");
            GameManager.GetPlayer().ChangeState(Player.State.Liquid);
        }

        if (m_Playertemperature < m_SolidLiqcutoff && m_Prevplayertemperature >= m_SolidLiqcutoff)
        {
            //Debug.Log("SOLID");
            GameManager.GetPlayer().ChangeState(Player.State.Solid);
        }

       /* m_Prevplayertemp = m_Playertemp;*/
=======
       
>>>>>>> refs/remotes/origin/master

        foreach (StateChanger stateChanger in m_stateChangers)
        {
            if(stateChanger.tag == "Player")
            {
                if (!m_Trigger)
                {
                    if (m_Player.m_Temperature > m_RoomTemperature)
                    {
                        m_Player.m_Temperature -= m_TemperatureChange * Time.deltaTime;
                    }

                    if (m_Player.m_Temperature < m_RoomTemperature)
                    {
                        m_Player.m_Temperature += m_TemperatureChange * Time.deltaTime;
                    }
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

    public void ChangePlayerTemp(float t)
    {
        m_Player.m_Temperature += t;
    }

    public void SetPlayerTemp(float t)
    {
        m_Player.m_Temperature = t;
    }

    public void HeaterCooler(bool trigger)
    {
        m_Trigger = trigger;
    }
}
