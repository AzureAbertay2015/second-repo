using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
 public class TimerScript : MonoBehaviour
{     
     private float m_time;
     private Text m_timeText;
 
     // Use this for initialization
    void Start()
    {
        m_timeText = GetComponent<Text>();
        m_time = GameManager.m_LevelLength;
    }
 
 	void Awake()
     {
         DontDestroyOnLoad(this);
     }
 	
 	// Update is called once per frame
 	void Update()
     {
        m_time -= Time.deltaTime;
        m_timeText.text = timeConvert(m_time);
       
        if (m_time <= 0)
        {
            m_time = 0;
            m_timeText.text = timeConvert(m_time);
            GameManager.GetGameRules().KillPlayer();

            //m_time = GameManager.m_LevelLength;
        }
    }
 
     string timeConvert(float time)
     {
         string minutes = Mathf.Floor(time / 60).ToString("00");
         string seconds = (Mathf.Floor(time % 60).ToString("00"));
        //string milliSeconds = ((time * 1000f) % 1000f).ToString("000");

        return minutes + "m " + seconds + "s ";// + milliSeconds + "ms";
     }

    public void addTime(float time)
    {
        m_time += time;
    }

    public float getTimeLeft()
    {
        return m_time;
    }

 }