using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour
{

    public float m_Roomtemp;
    public float m_Playertemp;
    public float m_Prevplayertemp;

    public float m_Tempchange;

    public float m_LiqGascutoff;
    public float m_SolidLiqcutoff;

    public float m_Abilitytempchange;

    private EnergyBarScript m_Energyscript;

    private bool m_Trigger;

    // Use this for initialization
    void Start()
    {

        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
        m_Prevplayertemp = m_Playertemp;
        m_LiqGascutoff = 40.0f;
        m_SolidLiqcutoff = 10.0f;
        m_Tempchange = 2.0f;
        m_Abilitytempchange = 20.0f;

        m_Trigger = false;

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
                m_Playertemp -= m_Abilitytempchange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if (Ekey)
        {
            if (m_Energyscript.TempUp())
            {
                //Debug.Log("temp before: " + m_Playertemp);
                m_Playertemp += m_Abilitytempchange;
                //Debug.Log("temp after: " + m_Playertemp);
            }
        }

        if(!m_Trigger)
        {
            if (m_Playertemp > m_Roomtemp)
            {
                m_Playertemp -= m_Tempchange * Time.deltaTime;
            }

            if (m_Playertemp < m_Roomtemp)
            {
                m_Playertemp += m_Tempchange * Time.deltaTime;
            }
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

    }

    public void ChangeRoomTemp(float t)
    {
        m_Roomtemp += t;
    }

    public void ChangePlayerTemp(float t)
    {
        m_Playertemp += t;
    }

    public void SetPlayerTemp(float t)
    {
        m_Playertemp = t;
    }

    public void HeaterCooler(bool trigger)
    {
        m_Trigger = trigger;
    }

}
