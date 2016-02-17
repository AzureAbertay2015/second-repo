using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{

    private float m_Fillamount;

    [SerializeField]
    private Image Filler;

    // Use this for initialization
    void Start()
    {

        m_Fillamount = 0;

    }

    // Update is called once per frame
    void Update()
    {

        UpdateFiller();

        if (m_Fillamount - m_Fillamount < 100)
        {
            m_Fillamount += 0.05f * Time.deltaTime;
        }


    }

    private void UpdateFiller()
    {
        Filler.fillAmount = m_Fillamount;
    }

    public bool TempUp()
    {
        if (m_Fillamount > 0.3f)
        {
            m_Fillamount -= 0.3f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TempDown()
    {
        if (m_Fillamount > 0.3f)
        {
            m_Fillamount -= 0.3f;

            return true;
        }
        else
        {
            return false;
        }
    }
}
