using UnityEngine;
using System.Collections;

public class TemperatureManager : MonoBehaviour {
    
    public float m_Roomtemp;
    public float m_Playertemp;
    public float m_Prevplayertemp;
	public float m_MinPlayerTemp;
	public float m_MaxPlayerTemp;
	private float m_TempChangeSpeed;
	public float m_MaxTempChangeSpeed;
	public float m_MinTempChangeSpeed;

    public float m_Tempchange;

    public float m_LiqGascutoff;
    public float m_SolidLiqcutoff;

    public float m_Abilitytempchange;

    private EnergyBarScript m_Energyscript;
    

	// Use this for initialization
	void Start () {
        
        m_Roomtemp = 20.0f;
        m_Playertemp = -10.0f;
        m_Prevplayertemp = m_Playertemp;
        m_LiqGascutoff = 40.0f;
        m_SolidLiqcutoff = 10.0f;
        m_Tempchange = 2.0f;
        m_Abilitytempchange = 20.0f;
		m_MinPlayerTemp = -100.0f;
		m_MaxPlayerTemp = 100.0f;
		m_TempChangeSpeed = 5.0f;
		m_MaxTempChangeSpeed = 20;
		m_MinTempChangeSpeed = 5.0f;


        m_Energyscript = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBarScript>();
        
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
            m_Playertemp += m_Tempchange * Time.deltaTime; ;
        }

        if (m_Playertemp >= m_LiqGascutoff && m_Prevplayertemp < m_LiqGascutoff)
        {
            //Debug.Log("GAS");
            GameManager.GetPlayer().ChangeState(Player.State.Gas);
        }

        if (m_Playertemp >= m_SolidLiqcutoff &&  m_Playertemp < m_LiqGascutoff && (m_Prevplayertemp >= m_LiqGascutoff || m_Prevplayertemp < m_SolidLiqcutoff))
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

        if(t > 0 && m_Playertemp > (m_Roomtemp + t))
        {
            m_Playertemp = m_Roomtemp + t;
        }

        else if (t < 0 && m_Playertemp < (m_Roomtemp + t))
        {
            m_Playertemp = m_Roomtemp + t;
        }
    }

	public IEnumerator HeatUpPlayer()
	{
		if (m_Playertemp <= m_MaxPlayerTemp)
		{
			while (m_Playertemp <= m_MaxPlayerTemp)
			{
				AccelerateTempChange();
				m_Playertemp = Mathf.MoveTowards(m_Playertemp, m_MaxPlayerTemp, m_TempChangeSpeed * Time.deltaTime);
				if (m_MaxPlayerTemp - m_Playertemp <= 0.01f)
				{
					m_Playertemp = m_MaxPlayerTemp;
				}
				yield return null;
			}
		}
	}

	public IEnumerator CoolDownPlayer()
	{
		if (m_Playertemp >= m_MinPlayerTemp)
		{
			while (m_Playertemp >= m_MinPlayerTemp)
			{
				AccelerateTempChange();
				m_Playertemp = Mathf.MoveTowards(m_Playertemp, m_MinPlayerTemp, m_TempChangeSpeed * Time.deltaTime);
				if (-m_MinPlayerTemp + m_Playertemp <= 0.01f)
				{
					m_Playertemp = m_MinPlayerTemp;
				}
				yield return null;
			}			
		}
	}

	public void ResetTempChangeSpeed()
	{
		m_TempChangeSpeed = m_MinTempChangeSpeed;
	}

	private void AccelerateTempChange()
	{
		m_TempChangeSpeed = Mathf.Pow(m_TempChangeSpeed, 1.005f);
		if (m_TempChangeSpeed >= m_MaxTempChangeSpeed)
			m_TempChangeSpeed = m_MaxTempChangeSpeed;
	}

	private void DecelerateTempChange()
	{
		m_TempChangeSpeed = Mathf.Pow(m_TempChangeSpeed, 0.99f);
		if (m_TempChangeSpeed <= m_MinTempChangeSpeed)
			m_TempChangeSpeed = m_MinTempChangeSpeed;
	}
}
