using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour
{

    public float m_Roomtemperature;
    public float m_Playertemperature;
    public float m_Prevplayertemperature;

    public float m_Temperaturechange;

    public float m_LiqGascutoff;
    public float m_SolidLiqcutoff;

    public float m_Abilitytemperaturechange;

    private EnergyBarScript m_Energyscript;

    private bool m_Trigger;

    // Use this for initialization
    void Start()
    {
        m_Prevplayertemperature = m_Playertemperature;
        m_LiqGascutoff = 40.0f;
        m_SolidLiqcutoff = 10.0f;
        m_Trigger = false;

        m_Roomtemperature = GameManager.temperatureValues[0];
        m_Playertemperature = GameManager.temperatureValues[1];
        m_Temperaturechange = GameManager.temperatureValues[2];
        m_Abilitytemperaturechange = GameManager.temperatureValues[3];

        //Get the energy script from it's tag
        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();

    }


    // Update is called once per frame
    void Update()
    {
        //Check if Q or E is down
        bool Qkey = Input.GetKeyDown(KeyCode.Q);
        bool Ekey = Input.GetKeyDown(KeyCode.E);

        if (Qkey)
        {
            if (m_Energyscript.TempDown())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                m_Playertemperature -= m_Abilitytemperaturechange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (Ekey)
        {
            if (m_Energyscript.TempUp())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                m_Playertemperature += m_Abilitytemperaturechange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if(!m_Trigger)
        {
            if (m_Playertemperature > m_Roomtemperature)
            {
                m_Playertemperature -= m_Temperaturechange * Time.deltaTime;
            }

            if (m_Playertemperature < m_Roomtemperature)
            {
                m_Playertemperature += m_Temperaturechange * Time.deltaTime;
            }
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

        m_Prevplayertemperature = m_Playertemperature;
        
    }

    public void ChangeRoomTemp(float t)
    {
        m_Roomtemperature += t;
    }

    public void ChangePlayerTemp(float t)
    {
        m_Playertemperature += t;
    }

    public void SetPlayerTemp(float t)
    {
        m_Playertemperature = t;
    }

    public void HeaterCooler(bool trigger)
    {
        m_Trigger = trigger;
    }

}
