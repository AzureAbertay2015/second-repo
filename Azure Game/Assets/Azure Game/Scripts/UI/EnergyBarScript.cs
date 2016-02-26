using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{

    public float m_Fillamount;

    [SerializeField]
    private Image Filler;

    public float m_Chargeamount;

    // Use this for initialization
    void Start()
    {

        m_Fillamount = 0;
        m_Chargeamount = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {

        UpdateFiller();

        if (m_Fillamount + (m_Chargeamount * Time.deltaTime) < 1.0f)
        {
            m_Fillamount += m_Chargeamount * Time.deltaTime;
        }

        //Debug.Log("fill amount " + m_Fillamount);


        m_Chargeamount += (0.01f * Time.deltaTime);
       
    }

    private void UpdateFiller()
    {
        Filler.fillAmount = m_Fillamount;
    }

    public bool TempUp()
    {
        if (m_Fillamount > 0.5f)
        {
            m_Fillamount -= 0.5f;

            m_Chargeamount = 0.01f;

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TempDown()
    {
        if (m_Fillamount > 0.5f)
        {
            m_Fillamount -= 0.5f;

            m_Chargeamount = 0.01f;

            return true;
        }
        else
        {
            return false;
        }
    }
}
